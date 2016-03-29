using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    public Animator _animator = null;
    public float _coolDown = 0f;
    public float _distance = 0f;
    private float _lastTimeHit = 0;
    private bool _activate = false;

    public void Update()
    {
        if (Vector2.Distance(this.transform.position, MegamanController.Instance.transform.position) <= this._distance)
        {
            this._activate = true;
        }

        if (this._activate && Time.time - this._lastTimeHit >= this._coolDown)
        {
            this._animator.SetTrigger("hit");
            this._lastTimeHit = Time.time;
        }
    }
}
