using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Botao : MonoBehaviour
{

    public bool ativado;
    FMOD.Studio.EventInstance apertar;
    // Start is called before the first frame update
    void Start()
    { 
        apertar = RuntimeManager.CreateInstance("event:/sfx/apertar_botao");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "GridTile" && collision.gameObject.tag != "Untagged")
        {
            ativado = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "GridTile" && collision.gameObject.tag != "Untagged")
        {
            ativado = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "GridTile" && collision.gameObject.tag != "Untagged")
        {
            apertar.start();
        }
    }

}
