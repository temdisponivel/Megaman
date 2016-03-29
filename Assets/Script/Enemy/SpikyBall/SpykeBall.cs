using UnityEngine;
using System.Collections;

public class SpykeBall : Enemy 
{
    public float _distance = 1f;
    public float _velocity = 1f;
    public bool _static = false;
    
    public void Update()
    {
        if (this._static) { return; }
        if (Vector2.Distance(MegamanController.Instance.transform.position, this.transform.position) <= this._distance)
        {
            this.transform.position += -Vector3.right * this._velocity * Time.deltaTime;
        }
    }
}
