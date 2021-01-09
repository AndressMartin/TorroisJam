using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject Panel;
    public GameObject PanelCredits;
    public Image[] imagens;
    public bool fadingout = false;
    private int indice = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (indice < 3)
                CallFadeOut();
            else if (indice == 3)
                CallStats();
            else if (indice == 4)
                CallCredits();
            else if (indice == 5)
                SceneManager.LoadScene(0);

        }
        if (fadingout)
            FadeOut(imagens[indice]);
    }

    public void CallFadeOut()
    {
        fadingout = true;
    }
    public void StopFadeOut()
    {
        fadingout = false;
    }
    public void FadeOut(Image thisImage)
    {
        thisImage.CrossFadeAlpha(0, .2f, true);
        if (thisImage.canvasRenderer.GetAlpha() <= 0.1f)
        {
            thisImage.canvasRenderer.SetAlpha(0f);
            indice++;
            StopFadeOut();
        }
    }
    public void CallStats()
    {
        Panel.SetActive(true);
        gameObject.GetComponent<SendStatsPanel>().sendToPanelTexts();
        indice++;
    }

    public void CallCredits()
    {
        imagens[3].canvasRenderer.SetAlpha(0f);
        Panel.SetActive(false);
        PanelCredits.SetActive(true);
        indice++;
    }
}
