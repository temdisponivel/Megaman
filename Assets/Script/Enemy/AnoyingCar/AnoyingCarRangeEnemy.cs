using UnityEngine;
using System.Collections;

public class AnoyingCarRangeEnemy : RangeEnemy
{
    public override bool ShouldAttack()
    {
        return base.ShouldAttack() && !this._animator.GetBool("destroyed");
    }

    public override void LateUpdate()
    {
        if (!this._animator.GetBool("destroyed"))
        {
            base.LateUpdate();
        }
        else
        {
            this.transform.position += Vector3.right * this._distance * Time.deltaTime * (this._spriteRenderer.flipX ? 1 : -1);
        }
    }

}
