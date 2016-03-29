using UnityEngine;
using System.Collections;

public class MetalBug : Enemy
{
    public float _lifeSpan = 5f;
    public float _velocity = 3f;
    protected bool _updating = true;
    private float _startTime = 0f;
    private Vector2 _initialTargetPosition = Vector2.zero;

    public void Start()
    {
        this._startTime = Time.time;
        this._initialTargetPosition = MegamanController.Instance.transform.position;
    }

    virtual public void LateUpdate()
    {
        if (this._updating)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this._initialTargetPosition, this._velocity * Time.deltaTime);
            if (((Vector2)this.transform.position - this._initialTargetPosition).magnitude < .1)
            {
                this._initialTargetPosition = MegamanController.Instance.transform.position;
            }
        }
        else
        {
            this.transform.position = (Vector2)this.transform.position + Vector2.up * this._velocity * Time.deltaTime;
        }

        if (this.transform.position.y > 15f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        if (Time.time - this._startTime >= this._lifeSpan)
        {
            this._updating = false;
        }
    }
}
