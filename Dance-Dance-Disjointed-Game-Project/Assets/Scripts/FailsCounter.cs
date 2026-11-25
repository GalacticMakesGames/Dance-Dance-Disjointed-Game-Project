using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FailsCounter : MonoBehaviour
{
    public static FailsCounter instance;

    //public TextMeshProUGUI livesRemainingText;
    //public int livesRemaining = 5;

    public GameObject firstStrikeX;
    public GameObject secondStrikeX;
    public GameObject thirdStrikeX;

    public bool firstStrike = false;
    public bool secondStrike = false;
    public bool thirdStrike = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //livesRemainingText.text = "LIVES: " + livesRemaining.ToString();
        firstStrikeX.SetActive(false);
        secondStrikeX.SetActive(false);
        thirdStrikeX.SetActive(false);
    }

    public void AddStrike()
    {
        if (firstStrike == false &&  secondStrike == false && thirdStrike == false)
        {
            firstStrikeX.SetActive(true);
            firstStrike = true;
        }
        else if (firstStrike == true && secondStrike == false && thirdStrike == false)
        {
            secondStrikeX.SetActive(true);
            secondStrike = true;
        }
        else if (firstStrike == true && secondStrike == true && thirdStrike == false)
        {
            thirdStrikeX.SetActive(true);
            thirdStrike = true;
        }
    }

    //public void LoseLife()
    //{
    //    livesRemainingText.text = "LIVES: " + livesRemaining.ToString();

    //    StartCoroutine(ScoreEffects());
    //}

    //private IEnumerator ScoreEffects()
    //{
    //    for (float i = 1f; i <= 1.2f; i += 0.05f) // 0.01f is a good speed in unity, but too slow in web build? And 2f is a good size in unity, but too big in web build?
    //    {
    //        livesRemainingText.rectTransform.localScale = new Vector3(i, i, i);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    livesRemainingText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    //    livesRemainingText.color = Color.red;

    //    livesRemaining -= 1;

    //    for (float i = 1.2f; i >= 1f; i -= 0.05f)
    //    {
    //        livesRemainingText.rectTransform.localScale = new Vector3(i, i, i);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    livesRemainingText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    //    livesRemainingText.color = Color.white;
    //}
}
