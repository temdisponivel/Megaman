using UnityEngine;
using System.Collections;

public class RangeEnemy : MonoBehaviour
{
    public GameObject _target = null;
    public GameObject _bullet = null;
    public GameObject _spawnPoint;
    public float _bulletForce = 1f;
    public float _distance = 3f;
    public Animator _animator = null;
    public SpriteRenderer _spriteRenderer = null;
    public SineCurve _curve = null;
    protected Vector2 _initialPosition;
    public float _cooldDown = 1f;
    private float _lastX = 0;
    private float _lastTimeShoot = 0;
    public bool _xAxis = true;

    public void Start()
    {
        this._initialPosition = this.transform.position;
        if (this._target == null)
        {
            this._target = MegamanController.Instance.gameObject;
        }
    }

    public void Update()
    {
        if (this.ShouldAttack())
        {
            GameObject bullet = (GameObject)GameObject.Instantiate(this._bullet, this._spawnPoint.transform.position, this.transform.rotation);
            Vector2 diff = (this._spawnPoint.transform.position - this._target.transform.position);
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
            //bullet.transform.Rotate(Vector3.forward, Vector3.Angle(Vector2.right, (this._target.transform.position - bullet.transform.position)));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * this._bulletForce, ForceMode2D.Impulse);
            this._lastTimeShoot = Time.time;
            this._animator.SetTrigger("shoot");
        }
    }

    virtual public void LateUpdate()
    {
        if (this._curve == null) return;
        bool flipped = this._spriteRenderer.flipX;
        bool flipping = this._spriteRenderer.flipX;
        this.transform.position = this._initialPosition + ((this._xAxis ? Vector2.right : Vector2.up) * this._curve.Value);
        this._spriteRenderer.flipX = (flipping = this.transform.position.x - this._lastX >= 0 && this._xAxis);
        this._lastX = this.transform.position.x;
        if (flipping != flipped)
        {
            this._spawnPoint.transform.localPosition = Vector2.Scale(this._spawnPoint.transform.localPosition, new Vector2() { x = -1, y = 1, });
        }
    }

    virtual public bool ShouldAttack()
    {
        return (Vector3.Distance(this._target.transform.position, this.transform.position) <= this._distance)
            && Time.time - this._lastTimeShoot >= this._cooldDown
            && ((this._target.transform.position.x < this.transform.position.x && !this._spriteRenderer.flipX)
            || (this._target.transform.position.x > this.transform.position.x && this._spriteRenderer.flipX));
    }
}