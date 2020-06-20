using UnityEngine;
using TMPro;

public class Effects : MonoBehaviour
{
    public TextMeshProUGUI thingsLeftText;
    private float rDiff = 0.3f;
    private float gDiff = 0.5825846f;
    private float bDiff = 0.2584906f;
    private bool thingsLeftTransition = false;
    private float thingsLeftTimeElapsed = 0f;

    public Transform progressHeart;
    private float stage0 = 0f;
    private float stage1 = 450f / 3;
    private float stage2 = 2f * 450 / 3;
    private float stage3 = 450f;
    private float heartStartPosition;
    private float heartDestPosition;
    private float heartTimeElapsed = 0f;
    private bool heartTransition = false;

    // Update is called once per frame
    void Update()
    {
        if (thingsLeftTransition)
        {
            thingsLeftTimeElapsed += Time.deltaTime;
            if (thingsLeftTimeElapsed >= 0.3f)
            {
                thingsLeftText.color = new Color(1f, 0.8825846f, 0.6084906f);
                thingsLeftTimeElapsed = 0;
                thingsLeftTransition = false;
            } else
            {
                float factor = thingsLeftTimeElapsed / 0.3f;
                thingsLeftText.color = new Color(
                    0.7f + rDiff * factor,
                    0.3f + gDiff * factor,
                    0.35f + bDiff * factor);
            }
        }
        if (heartTransition)
        {
            Debug.Log("Heart x: " + progressHeart.localPosition.x);
            heartTimeElapsed += Time.deltaTime;
            if (heartTimeElapsed >= 0.3f)
            {
                float x = heartDestPosition;
                float y = progressHeart.localPosition.y;
                float z = progressHeart.localPosition.z;
                progressHeart.localPosition = new Vector3(x, y, z);
                Debug.Log("Heart x: " + progressHeart.localPosition.x);
                heartTimeElapsed = 0;
                heartTransition = false;
            } else
            {
                float x = heartStartPosition + (heartTimeElapsed / 0.3f) * (heartDestPosition - heartStartPosition);
                float y = progressHeart.localPosition.y;
                float z = progressHeart.localPosition.z;
                progressHeart.localPosition = new Vector3(x, y, z);
            }
        }
    }

    public void BlinkThingsLeft()
    {
        thingsLeftText.color = new Color(0.7f, 0.3f, 0.35f);
        Color targetColor = new Color(1f, 0.8825846f, 0.6084906f);
        thingsLeftTransition = true;
    }

    public void MoveProgressHeart(int stage)
    {
        heartStartPosition = progressHeart.localPosition.x;
        Debug.Log("Heart x: " + progressHeart.localPosition.x);
        switch (stage)
        {
            case 0:
                heartDestPosition = -265 + stage0;
                break;
            case 1:
                heartDestPosition = -265 + stage1;
                break;
            case 2:
                heartDestPosition = -265 + stage2;
                break;
            case 3:
                heartDestPosition = -265 + stage3;
                break;
        }
        heartTransition = true;
    }
}
