using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitar : MonoBehaviour
{
    Transform Porta;
    BoxCollider2D portaBoxColl;
    public bool portaOpen;
    // Start is called before the first frame update
    void Start()
    {
        portaBoxColl = Porta.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portaOpen)
        {
            portaBoxColl.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CameraMov.podeMover = true;
        }
    }
}
