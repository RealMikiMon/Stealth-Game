using UnityEngine;
using TMPro; // o UnityEngine.UI si usas Text normal

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = "Score: " + finalScore;
    }
}