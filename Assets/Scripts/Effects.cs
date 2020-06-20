using UnityEngine;
using TMPro;
using System;

public class Effects : MonoBehaviour
{
    public TextMeshProUGUI thingsLeftText;
    private float rDiff = 0.3f;
    private float gDiff = 0.5825846f;
    private float bDiff = 0.2584906f;
    private bool thingsLeftTransition = false;
    private float timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        if (thingsLeftTransition)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 0.3f)
            {
                timeElapsed = 0;
                thingsLeftTransition = false;
            } else
            {
                float factor = timeElapsed / 0.3f;
                thingsLeftText.color = new Color(
                    0.7f + rDiff * factor,
                    0.3f + gDiff * factor,
                    0.35f + bDiff * factor);
            }
        }
    }

    public void BlinkThingsLeft()
    {
        thingsLeftText.color = new Color(0.7f, 0.3f, 0.35f);
        Color targetColor = new Color(1f, 0.8825846f, 0.6084906f);
        thingsLeftTransition = true;
    }
}
