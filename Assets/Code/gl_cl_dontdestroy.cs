using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gl_cl_dontdestroy : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
