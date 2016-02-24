using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	protected Rigidbody2D _body = null;
    protected Animator _animator = null;
    protected SpriteRenderer _spriteRenderer = null;
	public float _velocity = 10.0f;
	public float _velocityJump = 10.0f;
	private bool _jump = false;

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

		this._animator.SetFloat ("velocity", Mathf.Abs(horizontal));

		if (Input.GetButton ("Jump")) {
			if (this._body.velocity.y == 0) {
				this._jump = true;
			}
		}
		if (this._body.velocity.y == 0) {
			this._animator.SetBool ("jumping", false);
		}
	}

	void FixedUpdate()
	{
        Vector2 inpulseDirection = Vector2.right * ((this._velocity * Input.GetAxis("Horizontal")) - this._body.velocity.x);
        this._body.AddForce(inpulseDirection, ForceMode2D.Impulse);
		if (this._jump) 
		{
			this._body.AddForce (Vector2.up * (this._velocityJump), ForceMode2D.Impulse);
			this._animator.SetBool ("jumping", true);
			this._jump = false;
		}
	}
}