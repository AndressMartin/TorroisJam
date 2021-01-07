using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{

    public bool ativado;
    public int diferencaPlayer;
    public int diferencaCaixa;

    public int sentido; //0=horizontal 1=vertical
    public playerMoveGrid player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoveGrid>();

        if (tag == "AlavancaH")
            sentido = 0;
        else if (tag == "AlavancaV")
            sentido = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }
    void Update()
    {



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        diferencaPlayer = Mathf.Abs(player.direcao);

        if (collision.gameObject.tag == "Player")
        {

            if (sentido == 0)//alavanca horiznotal
            {

                if (diferencaPlayer == 16)//se player veio na vertical
                    player.Voltar();

                else if (diferencaPlayer == 1)
                    ativado = !ativado;
            }

            if (sentido == 1)
            {
                if (diferencaPlayer == 1)
                    player.Voltar();

                else if (diferencaPlayer == 16)
                    ativado = !ativado;
            }
        }

        //if (collision.gameObject.tag == "Torre")
        //{ Debug.Log("Entrou primeiro if");
        //    if (sentido == 0)//alavanca horiznotal
        //    {Debug.Log("entrou segundo if");
        //        if (Caixa.direcoesCaixa[1] == true)//se caixa veio na vertical   
        //            Debug.Log("entrou terceiro if");
        //        //scriptDaPorta.Voltar();
        //        else
        //            scriptDaPorta.ativado = !scriptDaPorta.ativado;

        //    }
        //    //else
        //    //{
        //    //    if (diferencaCaixa == 1)
        //    //        scriptDaPorta.Voltar();
        //    //    else
        //    //        scriptDaPorta.ativado = !scriptDaPorta.ativado;

        //    //}
        //}


    }

}
