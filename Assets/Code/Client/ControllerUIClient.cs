using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerUIClient : MonoBehaviour
{
    public Canvas CharacterSelect;

    public void Start()
    {
        CharacterSelect.enabled = true;
    }

    public void PressedSubmit()
    {
        CharacterSelect.enabled = false;
    }
}
