using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    public bool isRewinding;
    public bool tempoExpirado;
    private int velocidade = 100;
    private float recordTime = 7f;
    public List<Vector3> positions;
    public Vector3 firstPosition;

    void Start()
    {
        firstPosition = transform.position;
        positions = new List<Vector3>();
    }

    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        if (isRewinding && !tempoExpirado)
            Rewind();
        if (isRewinding && tempoExpirado)
        {
            StartCoroutine(EsperaRewind());
        }
        if (!isRewinding)
            Record();
    }

    

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            //Debug.Log("Expirou!");
            tempoExpirado = true;
            positions.RemoveAt(positions.Count - 1);
        }
        positions.Insert(0, transform.position);
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }

    void VoltarAutomatico()
    {
        Debug.Log("Voltando!");
        tempoExpirado = false;
        isRewinding = false;
        positions.Clear();
        transform.position = firstPosition;
    }

    IEnumerator EsperaRewind()
    {
        yield return new WaitForSeconds(1.2f);
        VoltarAutomatico();
    }
}
