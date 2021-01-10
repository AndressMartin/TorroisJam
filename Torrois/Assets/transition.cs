using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class transition : MonoBehaviour
{
    public Image thisImage;
    public bool fadingOut = true;
    public bool fadingIn;

    void Start()
    {
        fadingOut = true;
        thisImage.canvasRenderer.SetAlpha(0f);
    }

    void Update()
    {
        if (fadingOut)
            fadeOut();
        else if (fadingIn)
            fadeIn();

    }

    public void fadeOut()
    {
        thisImage.CrossFadeAlpha(1, 1f, false);
        if (thisImage.canvasRenderer.GetAlpha() >= 0.9f)
        {
            thisImage.canvasRenderer.SetAlpha(1f);
            fadingOut = false;
            fadingIn = true;
        }
    }

    public void fadeIn()
    {
        thisImage.CrossFadeAlpha(0, 1f, false);
        if (thisImage.canvasRenderer.GetAlpha() <= 0.1f)
        {
            thisImage.canvasRenderer.SetAlpha(0f);
            fadingIn = false;
            SceneManager.LoadScene(1);
        }
    }
}
