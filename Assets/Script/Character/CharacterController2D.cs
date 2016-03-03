﻿using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class CharacterController2D : MonoBehaviour
{
    public float _velocity = 10.0f;
    public float _velocityOnAir = 10.0f;
    public float _velocityJump = 10.0f;
    protected Rigidbody2D _body = null;
    protected BoxCollider2D _collider = null;
    private CharacterState _state = CharacterState.OnGround;
    protected bool _wallOnRight = false;
    public int Side { get; set; }

    #region lifecyle
    virtual protected void Start()
    {
        this._body = this.GetComponent<Rigidbody2D>();
        this._collider = this.GetComponent<BoxCollider2D>();
    }

    virtual protected void Update()
    {
        this.HandleCasts();
        if (Input.GetAxis("Horizontal") > 0)
        {
            this.Side = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            this.Side = -1;
        }
    }

    virtual protected void FixedUpdate()
    {
        this.MovePlayer();
    }
    #endregion

    virtual protected void MovePlayer()
    {
        Vector2 direction;
        if (this.ShouldMove(out direction))
        {
            this._body.AddForce(direction, ForceMode2D.Impulse);
        }

        if (this.ShouldJump(out direction))
        {
            this._body.AddForce(direction, ForceMode2D.Impulse);
            this.AddState(CharacterState.OnJump);
        }
    }

    virtual protected bool ShouldJump(out Vector2 jumpDirection)
    {
        jumpDirection = Vector2.up;
        return true;
    }

    virtual protected bool ShouldMove(out Vector2 moveDirection)
    {
        moveDirection = Vector2.right;
        return true;
    }

    virtual protected void HandleCasts()
    {
        bool collisionOnSide = false;
        bool collisionOnBottom = false;

        Vector2 origin = this.transform.position;
        origin.y += this._collider.bounds.extents.y / 2;
        RaycastHit2D hitA = Physics2D.Raycast(origin, Vector2.right);
        origin.y -= this._collider.bounds.extents.y;
        RaycastHit2D hitB = Physics2D.Raycast(origin, Vector2.right);
        if ((hitA.collider != null && hitA.distance <= this._collider.bounds.extents.x) || (hitB.collider != null && hitB.distance <= this._collider.bounds.extents.x))
        {
            collisionOnSide = true;
            this._wallOnRight = true;
            this.Side = 1;
        }
        else
        {
            origin = this.transform.position;
            origin.y += this._collider.bounds.extents.y / 2;
            hitA = Physics2D.Raycast(origin, Vector2.left);
            origin.y -= this._collider.bounds.extents.y;
            hitB = Physics2D.Raycast(origin, Vector2.left);
            if ((hitA.collider != null && hitA.distance <= this._collider.bounds.extents.x) || (hitB.collider != null && hitB.distance <= this._collider.bounds.extents.x))
            {
                collisionOnSide = true;
                this._wallOnRight = false;
                this.Side = -1;
            }
        }

        origin = this.transform.position;
        origin.x += this._collider.bounds.extents.x / 2;
        hitA = Physics2D.Raycast(origin, Vector2.down);
        origin.x -= this._collider.bounds.extents.x;
        hitB = Physics2D.Raycast(origin, Vector2.down);
        if ((hitA.collider != null && hitA.distance <= this._collider.bounds.extents.y) || (hitB.collider != null && hitB.distance <= this._collider.bounds.extents.y))
        {
            collisionOnBottom = true;
        }

        if (collisionOnBottom)
        {
            this.AddState(CharacterState.OnGround);
        }
        else
        {
            this.RemoveState(CharacterState.OnGround);
        }

        if (collisionOnSide)
        {
            this.AddState(CharacterState.OnWall);
        }
        else
        {
            this.RemoveState(CharacterState.OnWall);
        }
    }

    public bool GetState(CharacterState state)
    {
        return (this._state & state) == state;
    }

    virtual public void AddState(CharacterState state)
    {
        bool changed = (this._state & state) != state;
        this._state |= state;
        switch (state)
        {
            case CharacterState.OnGround:
                this.RemoveState(CharacterState.OnJump);
                break;
            case CharacterState.OnJump:
                this.RemoveState(CharacterState.OnGround);
                this.RemoveState(CharacterState.OnWall);
                break;
            case CharacterState.OnWall:
                this.RemoveState(CharacterState.OnJump);
                break;
            case CharacterState.Shooting:
            default:
                break;
        }
        if (changed)
        {
            this.OnStateChange(state, true);
        }
    }

    virtual public void RemoveState(CharacterState state)
    {
        bool changed = (this._state & state) == state;
        if (changed)
        {
            this._state &= ~state;
            this.OnStateChange(state, false);
        }
    }

    public event StateChange OnStateChange;
    public delegate void StateChange(CharacterState state, bool included);
}