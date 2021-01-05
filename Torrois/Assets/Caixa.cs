using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Caixa : MonoBehaviour
{
    public bool ativado;
    private float velocidade = 6f;
    public Transform pontoMov;
    public int gridCaixa;
    public int gridDoJogador;

    public bool colidiuJogador;
    public bool colidiuCaixa;
    public bool podeMover;
    public bool andaMax;
    public bool andandoComoRainha;

    //0 = esquerda, 1 = direita, 2 = cima, 3 = baixo
    [SerializeField] public List<bool> direcoesMov = new List<bool>(){false, false, false, false};
    [SerializeField] public List<bool> direcoesMovCxColl = new List<bool>() { false, false, false, false };

    private SpriteRenderer sprite;
    private BoxCollider2D boxTriggerer;

    ChecarMobilidade pontoMovScript;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxTriggerer = GetComponent<BoxCollider2D>();
        pontoMov = transform.GetChild(0);
        if (gameObject.tag == "Imovel")
        {
            podeMover = false;
            //Destroy(pontoMov.gameObject);
        }
        if (pontoMov != null)
        {
            pontoMovScript = gameObject.GetComponentInChildren<ChecarMobilidade>();
            pontoMov.GetComponent<ChecarMobilidade>().myParent = transform;
            pontoMov.parent = GameObject.FindGameObjectWithTag("HolderTemporario").transform;
        }    
        if (gameObject.tag == "Torre")
        {
            andaMax = false;
        }
        if (gameObject.tag == "Rainha")
        {
            andaMax = true;
        }
    }


    void FixedUpdate()
    {
        if (pontoMov != null)
        {
            verPorta();
            gridDoJogador = playerMoveGrid.gridAtual;
            if ((pontoMovScript.ColidiuParede && gameObject.tag != "Imovel") && !colidiuCaixa)
            {
                //Debug.Log("Caixa pode voltar");
                Voltar();
                pontoMovScript.ColidiuParede = false;
            }
            else
                Move();
        }
        
    }


    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        if (colidiuJogador || andandoComoRainha || colidiuCaixa)
        {
            if (podeMover == true)
            {
                if (colidiuJogador)
                {
                    if (andaMax == false)
                    {
                        if (playerMoveGrid.gridAnterior == gridDoJogador + 1) //Veio da direita
                        {
                            pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (playerMoveGrid.gridAnterior == gridDoJogador - 1) //Veio da esquerda
                        {
                            pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (playerMoveGrid.gridAnterior == gridDoJogador - 16) //Veio de cima
                        {
                            pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (playerMoveGrid.gridAnterior == gridDoJogador + 16) //Veio de baixo
                        {
                            pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                    }
                    if (andaMax == true)
                    {
                        andandoComoRainha = true;
                        if (playerMoveGrid.gridAnterior == gridDoJogador + 1) //Veio da direita
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (playerMoveGrid.gridAnterior == gridDoJogador - 1) //Veio da esquerda
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (playerMoveGrid.gridAnterior == gridDoJogador - 16) //Veio de cima
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (playerMoveGrid.gridAnterior == gridDoJogador + 16) //Veio de baixo
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                        if (pontoMovScript.ColidiuParede)
                            andandoComoRainha = false;
                    }
                }
                if (colidiuCaixa) //SE FALHAR, TESTAR ELSE IF
                {
                    if (andaMax == false)
                    {
                        if (direcoesMovCxColl[0] == true) //Veio da direita
                        {
                            pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (direcoesMovCxColl[1] == true) //Veio da esquerda
                        {
                            pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (direcoesMovCxColl[3] == true) //Veio de cima
                        {
                            pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (direcoesMovCxColl[2] == true) //Veio de baixo
                        {
                            pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                    }
                    if (andaMax == true) //TODO: PROVAVELMENTE NAO FUNCIONA! PRECISA CONFERIR! 
                    {
                        andandoComoRainha = true;
                        if (direcoesMovCxColl[0] == true) //Veio da direita
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                            direcoesMov[0] = true;
                        }
                        else if (direcoesMovCxColl[1] == true) //Veio da esquerda
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                            direcoesMov[1] = true;
                        }
                        if (direcoesMovCxColl[3] == true) //Veio de cima
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                            direcoesMov[3] = true;
                        }
                        else if (direcoesMovCxColl[2] == true) //Veio de baixo
                        {
                            if (!pontoMovScript.ColidiuParede)
                                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                            direcoesMov[2] = true;
                        }
                        if (pontoMovScript.ColidiuParede)
                            andandoComoRainha = false;
                    }
                }
            }
            if (!andandoComoRainha)
                colidiuJogador = false;
            if (!andandoComoRainha)
                colidiuCaixa = false;
        }
        if (Vector2.Distance(transform.position, pontoMov.position) == 0f) //PODE BUGAR SE COLISAO FOR MUITO RAPIDO
        {
            //Debug.Log("Direcoes iguais");
            direcoesMov[0] = false;
            direcoesMov[1] = false;
            direcoesMov[2] = false;
            direcoesMov[3] = false; 
        }
    }
    private void Voltar()
    {
        
        //Debug.Log("tentando voltar");
        if (direcoesMov[0] == true)
        {
            //Debug.Log("tentando voltar " + "esquerda");
            pontoMov.position += new Vector3(+1f, 0f, 0f);
        }
        if (direcoesMov[1] == true)
        {
            //Debug.Log("tentando voltar " + "direita");
            pontoMov.position += new Vector3(-1f, 0f, 0f);
        }
        if (direcoesMov[3] == true)
        {
            //Debug.Log("tentando voltar " + "cima");
            pontoMov.position += new Vector3(0f, +1f, 0f);
        }
        if (direcoesMov[2] == true)
        {
            //Debug.Log("tentando voltar " + "baixo");
            pontoMov.position += new Vector3(0f, -1f, 0f);
        }
        if (Vector3.Distance(playerMoveGrid.pontoMov.position, pontoMov.position) == 0)
        {
            playerMoveGrid.voltando = true;
        }
        if (gameObject.tag == "Rainha")
            colidiuJogador = false;
        andandoComoRainha = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GridTile")
        {
            GridIndice ThisIndice = collision.GetComponent<GridIndice>();
            gridCaixa = ThisIndice.thisIndice;
        }

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Esta colidindo com a caixa");
            colidiuJogador = true;
        }
    }

    public void verPorta()
    {
        if (ativado)
        { 
            boxTriggerer.isTrigger = false;
            

        }
        else
        { 
            boxTriggerer.isTrigger = true;
           

        }
        sprite.enabled = boxTriggerer.isTrigger;
        boxTriggerer.enabled = sprite.enabled;
        
    }

    public void sendParent()
    {
        pontoMov.GetComponent<ChecarMobilidade>().myParent = transform;
    }
}

