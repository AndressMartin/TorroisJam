using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class playerMoveGrid : MonoBehaviour
{
    private float velocidade = 5f;
    public static Transform pontoMov;
    ChecarMobilidade pontoMovScript;

    public static int gridAtual;
    public static int gridAnterior = gridAtual;
    int gridTemp;

    private bool gameStarted = true;

    public static bool voltando = false;

    private Vector2 pontoMovAntes;
    private Vector3 pontoMovAntesTemp;

    //Para o código de jogar longe
    public int qntQuadradosLocal;
    public bool podeJogar = false;
    public string direcaoTorreJoga;
    public int direcao;

    private GameObject childSpriteHolder;
    private Animator playerAnimator;

    public bool transitandoEntreFases;

    void Start()
    {
        childSpriteHolder = transform.GetChild(1).gameObject;
        playerAnimator = childSpriteHolder.gameObject.GetComponent<Animator>();
        pontoMov = transform.GetChild(0);
        pontoMovScript = gameObject.GetComponentInChildren<ChecarMobilidade>();
        pontoMov.parent = GameObject.FindGameObjectWithTag("HolderTemporario").transform;
        
    }
    
    void FixedUpdate()
    {
        
        if (!voltando && !podeJogar && !gameObject.GetComponent<Rewinder>().isRewinding)
            Move();
        if (voltando)
        {
            Voltar();
        }
        if (podeJogar)
            Jogado();
        //Debug.Log("Anterior = " + gridAnterior + "Atual = " + gridAtual);

    }

    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        pontoMovAntesTemp = pontoMovAntes;   
        if (Vector2.Distance(transform.position, pontoMov.position) == 0f)
        {
            if (gameObject.GetComponent<StudioEventEmitter>().CollisionTag == "Torre")  //GAMBIARRA!!!!
            {
                gameObject.GetComponent<StudioEventEmitter>().CollisionTag = "Imovel";
            }
            transitandoEntreFases = false; //Código da CameraMov
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                Virar();
                direcao = (int)Input.GetAxisRaw("Horizontal");
                pontoMovAntes = pontoMov.position;
                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                playerAnimator.SetTrigger("Andando");
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)  
            {
                direcao = (int)Input.GetAxisRaw("Vertical")*16;
                pontoMovAntes = pontoMov.position;
                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                playerAnimator.SetTrigger("Andando");
            }
        }
        if (pontoMovScript.ColidiuParede)
        {
            playerAnimator.SetTrigger("Empurrando");
            pontoMovScript.ColidiuParede = false;
        }
    }

    private void Virar()
    {
        if ((Input.GetAxisRaw("Horizontal")) == -1f)
        {
            childSpriteHolder.GetComponent<SpriteRenderer>().flipX = true;
        }
        if ((Input.GetAxisRaw("Horizontal")) == +1f)
        {
            childSpriteHolder.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void Voltar()
    {
        Debug.Log("Jogador Voltando");
        pontoMov.position = pontoMovAntes;
        pontoMovAntes = pontoMovAntesTemp;
        if (Vector2.Distance(pontoMov.position, pontoMovAntes) == 0f)
        {

            gridAtual = gridAnterior;
            gridAnterior = gridTemp;

            voltando = false;
        }
    }

    //public void Jogados()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
    //    pontoMovAntesTemp = pontoMovAntes;
    //    if (Vector2.Distance(transform.position, pontoMov.position) == 0f)
    //    {
    //        pontoMovAntes = pontoMov.position;
    //        gridAnterior = gridAtual;
    //        if (direcao == "esquerda")
    //        {
    //            pontoMov.position += new Vector3(-1f, 0f, 0f);
    //        }
    //        if (direcao == "direita")
    //        {
    //            pontoMov.position += new Vector3(+1f, 0f, 0f);
    //        }
    //        if (direcao == "cima")
    //        {
    //            pontoMov.position += new Vector3(0f, +1f, 0f);
    //        }
    //        if (direcao == "baixo")
    //        {
    //            pontoMov.position += new Vector3(0f, -1f, 0f);
    //        }
    //    }

    //}
    //public void Jogado()
    //{
    //    //pontoMovAntes = 
    //    gridAtual = gridAnterior;
    //    pontoMovAntes = pontoMov.position;
    //    for (int quadrado = 0; quadrado < qntQuadradosLocal; quadrado++)
    //    {
    //        gridAnterior = gridAtual;
    //        if (direcao == "esquerda")
    //        {
    //            pontoMov.position += new Vector3(-1f, 0f, 0f);
    //        }
    //        if (direcao == "direita")
    //        {
    //            pontoMov.position += new Vector3(+1f, 0f, 0f);
    //        }
    //        if (direcao == "cima")
    //        {
    //            pontoMov.position += new Vector3(0f, +1f, 0f);
    //        }
    //        if (direcao == "baixo")
    //        {
    //            pontoMov.position += new Vector3(0f, -1f, 0f);
    //        }
    //        while (Vector2.Distance(transform.position, pontoMov.position) != 0f)
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
    //        }
    //    }
    //    podeJogar = false;
    //}
    public void Jogado()
    {
        pontoMovAntes = pontoMov.position;
        gridAnterior = gridAtual;
        pontoMovAntesTemp = pontoMovAntes;
        for (int quadrado = 0; quadrado < qntQuadradosLocal; quadrado++)
        {
            gridAtual = gridAnterior;
            pontoMovAntesTemp = pontoMovAntes;
            if (direcaoTorreJoga == "esquerda")
            {
                pontoMov.position += new Vector3(-1f, 0f, 0f);
            }
            if (direcaoTorreJoga == "direita")
            {
                pontoMov.position += new Vector3(+1f, 0f, 0f);
            }
            if (direcaoTorreJoga == "cima")
            {
                pontoMov.position += new Vector3(0f, +1f, 0f);
            }
            if (direcaoTorreJoga == "baixo")
            {
                pontoMov.position += new Vector3(0f, -1f, 0f);
            }
        }
        podeJogar = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GridTile")
        {
            gridTemp = gridAnterior;
            GridIndice ThisIndice = collision.GetComponent<GridIndice>();
            gridAtual = ThisIndice.thisIndice;
            if (gameStarted)
            {
                gridAnterior = gridAtual;
                gameStarted = false;
            }
            //Debug.Log("Colidiu com " + gridAtual);
        }
        if (!transitandoEntreFases)
        {
            if (collision.gameObject.tag == "Imovel")
            {
                //Debug.Log("Ai!");
                pontoMovAntes = pontoMov.position;
                if (!voltando)
                    voltando = true;
            }
        }

        //TENTANDO RESOLVER Jogador entrando na caixa rapidamente dps da caixa colidir com a outra

        //if (collision.gameObject.tag == "Torre" && collision.GetComponent<Caixa>().colidiuCaixa == true) 
        //{
        //    Debug.Log("Player entrando na caixa");
        //    Voltar();
        //}

    }

    public void PegarInput(Input input)
    {

    } 
}
