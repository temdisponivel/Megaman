using UnityEngine;
using System.Collections;

public class FollowPlayer2D : MonoBehaviour
{
    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate,
    }

    public GameObject _target = null;
    public float _velocity = 1.0f;
    [Tooltip("The distance that this camera will mantain to the tager. Zero to center.")]
    public Vector2 _ditance = new Vector2();
    public bool _targetPlayer = false;
    public UpdateType _updateType = UpdateType.Update;

    void Start()
    {
        if (_targetPlayer && _target == null)
        {
            this._target = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        if (this._updateType != UpdateType.Update)
        {
            return;
        }
        this.transform.position = this.GetNextPosition(Time.deltaTime);
    }

    void LateUpdate()
    {
        if (this._updateType != UpdateType.LateUpdate)
        {
            return;
        }
        this.transform.position = this.GetNextPosition(Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (this._updateType != UpdateType.FixedUpdate)
        {
            return;
        }
        this.transform.position = this.GetNextPosition(Time.fixedDeltaTime);
    }

    protected Vector3 GetNextPosition(float deltaTime)
    {
        this.GetTarget();
        if (this._target == null)
        {
            return this.transform.position;
        }
        Vector2 difference = this.transform.position - this.transform.InverseTransformDirection(this._target.transform.position);
        Vector2 newPosition = Vector2.MoveTowards(this.transform.position, (Vector2)this._target.transform.position + this._ditance, (difference - this._ditance).magnitude * this._velocity * deltaTime);
        return new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
    }

    protected void GetTarget()
    {
        if (this._targetPlayer)
        {
            this._target = GameObject.FindWithTag("Player");
        }
    }
}
