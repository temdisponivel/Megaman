using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Rigidbody2D _body = null;
	public Animator _animator = null;
	public SpriteRenderer _spriteRenderer = null;
	public float _velocity = 10.0f;
	public float _velocityJump = 10.0f;

	void Start () 
	{
		this._body = this.GetComponent<Rigidbody2D> ();
		this._animator = this.GetComponent<Animator> ();
		this._spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		if (horizontal != 0) 
		{
			if (this._spriteRenderer.flipX && horizontal > 0 || !this._spriteRenderer.flipX && horizontal < 0) 
			{
				this._spriteRenderer.flipX = !this._spriteRenderer.flipX;
			}
		}

		this._animator.SetFloat ("velocity", Mathf.Abs(horizontal * 1.5f));

		float velocity = ((this._velocity * horizontal)- this._body.velocity.x) * 100;
		this._body.AddForce(Vector2.right * velocity * Time.deltaTime, ForceMode2D.Force);

		if (Input.GetButton ("Jump")) {
			if (this._body.velocity.y == 0) {
				this._animator.SetBool ("jumping", true);
				this._body.AddForce (Vector2.up * (this._velocityJump * Time.deltaTime), ForceMode2D.Impulse);
			}
		}
		if (this._body.velocity.y == 0) {
			this._animator.SetBool ("jumping", false);
		}
	}
}