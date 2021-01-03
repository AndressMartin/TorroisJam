using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    public GameObject porta;
    private Caixa scriptDaPorta;
    public bool ativado;

    SpriteRenderer sprite = new SpriteRenderer();
    playerMoveGrid player = new playerMoveGrid();

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

        if (hitbox.gameObject.tag == "Player" || hitbox.gameObject.tag == "Torre")
        {
            scriptDaPorta.ativado = true;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Torre")
        {
            scriptDaPorta.ativado = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scriptDaPorta.ativado = false;
        }

    }


}
