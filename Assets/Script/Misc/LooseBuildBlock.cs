using UnityEngine;
using System.Collections;

public class LooseBuildBlock : MonoBehaviour
{
    public float _secondsToFall = 1f;
    public Rigidbody2D _body = null;
    public float _startTime = 0f;

    public void Start()
    {
        this._startTime = Time.time;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.StartFall();
        }
    }

    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(this._secondsToFall);
        this._body.gravityScale = 1;
        this._body.isKinematic = false;
    }

    public void StartFall()
    {
        if (Time.time - this._startTime >= .1)
        {
            this.StartCoroutine(this.Fall());
        }
    }
}
