using UnityEngine;
using System.Collections;

public class MegamanCharacter : Character
{
    public Animator _animator = null;

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.HP--;
            this._animator.SetTrigger("damage");
        }
    }
}
