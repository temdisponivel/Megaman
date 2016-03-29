using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{
    public GameObject _looseGround = null;
    
    public GroundState _state = GroundState.Fixed;
    
    public void Update()
    {
        if (this._state == GroundState.Loose)
        {
            GameObject.Instantiate(this._looseGround, this.transform.position, this.transform.rotation);
            GameObject.Destroy(this.gameObject);
        }
    }
}
