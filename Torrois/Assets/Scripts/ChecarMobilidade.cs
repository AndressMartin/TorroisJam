using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarMobilidade : MonoBehaviour
{
    public Transform myParent;

    public bool ColidiuParede;

    [SerializeField]public List<bool> direcoesMov = new List<bool>() { false, false, false, false };
    private void Update()
    {
        if (myParent.GetComponent<Rewinder>().isRewinding)
            transform.position = myParent.GetComponent<Rewinder>().firstPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myParent.tag == "Torre" || myParent.tag == "Rainha" || myParent.tag == "Peon")
        {
            if (collision.gameObject.transform != myParent && ((collision.gameObject.tag == "Imovel") || (collision.gameObject.tag == "AlavancaH") || (collision.gameObject.tag == "AlavancaV")))
            {

                //if (myParent.GetComponent<Caixa>().colidiuCaixa == true) //PARA FUTURO, PRIMEIRO ARRUMAR COLISAO DAS CAIXAS
                //{
                //    Debug.Log("Mudei o pontoMov da caixa que colidiu comigo");
                //    myParent.GetComponent<Caixa>().PontoColidiuComigo.GetComponent<ChecarMobilidade>().ColidiuParede = true;
                //}
                Debug.Log("Checar COLISA");

                ColidiuParede = true;
            }
            else if (collision.gameObject.transform != myParent && (collision.gameObject.tag == "Torre" || collision.gameObject.tag == "Rainha" || collision.gameObject.tag == "Peon"))
            {
                if (myParent.tag == "Rainha")
                {
                    //myParent.GetComponent<Caixa>().andandoComoRainha = false;
                    myParent.GetComponent<Caixa>().colidiuJogador = false;
                }
                collision.gameObject.GetComponent<Caixa>().PontoColidiuComigo = gameObject.transform;
                //Debug.Log(collision.gameObject + " " + collision.gameObject.GetComponent<Caixa>().PontoColidiuComigo);

                if (collision.GetComponent<Caixa>().pontoMov.GetComponent<ChecarMobilidade>().ColidiuParede == false) //Se a outra caixa não tiver pra onde ir
                {
                    Debug.Log("Colidiu com o Peão");
                    
                    if (myParent.tag == "Rainha" && Vector2.Distance(transform.position, myParent.position) == 0f)
                    {
                        collision.GetComponent<Caixa>().colidiuCaixa = true;
                    }
                    else
                        collision.GetComponent<Caixa>().colidiuCaixa = true;
                    for (int direcao = 0; direcao < myParent.GetComponent<Caixa>().direcoesMov.Count; direcao++)
                    {
                        collision.GetComponent<Caixa>().direcoesMovCxColl[direcao] = myParent.GetComponent<Caixa>().direcoesMov[direcao];
                    }
                }
            }
        }
    }
}
