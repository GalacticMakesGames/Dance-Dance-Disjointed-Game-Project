using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoreoProgressBar : MonoBehaviour
{
    public int maxChoreoBarFill = 20;
    public int currentChoreoBarFill = 0;

    public Image fillImage;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    void Update()
    {
        // calculate and update slider value
        float fillValue = (float)currentChoreoBarFill / (float)maxChoreoBarFill;
        slider.value = fillValue;

        // trigger victory screen when the choro is completed
        if (fillValue >= slider.maxValue)
        {
            Debug.Log("Choreography Complete! Victory Screen enabled.");
        }

        // handles visibility of fill based on whether there is progress
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        else
        {
            fillImage.enabled = true;
        }
    }

    public void ChoreoBarFill(int fillAmount)
    {
        currentChoreoBarFill += fillAmount;
        currentChoreoBarFill = Mathf.Clamp(currentChoreoBarFill, 0, maxChoreoBarFill);
    }
}
