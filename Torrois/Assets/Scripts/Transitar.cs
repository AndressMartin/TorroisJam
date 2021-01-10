using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitar : MonoBehaviour
{
    BoxCollider2D portaBoxColl;
    public bool portaOpen;
    public bool passou;
    public bool ganhou;
    // Start is called before the first frame update
    void Start()
    {
        portaBoxColl = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portaOpen && !passou)
        {
            portaBoxColl.enabled = true;
        }
        else
            portaBoxColl.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ganhou)
            {
                SceneManager.LoadScene(3);
            }
            passou = true;
            CameraMov.podeMover = true;
        }
    }
}
