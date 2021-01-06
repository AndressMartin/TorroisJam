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
                Debug.Log("Checar COLISA");
                ColidiuParede = true;
            }
            else if (collision.gameObject.transform != myParent &&
                (collision.gameObject.tag == "Torre" || collision.gameObject.tag == "Rainha" || collision.gameObject.tag == "Peon"))
            {
                //Debug.Log(collision.gameObject.tag);
                //if (collision.tag == myParent.tag)
                //    Debug.Log("oporra");
                //Debug.Log("Deu certo? ;~;");

                if (collision.GetComponent<Caixa>().pontoMov.GetComponent<ChecarMobilidade>().ColidiuParede == false) //Se a outra caixa não tiver pra onde ir
                {
                    Debug.Log("Se chamar no bug, isto é um problema.");
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
}
