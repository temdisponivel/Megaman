using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MegamanCharacter : Character
{
    public void Update()
    {
        if (this.HP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        if (this.transform.position.y < -0.5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
