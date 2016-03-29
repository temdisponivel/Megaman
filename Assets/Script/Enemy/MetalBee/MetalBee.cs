using UnityEngine;
using System.Collections;

public class MetalBee : MetalBug
{
    public float _chances = .1f;
    public GameObject _life = null;

    private bool _spawned = false;

    public override void LateUpdate()
    {
        base.LateUpdate();
        if (!this._spawned && !this._updating)
        {
            this._spawned = true;
            if (Random.value <= this._chances)
            {
                GameObject.Instantiate(this._life, this.transform.position, this.transform.rotation);
            }
        }
    }
}
