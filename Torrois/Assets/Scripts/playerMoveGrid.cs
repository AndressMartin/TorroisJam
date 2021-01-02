using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveGrid : MonoBehaviour
{
    private float velocidade = 5f;
    public Transform pontoMov;
    public static int gridAtual;

    public static int gridAnterior = gridAtual;
    int gridTemp;

    private bool gameStarted = true;

    public static bool voltando = false;

    private Vector2 pontoMovAntes;
    private Vector3 pontoMovAntesTemp;

    public int qntQuadradosLocal;
    public bool podeJogar = false;
    public string direcao;


    void Start()
    {
        pontoMov = transform.GetChild(0);
        pontoMov.parent = null;
    }
    
    void FixedUpdate()
    {
        if (!voltando && !podeJogar)
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
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                pontoMovAntes = pontoMov.position;
                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)  
            {
                pontoMovAntes = pontoMov.position;
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
        pontoMov.position = pontoMovAntes;
        pontoMovAntes = pontoMovAntesTemp;
        if (Vector2.Distance(pontoMov.position, pontoMovAntes) == 0f)
            voltando = false;
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
            if (direcao == "esquerda")
            {
                pontoMov.position += new Vector3(-1f, 0f, 0f);
            }
            if (direcao == "direita")
            {
                pontoMov.position += new Vector3(+1f, 0f, 0f);
            }
            if (direcao == "cima")
            {
                pontoMov.position += new Vector3(0f, +1f, 0f);
            }
            if (direcao == "baixo")
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

        if (collision.gameObject.tag == "Imovel")
        {
            Debug.Log("Ai!");
            pontoMovAntes = pontoMov.position;
            if (!voltando)
                voltando = true;
        }
    }
}
