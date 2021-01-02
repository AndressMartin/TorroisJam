using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    [SerializeField] public static float timerTime = 120;
    public static bool ganhou = false;
    public static float timerMax = 120f;
    void Awake()
    {
        ganhou = false;
        timerText = GetComponent<TextMeshProUGUI>();
        StartCooldownTimer();
    }

    private void Update()
    {
        UpdateTimer();
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
}
