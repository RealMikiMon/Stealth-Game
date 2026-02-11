using UnityEngine;
using TMPro;

public class FinalTimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);
        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}