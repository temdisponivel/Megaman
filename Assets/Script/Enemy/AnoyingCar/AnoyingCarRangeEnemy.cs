using UnityEngine;
using System.Collections;

public class AnoyingCarRangeEnemy : RangeEnemy
{
    private bool _onGround = false;

    public override bool ShouldAttack()
    {
        return base.ShouldAttack() && !this._animator.GetBool("destroyed");
    }

    public override void LateUpdate()
    {
        if (!this._animator.GetBool("destroyed") && this._onGround)
        {
            base.LateUpdate();
        }
        else if (this._onGround)
        {
            this.transform.position += Vector3.right * (this._distance/2) * Time.deltaTime * (this._spriteRenderer.flipX ? 1 : -1);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyCar")
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), coll.collider);
        }
        else if (coll.gameObject.tag == "Ground" && !this._onGround)
        {
            this._onGround = true;
            this._initialPosition = this.transform.position;
        }
    }

}
