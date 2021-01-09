using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{

    public static bool tempoParado;
    private TextMeshProUGUI timerText;
    [SerializeField] public static float timerTime = 120f;
    public static float timerMax = 5f;

    Color white = new Color(255, 255, 255);
    Color red = new Color(255, 0, 0);
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCooldownTimer();
    }

    private void Update()
    {
        if (!tempoParado)
        {
            if (timerTime > 0)
            {
                UpdateTimer();
                timerText.color = white;
            }
            else
            {
                timerTime = 0;
                timerText.color = red;
                timerText.text = "FALHOU";
            }
        }
    }

    void StartCooldownTimer()
    {
        if (timerText != null)
        {
            timerTime = timerMax;
            timerText.text = "5:00";
            //InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerText != null)
        {
            timerTime -= Time.deltaTime;
            string minutes = Mathf.Floor(timerTime / 60).ToString("00");
            string seconds = (timerTime % 60).ToString("00");
            //string fraction = ((timerTime * 100) % 100).ToString("000");
            timerText.text = minutes + ":" + seconds/* + "\n:" + fraction*/;
        }
    }

    public static void ReiniciarTimer() //TODO: ReiniciarTimer quando voltar a 0, não quando chamar
    {
        timerTime = timerMax;
    }

    public static void StopTimer()
    {
        tempoParado = true;
    }

    public static void PlayTimer()
    {
        tempoParado = false;
    }
}
