using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "PlayerBullet")
        {
            this.HP -= collider.gameObject.GetComponent<Bullet>()._damage;
            if (this.HP <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
