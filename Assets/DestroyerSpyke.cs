using UnityEngine;
using System.Collections;

public class DestroyerSpyke : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log(collision.gameObject);
            Debug.Log(collision.gameObject.transform.position);
            Ground aux = null;
            if ((aux = collision.gameObject.GetComponent<Ground>()) != null)
            {
                aux._state = GroundState.Loose;
            }
            else
            {
                LooseBuildBlock auxBuild = collision.gameObject.GetComponent<LooseBuildBlock>();
                if (auxBuild != null)
                {
                    auxBuild.StartFall();
                }
            }
        }
    }
}