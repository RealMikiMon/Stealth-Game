using UnityEngine;
using TMPro;

public class DistanceUI : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TMP_Text distanceText;

    void Awake()
    {
        if (distanceText == null)
            distanceText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (player == null || distanceText == null) return;
        distanceText.text = $"Steps: {player.DistanceTraveled:F1}";
    }
}