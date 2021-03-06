﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image thisImage;
    public GameObject player;
    public bool fadingOut;
    public bool fadingIn;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thisImage.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Rewinder>().isRewinding)
        {
            fadingOut = true;
        }
        if (fadingOut)
            fadeOut();
        else if (fadingIn)
            fadeIn();
        else
            thisImage.canvasRenderer.SetAlpha(0f);

    }

    public void fadeOut()
    {
        thisImage.CrossFadeAlpha(1, .2f, true);
        Debug.Log("Alpha: " + thisImage.canvasRenderer.GetAlpha());
        if (thisImage.canvasRenderer.GetAlpha() >= 0.9f)
        {
            thisImage.canvasRenderer.SetAlpha(1f);
            fadingOut = false;
            fadingIn = true;
        }
    }

    public void fadeIn()
    {
        thisImage.CrossFadeAlpha(0, .2f, true);
        if (thisImage.canvasRenderer.GetAlpha() <= 0.1f)
        {
            thisImage.canvasRenderer.SetAlpha(0f);
            fadingIn = false;
        }
    }
}
