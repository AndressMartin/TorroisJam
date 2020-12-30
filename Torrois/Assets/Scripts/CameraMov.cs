using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public static bool podeMover;
    private float velocidade = 20f;
    private GameObject player;
    private GameObject movePoint;
    [SerializeField] public List<GameObject> listaSalas = new List<GameObject>();
    public int indice = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movePoint = player.transform.GetChild(0).gameObject;
        transform.position = new Vector3(0f, 0f, -10f);
    }

    void Update()
    {
        if (podeMover)
        {
            FazerMovimento();
        }
    }

    public void FazerMovimento()
    {
        transform.position = Vector3.MoveTowards(transform.position, listaSalas[indice+1].transform.position, velocidade * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, listaSalas[indice+1].transform.position) == 0f)
        {
            indice += 1;
            podeMover = false;
            player.transform.position = transform.position;
            movePoint.transform.position = transform.position;
            
        }
    }
}
