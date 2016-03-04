using UnityEngine;
using System.Collections;

public class MegamanController : CharacterController2D
{
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

    override protected bool ShouldJump(out Vector2 jumpDirection)
    {
        bool jump = (Input.GetButtonDown("Jump") && (!this.GetState(CharacterState.OnJump) || this.GetState(CharacterState.OnWall)));
        jumpDirection = Vector2.up * this._velocityJump;
        if (jump && this.GetState(CharacterState.OnWall) && !this.GetState(CharacterState.OnGround))
        {
            jumpDirection = ((this._wallOnRight ? Vector2.left : Vector2.right) * 2) + (Vector2.up * 10);
            this.Side = this._wallOnRight ? -1 : 1;
        }
        return jump;
    }
}
