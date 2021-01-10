using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    public FMOD.Studio.EventInstance mouseOver;
    public FMOD.Studio.EventInstance click;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void MousedOver()
    {
        mouseOver = RuntimeManager.CreateInstance("event:/sfx/menu_selection");
        Debug.Log("Mouse Over");
        mouseOver.start();
    }

    public void ClickSound()
    {
        click = RuntimeManager.CreateInstance("event:/sfx/menu_click");
        click.start();
    }
}
