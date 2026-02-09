using UnityEngine;
using TMPro;
public class DistanceToEnd : MonoBehaviour
{
    [SerializeField] private Transform player, point;
    [SerializeField] private TextMeshProUGUI distanceText;
    private float distance;
    private void Update()
    {
        CalculatedDistance();
        Win();
    }

    private void CalculatedDistance()
    {
        distance = (point.transform.position.x - player.transform.position.x) + (point.transform.position.y - player.transform.position.y);
        distanceText.text = "Objective" + distance.ToString();
    }

    private void Win()
    {
        if (distance <= 1)
        {
            SceneHandler.Instance.ChangeScene();
        }
    }
}
