using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Shake : MonoBehaviour
{
    public bool perdeuTimer;
    public static Animator camAnim;

    FMOD.Studio.EventInstance destruicao;
    public bool tocar;

    // Start is called before the first frame update
    void Start()
    {
        destruicao = RuntimeManager.CreateInstance("event:/sfx/engrenagens");
        camAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cooldown.timerTime == 0)
        {
            if (!tocar)
            {
                destruicao.start();
                tocar = true;
            }
            if (!perdeuTimer)
            {
                CamShake();
                trazerCameraJunto();
                StartCoroutine(stopCamShake());
            }
        }
        if (Cooldown.timerTime > 0 && perdeuTimer)
        {
            perdeuTimer = false;
            tocar = false;
        }
    }

    public static void CamShake()
    {
        camAnim.SetBool("shake", true);
    }

    public void trazerCameraJunto()
    {
        transform.parent.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z);
    }

    IEnumerator stopCamShake()
    {
        Debug.Log("Começou");
        yield return new WaitForSeconds(2);

        camAnim.SetBool("shake", false);
        perdeuTimer = true;
        transform.parent.position = new Vector3(0f, Mathf.Round(transform.parent.position.y), Mathf.Round(transform.parent.position.z));
    }
}
