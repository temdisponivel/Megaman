using UnityEngine;
using System.Collections;

public class SpykeBall : Enemy 
{
    public float _velocity = 1f;
    public bool _static = false;
    
    public void Update()
    {
        if (this._static) { return; }
        this.transform.position += -Vector3.right * this._velocity * Time.deltaTime;
    }
}
