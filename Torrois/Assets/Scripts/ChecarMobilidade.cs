using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarMobilidade : MonoBehaviour
{
    public Transform myParent;

    public bool ColidiuParede;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myParent.tag == "Torre" || myParent.tag == "Rainha" || myParent.tag == "Peon")
        {
            if (collision.gameObject.transform != myParent && ((collision.gameObject.tag == "Imovel") || (collision.gameObject.tag == "AlavancaH") || (collision.gameObject.tag == "AlavancaV")))
            {
                Debug.Log(myParent.GetComponent<Caixa>().colidiuCaixa);
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
                //Debug.Log(collision.gameObject.tag);
                //if (collision.tag == myParent.tag)
                //    Debug.Log("oporra");
                //Debug.Log("Deu certo? ;~;");
                collision.gameObject.GetComponent<Caixa>().PontoColidiuComigo = gameObject.transform;
                Debug.Log(collision.gameObject + " " + collision.gameObject.GetComponent<Caixa>().PontoColidiuComigo);

                if (collision.GetComponent<Caixa>().pontoMov.GetComponent<ChecarMobilidade>().ColidiuParede == false) //Se a outra caixa não tiver pra onde ir
                {
                    Debug.Log("Colidiu com o Peão");
                    collision.GetComponent<Caixa>().colidiuCaixa = true;
                    for (int direcao = 0; direcao < myParent.GetComponent<Caixa>().direcoesMov.Count; direcao++)
                    {
                        collision.GetComponent<Caixa>().direcoesMovCxColl[direcao] = myParent.GetComponent<Caixa>().direcoesMov[direcao];
                    }
                }
                //else //TENTANDO PREVENIR O BUG DAS CAIXAS JUNTAS
                //{
                //    ColidiuParede = true;
                //}
            }
            //collision.GetComponent<Caixa>().colidiuCaixa = false;
        }
        //if (collision.gameObject.tag == "Imovel")
        //{
        //    //Debug.Log("Deu certo? ;~;");
        //    ColidiuParede = true;
        //}

    }

    public void ChecarProx()
    {

    }
}
