using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirSalaAnterior : MonoBehaviour
{
    public static GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DestruirSalaAnteriorFunc()
    {
        GameObject salaAnterior = GameObject.Find("Sala" + MainCamera.GetComponent<CameraMov>().indice.ToString());
        Destroy(salaAnterior);
    }
}
