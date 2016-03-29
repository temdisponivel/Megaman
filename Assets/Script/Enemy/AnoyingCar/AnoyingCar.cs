using UnityEngine;
using System.Collections;

public class AnoyingCar : Enemy
{
    private float _startHP = 0f;
    public Animator _animator = null;
    public Vector2 _lastPosition;

    public void Start()
    {
        this._startHP = this.HP;
        this._lastPosition = this.transform.position;
    }

    public void Update()
    {
        this._lastPosition = this.transform.position;
    }

    override public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "PlayerBullet")
        {
            this.HP -= collider.gameObject.GetComponent<Bullet>()._damage;
            if (this.HP <= this._startHP / 2)
            {
                this._animator.SetBool("destroyed", true);
            }
            if (this.HP <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = (Vector2)collider.gameObject.transform.position + ((Vector2)this.transform.position - this._lastPosition);
            if (!this._animator.GetBool("destroyed") && !collider.gameObject.GetComponent<MegamanController>().GetState(CharacterState.Damage))
            {
                collider.gameObject.GetComponent<MegamanController>().AddState(CharacterState.Damage);
                collider.gameObject.GetComponent<MegamanCharacter>().HP--;
            }
        }
    }
}
