using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour
{
    public Transform _pivot = null;
    public float _dump = .5f;
    public float _epsilon = .5f;
    protected Vector3 _lastPosition = default(Vector3);

    public void Start()
    {
        this._lastPosition = this._pivot.position;
    }

    public void FixedUpdate()
    {
        Vector2 delta = this._pivot.position - this._lastPosition;
        Vector2 direction = Vector2.zero;
        if ((delta.x < this._epsilon && delta.x > -this._epsilon) && (delta.y > -this._epsilon && delta.y < this._epsilon))
        {
            return;
        }
        if (delta.x < -this._epsilon)
        {
            direction = -Vector2.right * delta.magnitude;
        }
        else if (delta.x > this._epsilon)
        {
            direction = Vector2.right * delta.magnitude;
        }
        if (delta.y < -this._epsilon)
        {
            direction += -Vector2.up * delta.magnitude;
        }
        else if (delta.y > this._epsilon)
        {
            direction += Vector2.up * delta.magnitude;
        }
        this.transform.position = (Vector2)this.transform.position + direction * this._dump;
        this._lastPosition = this._pivot.position;
    }
}
