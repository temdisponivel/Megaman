using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Character _character = null;
    public GameObject _lifeStack = null;
    public Image[] _lifeBits = null;
    protected float _initialLife = 0f;
    protected float _lifePercent = 0f;
    protected float _lastLife = 0f;
    
    protected virtual void Start()
    {
        //this._lifeBits = this._lifeStack.GetComponentsInChildren<Image>();
        this._initialLife = this._character.HP;
        this._lifePercent = (this._lifeBits.Length / 100.0f) * this._initialLife;
    }

    protected virtual void Update()
    {
        if (this._character.HP != this._lastLife)
        {
            this._lastLife = this._character.HP;
            Image lifeBit = null;
            for (int i = 0; i < this._lifeBits.Length; i++)
            {
                lifeBit = this._lifeBits[i];
                if (i * this._lifePercent >= this._lastLife)
                {
                    lifeBit.gameObject.SetActive(false);
                }
                else
                {
                    lifeBit.gameObject.SetActive(true);
                }
            }
        }
    }
}
