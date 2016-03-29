using UnityEngine;
using System.Collections;

public class DestroyerSpyke : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ALO");
        if (collision.gameObject.tag == "Ground")
        {
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