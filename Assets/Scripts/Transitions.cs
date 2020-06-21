using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    public string[] manualInstructions;
    public Image[] manualImages;
    private int manualPage = 0;
    public Text manualText;
    public GameObject manualNextButton;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Manual"))
        {
            manualText.text = manualInstructions[0];
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("kurantoScene");
    }

    public void NextManualPage()
    {
        if (manualPage < manualInstructions.Length - 1)
        {
            manualImages[manualPage].gameObject.SetActive(false);
            manualPage++;
            manualText.text = manualInstructions[manualPage];
            manualImages[manualPage].gameObject.SetActive(true);
            if (manualPage == manualInstructions.Length - 1)
            {
                manualNextButton.SetActive(false);
            }
        }
    }
}
