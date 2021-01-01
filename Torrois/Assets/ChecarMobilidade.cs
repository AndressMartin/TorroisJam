using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarMobilidade : MonoBehaviour
{
    public bool ColidiuParede;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Imovel")
        {
            //Debug.Log("Deu certo? ;~;");
            ColidiuParede = true;
        }

    }
}
