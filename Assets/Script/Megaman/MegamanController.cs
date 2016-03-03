using UnityEngine;
using System.Collections;

public class MegamanController : CharacterController2D
{
    override protected bool ShouldJump(out Vector2 jumpDirection)
    {
        bool jump = (Input.GetButton("Jump") && (!this.GetState(CharacterState.OnJump) || this.GetState(CharacterState.OnWall)));
        jumpDirection = Vector2.up * this._velocityJump;
        if (jump && this.GetState(CharacterState.OnWall) && !this.GetState(CharacterState.OnGround))
        {
            this._body.AddForce((Vector2.right * -this.Side * this._velocityJump) + Vector2.up * 2, ForceMode2D.Impulse);
            this.Side = -this.Side;
        }
        return jump;
    }

    override protected bool ShouldMove(out Vector2 moveDirection)
    {
        moveDirection = Vector2.zero;
        float horizontal = Input.GetAxis("Horizontal");
        float velocity = this.GetState(CharacterState.OnGround) ? this._velocity : this._velocityOnAir;
        if (horizontal != 0)
        {
            moveDirection = Vector2.right * ((velocity * horizontal) - this._body.velocity.x);
        }
        return horizontal != 0;
    }

    protected override void MovePlayer()
    {
        Vector2 direction;
        if (this.ShouldMove(out direction))
        {
            this._body.AddForce(direction, this.GetState(CharacterState.OnGround) ? ForceMode2D.Impulse : ForceMode2D.Force);
        }

        if (this.ShouldJump(out direction))
        {
            this._body.AddForce(direction, ForceMode2D.Impulse);
            this.AddState(CharacterState.OnJump);
        }
    }
}
