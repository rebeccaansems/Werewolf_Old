using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gl_cl_dontdestroy : MonoBehaviour
{
    private static gl_cl_dontdestroy instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            UnityEngine.SceneManagement.SceneManager.LoadScene("ControllerCharacterSelection");
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
