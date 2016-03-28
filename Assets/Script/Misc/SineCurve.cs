using UnityEngine;
using System.Collections;

public class SineCurve : MonoBehaviour
{
    public float _multiplier = 0f;
    public float _frequency = 1f;
    public float Value = 0;

    public void Update()
    {
        this.Value = this._multiplier * Mathf.Sin(2 * Mathf.PI * this._frequency * Time.time + 90);
    }
}