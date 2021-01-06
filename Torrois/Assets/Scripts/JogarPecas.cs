using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogarPecas : MonoBehaviour
{
    public enum DirecoesJogar { esquerda, direita, cima, baixo };
    [Range(1, 3)]
    public int qntdCasasJogar;

    [SerializeField] public List<GameObject> listaObjetosJogaveis = new List<GameObject>();

    playerMoveGrid playerMoveLocal;

    void Start()
    {
        playerMoveLocal = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoveGrid>();
        listaObjetosJogaveis.Add(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Torre"))
        {
            listaObjetosJogaveis.Add(gObj);
        }
    }


    void Update()
    {
        if (Cooldown.timerTime <= 0)
        {
            JogarPecasFunc();
        }
    }

    void JogarPecasFunc()
    {
        DirecoesJogar direcao = (DirecoesJogar)Random.Range(0, 1);
        qntdCasasJogar = Random.Range(4, 9);
        playerMoveLocal.qntQuadradosLocal = qntdCasasJogar;
        playerMoveLocal.direcaoTorreJoga = direcao.ToString();
        playerMoveLocal.podeJogar = true;
        Debug.Log(direcao.ToString());
        Cooldown.ReiniciarTimer();
    }
}
