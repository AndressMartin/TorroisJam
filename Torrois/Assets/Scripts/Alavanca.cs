using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Alavanca : MonoBehaviour
{

    public bool ativado;
    public int diferencaPlayer;
    public int diferencaCaixa;

    public int sentido; //0=horizontal 1=vertical
    public playerMoveGrid player;

    private Animator animator;
    FMOD.Studio.EventInstance trocar;

    void Start()
    {
        trocar = RuntimeManager.CreateInstance("event:/sfx/alavanca");
        animator = GetComponent<Animator>();
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

        if (collision.gameObject.tag == "Player" && collision.gameObject.tag != "GridTile")
        {

            if (sentido == 0)//alavanca horiznotal
            {

                if (diferencaPlayer == 16)//se player veio na vertical
                {
                    player.Voltar();
                }
                else if (diferencaPlayer == 1)
                {
                    animator.SetTrigger("ativado");
                    trocar.start();
                    ativado = !ativado;
                    player.Voltar();
                }
            }

            if (sentido == 1)
            {
                if (diferencaPlayer == 16)//se player veio na vertical
                {
                    animator.SetTrigger("ativado");
                    trocar.start();
                    ativado = !ativado;
                    player.Voltar();
                }
                else if (diferencaPlayer == 1)
                {
                    player.Voltar();
                }
            }
        }

        if ((collision.gameObject.GetComponent<ChecarMobilidade>().myParent.tag == "Torre" || 
            collision.gameObject.GetComponent<ChecarMobilidade>().myParent.tag == "Rainha" ||
                collision.gameObject.GetComponent<ChecarMobilidade>().myParent.tag == "Peon") &&
                collision.gameObject.tag !="GridTile") 
        {
 

            if (sentido == 0)//alavanca horiznotal
            {
                    if (collision.gameObject.GetComponent<ChecarMobilidade>().myParent.GetComponent<Caixa>().direcoesMov[0]
                    || collision.gameObject.GetComponent<ChecarMobilidade>().myParent.GetComponent<Caixa>().direcoesMov[1])
                {
                    trocar.start();
                    animator.SetTrigger("ativado");
                    ativado = !ativado;
                }
                
                
            }

            if (sentido == 1)//alavanca horiznotal
            {
                if (collision.gameObject.GetComponent<ChecarMobilidade>().myParent.GetComponent<Caixa>().direcoesMov[2]
                || collision.gameObject.GetComponent<ChecarMobilidade>().myParent.GetComponent<Caixa>().direcoesMov[3])
                {
                    trocar.start();
                    animator.SetTrigger("ativado");
                    ativado = !ativado;
                }

            }
        }

        


    }

}
