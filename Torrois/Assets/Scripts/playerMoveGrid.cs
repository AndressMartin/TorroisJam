﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveGrid : MonoBehaviour
{
    public float velocidade = 5f;
    public Transform pontoMov;
    public static int gridAtual;

    public static int gridAnterior = gridAtual;
    int gridTemp;

    private bool gameStarted = true;

    public static bool voltando = false;

    private Vector2 pontoMovPosAntes;
    private Vector3 pontoMovAntesTemp;
    public static bool colidiuCaixaImovel;

    void Start()
    {
        pontoMov.parent = null;
    }
    
    void FixedUpdate()
    {
        if (!voltando)
            Move();
        else
            Voltar();
        Debug.Log("Anterior = " + gridAnterior + "Atual = " + gridAtual);
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
            Debug.Log("Colidiu com " + gridAtual);
        }

        if (collision.gameObject.tag == "Imovel")
        {
            if (!voltando)
                voltando = true;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);
        pontoMovAntesTemp = pontoMovPosAntes;
        if (Vector2.Distance(transform.position, pontoMov.position) == 0f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                pontoMovPosAntes = pontoMov.position;
                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)  
            {
                pontoMovPosAntes = pontoMov.position;
                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }

    private void Voltar()
    {
        Debug.Log("Jogador Voltando");

        gridAtual = gridAnterior;
        gridAnterior = gridTemp;
        pontoMov.position = pontoMovPosAntes;
        pontoMovPosAntes = pontoMovAntesTemp;
        if (Vector2.Distance(pontoMov.position, pontoMovPosAntes) == 0f)
            voltando = false;
        if (colidiuCaixaImovel)
        {
            pontoMovPosAntes = pontoMovAntesTemp;
            colidiuCaixaImovel = false;
        }
    }

    public static void Jogado(int qntdQuadrados)
    {
        //for (int quadrado = 0; quadrado < qntdQuadrados; quadrado++)
        //{
        //    pontoMovPosAntes = pontoMov.position;
        //    gridAnterior = gridAtual;

        //    pontoMov.position += new Vector3(1f, 0f, 0f);
        //}
    }
}
