using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class MenuScript : MonoBehaviour
{
    FMOD.Studio.EventInstance mouseOver;

    private void Start()
    {
        mouseOver = RuntimeManager.CreateInstance("event:/sfx/menu_selection");
    }

    private void OnMouseOver()
    {
        mouseOver.start();
    }
}
