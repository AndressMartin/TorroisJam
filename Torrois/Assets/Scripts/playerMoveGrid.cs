using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveGrid : MonoBehaviour
{
    public float velocidade = 5f;
    public Transform pontoMov;


    void Start()
    {
        pontoMov.parent = null;
    }
    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pontoMov.position, velocidade * Time.deltaTime);

        if (Vector2.Distance(transform.position, pontoMov.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) 
            {
                pontoMov.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                pontoMov.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }
        }
    }
}
