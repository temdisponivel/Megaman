using UnityEngine;
using System.Collections;

public class SpykeBall : Character 
{
	public void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "PlayerBullet") 
		{
			Debug.Log ("ALO DANO");
			this.HP -= collider.gameObject.GetComponent<Bullet>()._damage;
			if (this.HP <= 0) 
			{
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}
