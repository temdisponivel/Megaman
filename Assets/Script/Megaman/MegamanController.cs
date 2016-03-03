using UnityEngine;
using System.Collections;

public class MegamanController : CharacterController2D
{
    override protected bool ShouldJump(out Vector2 jumpDirection)
    {
        jumpDirection = Vector2.up * this._velocityJump;
        if (this.GetState(CharacterState.OnWall) && !this.GetState(CharacterState.OnGround))
        {
            jumpDirection += (Vector2.right * 2 * (this._wallOnRight ? -1 : 1)) * this._velocityJump * 2;
        }
        return (Input.GetButton("Jump") && (!this.GetState(CharacterState.OnJump) || this.GetState(CharacterState.OnWall)));
    }

    override protected bool ShouldMove(out Vector2 moveDirection)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float velocity = this.GetState(CharacterState.OnGround) ? this._velocity : this._velocityOnAir;
        moveDirection = Vector2.right * ((velocity * horizontal) - this._body.velocity.x);
        return true;
    }
}
