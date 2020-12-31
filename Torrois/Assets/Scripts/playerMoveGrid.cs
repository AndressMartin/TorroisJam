using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveGrid : MonoBehaviour
{
    public float velocidade = 5f;
    public Transform pontoMov;
    public static int gridAtual;

    public static int gridAnterior = gridAtual;

    private bool gameStarted = true;

    void Start()
    {
        pontoMov.parent = null;
    }
    
    void FixedUpdate()
    {
        Move();
        Debug.Log("Anterior = " + gridAnterior + "Atual = " + gridAtual);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GridTile")
        {
            GridIndice ThisIndice = collision.GetComponent<GridIndice>();
            gridAtual = ThisIndice.thisIndice;
            if (gameStarted)
            {
                gridAnterior = gridAtual;
                gameStarted = false;
            }
            //Debug.Log("Colidiu com " + gridAtual);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);

        if (Vector2.Distance(transform.position, pontoMov.position) == 0f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                            if (Input.GetAxisRaw("Horizontal") == -1)
                                gridAnterior = gridAtual;
                            if (Input.GetAxisRaw("Horizontal") == +1)
                                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                            if (Input.GetAxisRaw("Vertical") == -1)
                                gridAnterior = gridAtual;
                            if (Input.GetAxisRaw("Vertical") == +1)
                                gridAnterior = gridAtual;
                pontoMov.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}
