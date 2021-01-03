using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{

    private float velocidade = 6f;
    public Transform pontoMov;
    public int gridCaixa;
    public int gridDoJogador;

    public bool colidiuJogador = false;
    public bool podeMover;
    public bool andaMax;

    //0 = esquerda, 1 = direita, 2 = cima, 3 = baixo
    [SerializeField] public List<bool> direcoesMov = new List<bool>(){false, false, false, false};

    ChecarMobilidade pontoMovScript;

    void Start()
    {
        pontoMov = transform.GetChild(0);
        pontoMovScript = gameObject.GetComponentInChildren<ChecarMobilidade>();
        pontoMov.parent = null;

        if (gameObject.tag == "Imovel")
            podeMover = false;
    }


    void FixedUpdate()
    {
        gridDoJogador = playerMoveGrid.gridAtual;
        if (pontoMovScript.ColidiuParede && gameObject.tag != "Imovel")
        {
            //Debug.Log("Caixa pode voltar");
            Voltar();
            pontoMovScript.ColidiuParede = false;
        }
        else
            Move();
    }


    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        if (colidiuJogador)
        {
            if (podeMover == true)
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
                    //colidiuJogador = false;
                }
            }
            colidiuJogador = false; //APENAS SE DER CERTO O COLIDIU NO JOGADOR
            //if (podeMover == false)
            //{
            //    playerMoveGrid.voltando = true;
            //    colidiuJogador = false;
            //}
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
        playerMoveGrid.voltando = true;
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
}
