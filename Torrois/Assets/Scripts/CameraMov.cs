using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public static bool podeMover;
    private float velocidade = 20f;
    private GameObject player;
    public GameObject playerMovePoint;
    public GameObject GameManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        transform.position = new Vector3(0f, 0f, -10f);
    }

    void FixedUpdate()
    {
        if (podeMover)
        {
            GameManager.GetComponent<SalaManager>().AtivarProxSala();
            GameManager.GetComponent<SalaManager>().DesativarNome();
            FazerMovimento();
        }
    }

    public void FazerMovimento()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            GameManager.GetComponent<SalaManager>().salasHolder[SalaManager.indice+1].transform.position, velocidade * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, GameManager.GetComponent<SalaManager>().salasHolder[SalaManager.indice+1].transform.position) == 0f)
        {
            SalaManager.indice += 1;
            podeMover = false;
            TrazerGrid();
            TrazerJogador();
            StartCoroutine(EsperaParaDesativarSala());
        }
    }

    public void TrazerGrid()
    {
        GameObject.FindGameObjectWithTag("GameController").transform.position = gameObject.transform.position;
    }

    public void TrazerJogador()
    {
        player.GetComponent<FMODUnity.StudioEventEmitter>().CollisionTag = "Torre";  //GAMBIARRA!!!!
        player.GetComponent<playerMoveGrid>().transitandoEntreFases = true;
        playerMovePoint.transform.position = FindClosestWalkableGrid().transform.position;
        player.GetComponent<Rewinder>().firstPosition = FindClosestWalkableGrid().transform.position;
        //Debug.Log(FindClosestGrid().transform.position);
    }

    public GameObject FindClosestWalkableGrid()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("GridTile");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = playerMovePoint.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        closest.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y, 0f);
        return closest;
    }

    IEnumerator EsperaParaDesativarSala()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        GameManager.GetComponent<SalaManager>().DesativarSalaAnterior();
        GameManager.GetComponent<SalaManager>().AtivarNome();
    }
}
