using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{

    public float velocidade = 5f;
    public Transform pontoMov;
    public int gridCaixa;
    public int gridDoJogador;

    public bool colidiu = false;

    void Start()
    {
        pontoMov.parent = null;
    }


    void Update()
    {
        //if (gridCaixa == gridDoJogador)
        //{
            
        //}
        gridDoJogador = playerMoveGrid.gridAtual;
        Move();
    }

    private void Move()
    { //TODO: Por onde o jogador está vindo?

        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        if (colidiu)
        {
            Debug.Log("Sai do IF");
            if (playerMoveGrid.gridAnterior == gridDoJogador + 1) //Veio da direita
                pontoMov.position += new Vector3(-1f, 0f, 0f); //Caixa pra esquerda
            else if(playerMoveGrid.gridAnterior == gridDoJogador - 1) //Veio da esquerda
                pontoMov.position += new Vector3(+1f, 0f, 0f); //Caixa pra direita
            if (playerMoveGrid.gridAnterior == gridDoJogador - 16) //Veio de cima
                pontoMov.position += new Vector3(0f, -1f, 0f); //Caixa pra baixo
            else if (playerMoveGrid.gridAnterior == gridDoJogador + 16) //Veio de baixo
                pontoMov.position += new Vector3(0f, +1f, 0f); //Caixa pra cima
            colidiu = false;
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
            Debug.Log("Esta colidindo com a caixa");
            colidiu = true;
        }
    }

}
