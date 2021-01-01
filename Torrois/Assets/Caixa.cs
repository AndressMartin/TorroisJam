using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    private float velocidade = 6f;
    public Transform pontoMov;
    public int gridCaixa;
    public int gridDoJogador;

    public bool colidiu = false;
    public bool podeMover;
    public bool andaMax;

    //private BoxCollider2D boxCollider;
    private BoxCollider2D boxMoveTriggerer;

    void Start()
    {
        boxMoveTriggerer = GetComponent<BoxCollider2D>();
        pontoMov.parent = null;
        if (gameObject.tag == "Imovel")
            podeMover = false;
    }


    void FixedUpdate()
    {
        gridDoJogador = playerMoveGrid.gridAtual;
        Move();
    }

    private void Move()
    {

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        if (colidiu)
        {
            if (podeMover == true)
            {
                if (andaMax == false)
                {
                    if (playerMoveGrid.gridAnterior == gridDoJogador + 1) //Veio da direita
                        pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
                    else if (playerMoveGrid.gridAnterior == gridDoJogador - 1) //Veio da esquerda
                        pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
                    if (playerMoveGrid.gridAnterior == gridDoJogador - 16) //Veio de cima
                        pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
                    else if (playerMoveGrid.gridAnterior == gridDoJogador + 16) //Veio de baixo
                        pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
                    colidiu = false;
                }
                else
                {
                    //Debug.Log("TODO: Limite da Sala.");
                }
                
            }
            if (podeMover == false)
            {
                playerMoveGrid.voltando = true;
                colidiu = false;
            }
        }
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
            colidiu = true;
        }
    }
}
