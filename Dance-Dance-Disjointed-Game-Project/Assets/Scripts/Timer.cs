using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public static float currentTime;
    public static float startingTime = 0;
    public bool countDown;

    public bool hasLimit;
    public static float timerLimit = 8;

    public GameObject gameOverScreen;
    public bool isGameOver = false;

    public DancePoseSequence dancePoseSequence;
    public FailsCounter failsCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            FailsCounter.instance.LoseLife();

            if (failsCounter.livesRemaining <= 0)
            {
                isGameOver = true;
                GameOverState();
            }

            dancePoseSequence.OnGroupsAligned();
            //timerText.color = Color.red;
            //enabled = false;
        }

        SetTimerText();

        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }

    public void GameOverState()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        Debug.Log("GameOver");
    }
}
