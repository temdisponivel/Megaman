using UnityEngine;
using System.Collections;

public class RangeEnemy : MonoBehaviour
{
    public GameObject _target = null;
    public GameObject _bullet = null;
    public GameObject _weapon = null;
    public float _bulletForce = 1f;
    public float _distance = 3f;
    public Animator _animator = null;
    public SpriteRenderer _spriteRenderer = null;
    public SineCurve _curve = null;
    private Vector2 _initialPosition;
    public float _cooldDown = 1f;
    private float _lastX = 0;
    private float _lastTimeShoot = 0;

    public void Start()
    {
        this._initialPosition = this.transform.position;
    }

    public void Update()
    {
        if (this.ShouldAttack())
        {
            GameObject bullet = (GameObject)GameObject.Instantiate(this._bullet, this.transform.position, this._weapon.transform.rotation);
            this._lastTimeShoot = Time.time;
        }
    }

    public void LateUpdate()
    {
        this.transform.position = this._initialPosition + (Vector2.right * this._curve.Value);
        this._spriteRenderer.flipX = this.transform.position.x - this._lastX >= 0;
        this._lastX = this.transform.position.x;
    }

    public bool ShouldAttack()
    {
        return (Vector3.Distance(this._target.transform.position, this.transform.position) <= this._distance)
            && Time.time - this._lastTimeShoot >= this._cooldDown
            && (this._target.transform.position.x < this.transform.position.x || this._spriteRenderer.flipX);
    }
}