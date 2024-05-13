using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    int minute = 0;
    float seconds = 0f;
    float oldSeconds = 0f;
    TextMeshProUGUI timerText;
    public bool timerStop;

    void Start()
    {
        timerStop = true;
        timerText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!timerStop)
        {
            seconds += Time.deltaTime;
        }
        if (seconds >= 60f)
        {
            minute++;
            seconds -= 60;
        }
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
    }
}
