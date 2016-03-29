using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _toSpawn = null;
    public float _interval = 1f;
    public int _quantity = 3;

    public IEnumerator Spawn()
    {
        for (int i = 0; i < this._quantity; i++)
        {
            GameObject.Instantiate(this._toSpawn, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(this._interval);
        }
        //GameObject.Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
            this.StartCoroutine(this.Spawn());
        }
    }
}
