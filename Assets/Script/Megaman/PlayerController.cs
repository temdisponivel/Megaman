using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    [Flags]
    public enum PlayerState
    {
        OnGround = 1,
        OnJump = 2,
        OnWall = 4,
    }

    public float _velocity = 10.0f;
    public float _velocityJump = 10.0f;
    protected Rigidbody2D _body = null;
    protected Animator _animator = null;
    protected SpriteRenderer _spriteRenderer = null;
    protected Collider2D[] _cicleColliders = null;
    protected Collider2D _boxCollider = null;
    protected PlayerState _state = PlayerState.OnGround;
    protected Collider2D _collisionWall = null;
    protected float _gravityScaleOnWall = .2f;
    protected float _gravityScaleBkp = 1f;

    #region lifecyle
    protected void Start()
    {
        this._body = this.GetComponent<Rigidbody2D>();
        this._animator = this.GetComponent<Animator>();
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._cicleColliders = this.GetComponents<CircleCollider2D>();
        this._boxCollider = this.GetComponent<BoxCollider2D>();
        this._gravityScaleBkp = this._body.gravityScale;
    }

    protected void Update()
    {
        this.HandleAnimation();
    }

    protected void FixedUpdate()
    {
        this.MovePlayer();
    }
    #endregion

    protected void MovePlayer()
    {
        Vector2 direction;
        if (this.ShouldMove(out direction))
        {
            this._body.AddForce(direction, ForceMode2D.Impulse);
        }

        if (this.ShouldJump(out direction))
        {
            Vector2 sideDirection = Vector2.zero;
            if ((this._state & PlayerState.OnWall) == PlayerState.OnWall)
            {
                /*
                Vector2 wallPosition = this._collisionWall.transform.position - this.transform.position;
                if (wallPosition.x >= 0)
                {
                    this._body.AddForce(Vector2.left * this._velocityJump, ForceMode2D.Impulse);
                }
                else
                {
                    this._body.AddForce(Vector2.right * this._velocityJump, ForceMode2D.Impulse);
                }
                */
            }
            this._body.gravityScale = this._gravityScaleBkp;
            this._body.AddForce(direction, ForceMode2D.Impulse);
            this._animator.SetBool("jumping", true);
            this._state |= PlayerState.OnJump;
        }
    }

    protected void HandleAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        this._animator.SetFloat("velocity", Mathf.Abs(horizontal) * (this._state == PlayerState.OnGround ? 1 : 0));

        if (horizontal != 0)
        {
            if (this._spriteRenderer.flipX && horizontal > 0 || !this._spriteRenderer.flipX && horizontal < 0)
            {
                this._spriteRenderer.flipX = !this._spriteRenderer.flipX;
            }
        }

        if ((this._state & PlayerState.OnJump) != PlayerState.OnJump)
        {
            this._animator.SetBool("jumping", false);
        }
    }

    #region Physics

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            this._gravityScaleBkp = this._body.gravityScale;
            this._body.gravityScale = this._gravityScaleOnWall;
            this._collisionWall = collider;
            this._state |= PlayerState.OnWall;
            //this._state &= ~PlayerState.OnJump;
        }
        else if (collider.gameObject.tag == "Ground")
        {
            this._body.gravityScale = this._gravityScaleBkp;
            this._state |= PlayerState.OnGround;
            this._state &= ~PlayerState.OnJump;
        }
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            this._body.gravityScale = this._gravityScaleBkp;
            this._state &= ~PlayerState.OnWall;
        }
        else if (collider.gameObject.tag == "Ground")
        {
            this._state &= ~PlayerState.OnGround;
        }
    }
    #endregion

    protected bool ShouldJump(out Vector2 jumpDirection)
    {
        jumpDirection = Vector2.up * this._velocityJump;
        return (Input.GetButton("Jump") && (this._state & PlayerState.OnJump) != PlayerState.OnJump);
    }

    protected bool ShouldMove(out Vector2 moveDirection)
    {
        float horizontal = Input.GetAxis("Horizontal");
        moveDirection = Vector2.right * 0;
        if ((this._state & PlayerState.OnWall) == PlayerState.OnWall)
        {
            Vector2 wallPosition = this._collisionWall.transform.position - this.transform.position;
            Debug.Log(wallPosition);
            if (horizontal > 0 && wallPosition.x < 0 || horizontal < 0 && wallPosition.x > 0)
            {
                moveDirection = Vector2.right * ((this._velocity * horizontal) - this._body.velocity.x);
            }
        }
        else
        {
            moveDirection = Vector2.right * ((this._velocity * horizontal) - this._body.velocity.x);
        }
        return true;
    }
}