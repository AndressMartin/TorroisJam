using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
    public static GameObject MainCamera;
    public GameObject[] listaSalas;
    public GameObject[] salasHolder;
    public static int indice = 0;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesativarSalaAnterior()
    {
        int indiceTemp = indice - 1;
        listaSalas[indiceTemp].SetActive(false);
    }

    public void AtivarProxSala()
    {
        int indiceTemp = indice + 1;
        listaSalas[indiceTemp].SetActive(true);
    }
}
