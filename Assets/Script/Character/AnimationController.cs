using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public CharacterController2D _character = null;

    protected Animator _animator = null;
    protected SpriteRenderer _spriteRenderer = null;
    protected Rigidbody2D _body = null;

    protected void Start()
    {
        if (this._character == null)
        {
            this._character = this.GetComponent<CharacterController2D>();
        }
        this._body = this._character.GetComponent<Rigidbody2D>();
        this._animator = this._character.GetComponent<Animator>();
        this._spriteRenderer = this._character.GetComponent<SpriteRenderer>();
        this._character.OnStateChange += this.OnCharacterStateChange;
    }

    virtual protected void Update()
    {
        this.HandleAnimation();
    }

    virtual protected void HandleAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");

        this._animator.SetFloat("velocity", Mathf.Abs(horizontal) * (this._character.GetState(CharacterState.OnGround) && !this._character.GetState(CharacterState.OnWall) ? 1 : 0));
        this._animator.SetFloat("y_velocity", this._body.velocity.y);

        if (this._spriteRenderer.flipX && this._character.Side > 0 || !this._spriteRenderer.flipX && this._character.Side < 0)
        {
            this._spriteRenderer.flipX = !this._spriteRenderer.flipX;
        }
    }

    protected void OnCharacterStateChange(CharacterState state, bool included)
    {
        switch (state)
        {
            case CharacterState.Initialized:
                this._animator.SetBool("any_state_enable", true);
                break;
            case CharacterState.OnJump:
                this._animator.SetBool("jump", included);
                break;
            case CharacterState.OnWall:
                if (!included || !this._character.GetState(CharacterState.OnGround))
                {
                    this._animator.SetBool("slide", included);
                    break;
                }
                break;
            case CharacterState.Shooting:
                this._animator.SetFloat("shoot", included ? 1 : 0);
                break;
            case CharacterState.OnGround:
                this._animator.SetBool("slide", false);
                break;
            default:
                break;
        }
    }
}
