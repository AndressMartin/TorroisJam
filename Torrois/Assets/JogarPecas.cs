using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogarPecas : MonoBehaviour
{
    public enum DirecoesJogar { esquerda, direita, cima, baixo };
    [Range(1, 3)]
    public int qntdCasasJogar;

    [SerializeField] public List<GameObject> listaObjetosJogaveis = new List<GameObject>();

    void Start()
    {
        listaObjetosJogaveis.Add(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Torre"))
        {
            listaObjetosJogaveis.Add(gObj);
        }
    }


    void Update()
    {
        int direcaoEscolhida = Random.Range((int)DirecoesJogar.esquerda, (int)DirecoesJogar.baixo);
        Debug.Log(direcaoEscolhida);
        if (Cooldown.timerTime == 0)
        {
            JogarPecasFunc();
        }
    }

    void JogarPecasFunc()
    {
        int direcaoEscolhida = Random.Range((int)DirecoesJogar.esquerda, (int)DirecoesJogar.baixo);
        qntdCasasJogar = Random.Range(1, 4);
        playerMoveGrid.Jogado(qntdCasasJogar);
    }
}
