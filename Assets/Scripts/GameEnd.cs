using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private TimerUI timer;
    public void EndGame()
    {
        TimerUI timer = FindObjectOfType<TimerUI>();
        if (timer == null)
        {
            return;
        }
        PlayerPrefs.SetFloat("FinalTime", timer.CurrentTime);
        PlayerPrefs.Save();
        SceneHandler.Instance.ChangeScene();
    }
}