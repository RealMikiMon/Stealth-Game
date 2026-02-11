using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timerTime;
    public float CurrentTime => timerTime;

    void Update()
    {
        timerTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timerTime / 60);
        int seconds = Mathf.FloorToInt(timerTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

}
