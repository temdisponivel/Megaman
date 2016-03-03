using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public GameObject _bullet = null;

    virtual protected void Update()
    {
        Vector2 position, direction;
        Quaternion rotation;
        this.ShouldShoot(out position, out direction, out rotation);
         GameObject.Instantiate(this._bullet, position, rotation);
    }

    virtual protected bool ShouldShoot(out Vector2 position, out Vector2 direction, out Quaternion rotation)
    {
        position = this.transform.position;
        rotation = this.transform.rotation;
        direction = Vector2.right * (Input.GetAxis("Horizontal") >= 0 ? 1 : -1);
        return true;
    }
}
