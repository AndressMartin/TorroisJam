using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public float velocidade = 20f;
    public Transform cameraMov;


    void Start()
    {
        transform.position = new Vector3(0f, 0f, -10f);
        cameraMov.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, cameraMov.position, velocidade * Time.deltaTime);

        if (Vector3.Distance(transform.position, cameraMov.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                cameraMov.position += new Vector3(Input.GetAxisRaw("Horizontal")*10, 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                cameraMov.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*10, 0f);
            }
        }
    }
}
