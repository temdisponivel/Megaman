using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{
    public int _seconds = 2;
    public int _life = 0;

    public void Start()
    {
        this.StartCoroutine(this.Fade());
    }

    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(this._seconds);
        GameObject.Destroy(this.gameObject);
    }
}
