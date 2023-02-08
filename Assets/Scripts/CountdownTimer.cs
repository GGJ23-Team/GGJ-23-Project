using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float initialTime = 90;
    [SerializeField] private Text timerText;
    float secondsLeft = 0f;
    bool timerEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        timerEnabled = true;
        secondsLeft = initialTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            if (secondsLeft >= 0)
            {
                UpdateTimer();
                secondsLeft -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time's up!");
                secondsLeft = 0;
                timerEnabled = false;
                StartCoroutine(WaitForSceneLoad());
            }
        }
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(secondsLeft / 60);
        int seconds = Mathf.FloorToInt(secondsLeft % 60);
        timerText.text = string.Format("{0:00}\n{1:00}", minutes, seconds);
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1.0f);
        new LoadScene().LoadSceneByPath("Assets/Scenes/Game Over.unity");
    }
}
