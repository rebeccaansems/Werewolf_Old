using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gl_dontdestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
