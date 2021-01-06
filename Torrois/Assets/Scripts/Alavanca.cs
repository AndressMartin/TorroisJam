using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{

    public GameObject porta;
    private Caixa scriptDaPorta;
    public bool ativado;

    public int sentido; //0=horizontal 1=vertical
    public Transform pontoMov;
    playerMoveGrid player = new playerMoveGrid();
    public BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        if (tag == "AlavancaH")
            sentido = 0;
        else if (tag == "AlavancaV")
            sentido = 1;
        scriptDaPorta = porta.GetComponent<Caixa>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sentido);
        ativado = scriptDaPorta.ativado;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Torre")
        {
            scriptDaPorta.ativado = !scriptDaPorta.ativado;
        }
    }



}
