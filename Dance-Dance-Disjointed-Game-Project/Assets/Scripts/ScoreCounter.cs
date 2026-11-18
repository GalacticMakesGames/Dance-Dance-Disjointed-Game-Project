using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;

    public TextMeshProUGUI scoreText;
    public int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddPoint()
    {
        scoreText.text = "SCORE: " + score.ToString();

        StartCoroutine(ScoreEffects());
    }

    private IEnumerator ScoreEffects()
    {
        for (float i = 1f; i <= 1.2f; i += 0.05f) // 0.01f is a good speed in unity, but too slow in web build? And 2f is a good size in unity, but too big in web build?
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        scoreText.color = Color.green;

        score += 1;

        for (float i = 1.2f; i >= 1f; i -= 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        scoreText.color = Color.white;
    }
}
