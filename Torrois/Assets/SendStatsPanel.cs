using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendStatsPanel : MonoBehaviour
{
    GameObject persister;
    private GameObject Panel;
    public TextMeshProUGUI[] timerTexts;
    public TextMeshProUGUI[] nameTexts;
    private Color red = new Color(255, 0, 0);

    public void sendToPanelTexts()
    {
        Panel = gameObject.GetComponent<Ending>().Panel;
        persister = GameObject.FindGameObjectWithTag("Persister");
        for (int i = 0; i < persister.GetComponent<getStats>().salaNames.Length; i++)
        {
            nameTexts[i].text = persister.GetComponent<getStats>().salaNames[i];
        }
        for (int i = 0; i < persister.GetComponent<getStats>().salaNames.Length; i++)
        {
            timerTexts[i].text = persister.GetComponent<getStats>().timerStats[i];
            if (persister.GetComponent<getStats>().timerStats[i] == "00:00")
            {
                timerTexts[i].color = red;
            }
        }
    }
}
