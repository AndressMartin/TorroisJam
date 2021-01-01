using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    public Transform pontoMov;
    public int ativado;
    playerMoveGrid player = new playerMoveGrid();
    public BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        ativado = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Alavanca valor: " +ativado);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ativado *= -1;
    }
   
    
}
