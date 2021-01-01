using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogarPecas : MonoBehaviour
{
    public enum DirecoesJogar { esquerda, direita, cima, baixo };
    [Range(1, 3)]
    public int qntdCasasJogar;

    void Start()
    {
        
    }


    void Update()
    {
        if (Cooldown.timerTime == 0)
        {
            JogarPecasFunc();
        }
    }

    void JogarPecasFunc()
    {

    }
}
