using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SalaManager : MonoBehaviour
{
    public static GameObject MainCamera;
    public GameObject[] listaSalas;
    public GameObject[] salasHolder;
    public TextMeshProUGUI SalaName;
    public string[] nomesSalas;
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

    public void DesativarNome()
    {
        int indiceTemp = indice - 1;
        SalaName.transform.parent.gameObject.SetActive(false);
        
    }

    public void AtivarNome()
    {
        int indiceTemp = indice;
        SalaName.transform.parent.gameObject.SetActive(true);
        SalaName.text = nomesSalas[indiceTemp];
    }
}
