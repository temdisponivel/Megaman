using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Rigidbody2D _body = null;
	public float _velocity = 10.0f;
	public float _velocityJump = 10.0f;

	void Start () 
	{
		this._body = this.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float velocity = ((this._velocity * horizontal)- this._body.velocity.x) * 100;
		this._body.AddForce(Vector2.right * velocity * Time.deltaTime, ForceMode2D.Force);

		if (Input.GetButton ("Jump")) 
		{
			if (this._body.velocity.y == 0) 
			{
				this._body.AddForce (Vector2.up * (this._velocityJump * Time.deltaTime), ForceMode2D.Impulse);
			}
		}
	}
}