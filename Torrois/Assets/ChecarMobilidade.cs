using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarMobilidade : MonoBehaviour
{
    public Transform myParent;

    public bool ColidiuParede;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myParent.tag == "Torre" || myParent.tag == "Rainha")
        {
            if (collision.gameObject.transform != myParent && 
                (collision.gameObject.tag == "Torre" || collision.gameObject.tag == "Rainha"))
            {
                //Debug.Log(collision.gameObject.tag);
                //Debug.Log("Deu certo? ;~;");

                if (collision.GetComponent<Caixa>().pontoMov.GetComponent<ChecarMobilidade>().ColidiuParede != true) //Se a outra caixa não tiver pra onde ir
                {
                    collision.GetComponent<Caixa>().colidiuCaixa = true;
                    for (int direcao = 0; direcao < myParent.GetComponent<Caixa>().direcoesMov.Count; direcao++)
                    {
                        collision.GetComponent<Caixa>().direcoesMovCxColl[direcao] = myParent.GetComponent<Caixa>().direcoesMov[direcao];
                    }
                }
            }
            else if (collision.gameObject.transform != myParent && (collision.gameObject.tag == "Imovel"))
            {
                ColidiuParede = true;
            }
        }
        //if (collision.gameObject.tag == "Imovel")
        //{
        //    //Debug.Log("Deu certo? ;~;");
        //    ColidiuParede = true;
        //}

    }
}
