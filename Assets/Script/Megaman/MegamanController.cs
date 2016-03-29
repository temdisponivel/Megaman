using UnityEngine;
using System.Collections;

public class MegamanController : CharacterController2D
{
    static public MegamanController Instance = null;
    public Character _character = null;
    public float _dashImpulse = 1f;
    public float _dashCoolDown = 1f;
    protected float _lastDashTime = 0f;

    override public void Start()
    {
        if (MegamanController.Instance == null)
        {
            MegamanController.Instance = this;
        }
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time - this._lastDashTime >= this._dashCoolDown && !this.GetState(CharacterState.Dashing))
            {
                this._body.velocity += Vector2.right * this.Side * this._dashImpulse;
                this._lastDashTime = Time.time;
                this.AddState(CharacterState.Dashing);
            }
        }
    }
    
    override protected bool ShouldMove(out Vector2 moveDirection)
    {
        moveDirection = Vector2.zero;
        float horizontal = Input.GetAxis("Horizontal");
        float velocity = this.GetState(CharacterState.OnGround) ? this._velocity : this._velocityOnAir;
        if (horizontal != 0)
        {
            moveDirection = Vector2.right * ((velocity * horizontal) - (this.GetState(CharacterState.Dashing) ? this._body.velocity.x / 2 : this._body.velocity.x));
        }
        return horizontal != 0;
    }

    override protected bool ShouldJump(out Vector2 jumpDirection)
    {
        bool jump = (Input.GetButtonDown("Jump") && (!this.GetState(CharacterState.OnJump) || this.GetState(CharacterState.OnWall)));
        jumpDirection = Vector2.up * this._velocityJump;
        if (jump && this.GetState(CharacterState.OnWall) && !this.GetState(CharacterState.OnGround))
        {
            jumpDirection = ((this.WallOnRight ? Vector2.left : Vector2.right) * 3) + (Vector2.up * this._velocityJump * 1.3f);
            this.Side = this.WallOnRight ? -1 : 1;
        }
        return jump;
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" && !this.GetState(CharacterState.Damage))
        {
            this._character.HP--;
            this.AddState(CharacterState.Damage);
        }
        else if (collision.gameObject.tag == "Life")
        {
            this._character.HP += collision.gameObject.GetComponent<Life>()._life;
            GameObject.Destroy(collision.gameObject);
        }
    }
}
