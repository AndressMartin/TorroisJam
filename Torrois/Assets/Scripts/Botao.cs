using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public GameObject porta;
    private Caixa scriptDaPorta;
    public bool ativado;

    public BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {

        scriptDaPorta = porta.GetComponent<Caixa>();

    }

    // Update is called once per frame
    void Update()
    {
        ativado = scriptDaPorta.ativado;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "GridTile")
        {
            scriptDaPorta.ativado = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "GridTile")
        {
            scriptDaPorta.ativado = true;
        }
    }

}
