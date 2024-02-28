using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    private static Timer instance;

    public static Timer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Timer").AddComponent<Timer>();
            }
            return instance;
        }
    }

    public float duration = 365f;
    private float currentTime;

    public TextMeshProUGUI timerText; // Reference to TMP text element

    public delegate void TimerEventHandler();
    public event TimerEventHandler OnTimerEnd;

    // Event for method activation every second
    public event TimerEventHandler OnSecondElapsed;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentTime = duration;

        if (timerText != null)
        {
            UpdateTimerText();
        }

        StartCoroutine(Countdown());
    }

    private void UpdateTimerText()
    {
        timerText.text = currentTime.ToString("F1");
    }

    private IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime -= 1.0f;

            if (timerText != null)
            {
                UpdateTimerText();
            }
            OnSecondElapsed?.Invoke();
        }

        OnTimerEnd?.Invoke();
    }
}
