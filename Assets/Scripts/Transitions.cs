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

    private bool fadeOut = false;
    private bool fadeIn = false;
    private bool blackout = false;
    public Texture fadeTexture;
    private float fadeTimer;
    private float fadeDuration = 0.75f;
    private delegate void SceneTransition();
    private SceneTransition sceneTransition;

    public Image changedPrologueBg;
    private bool disableSceneTransition = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Manual"))
        {
            manualText.text = manualInstructions[0];
        }

        fadeIn = true;

        if (SceneManager.GetActiveScene().name.Equals("Title"))
        {
            if (GameObject.FindObjectOfType<AudioPersist>() != null && !GameObject.FindObjectOfType<AudioPersist>().soundTrack.Equals("springbreeze"))
            {
                JSAM.AudioManager.PlayMusic(JSAM.Music.springbreeze);
                GameObject.FindObjectOfType<AudioPersist>().soundTrack = "springbreeze";
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Prologue"))
        {
            if (GameObject.FindObjectOfType<AudioPersist>() != null && !GameObject.FindObjectOfType<AudioPersist>().soundTrack.Equals("earthspirits"))
            {
                JSAM.AudioManager.PlayMusic(JSAM.Music.earthspirits);
                GameObject.FindObjectOfType<AudioPersist>().soundTrack = "earthspirits";
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("kurantoScene"))
        {
            if (GameObject.FindObjectOfType<AudioPersist>() != null && !GameObject.FindObjectOfType<AudioPersist>().soundTrack.Equals("voile"))
            {
                JSAM.AudioManager.PlayMusic(JSAM.Music.voile);
                GameObject.FindObjectOfType<AudioPersist>().soundTrack = "voile";
            }
        }
    }

    void Update()
    {
        if (fadeIn || fadeOut)
        {
            fadeTimer += Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (fadeIn)
        {
            if (fadeTimer >= fadeDuration)
            {
                fadeTimer = 0f;
                fadeIn = false;
                blackout = false;
            }
            else
            {
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.g, 1f - (fadeTimer / fadeDuration));
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture, ScaleMode.StretchToFill);
            }
        } else if (fadeOut)
        {
            if (fadeTimer >= fadeDuration)
            {
                fadeTimer = 0f;
                fadeOut = false;
                blackout = true;
                if (!disableSceneTransition)
                {
                    sceneTransition();
                }
            }
            else
            {
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.g, fadeTimer / fadeDuration);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture, ScaleMode.StretchToFill);
            }
        } else if (blackout)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.g, 1f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture, ScaleMode.StretchToFill);
        }
    }

    public void DisableSceneTransition()
    {
        // so fading out won't automatically trigger scene transition
        disableSceneTransition = true;
    }

    public void EnableSceneTransition()
    {
        // undo the above
        disableSceneTransition = false;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void ChangePrologueBgImg()
    {
        changedPrologueBg.gameObject.SetActive(true);
    }

    public void LoadGameScene()
    {
        fadeOut = true;
        sceneTransition = () => { SceneManager.LoadScene("kurantoScene"); };
    }

    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
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

    public void StartGame()
    {
        fadeOut = true;
        sceneTransition = () => {
            SceneManager.LoadScene("Prologue");
        };
    }

    public void ReadManual()
    {
        fadeOut = true;
        sceneTransition = () => { SceneManager.LoadScene("Manual"); };
    }

    public void ViewCredits()
    {
        fadeOut = true;
        sceneTransition = () => { SceneManager.LoadScene("Credits"); };
    }

    public void ReturnToTitle()
    {
        fadeOut = true;
        sceneTransition = () => { SceneManager.LoadScene("Title"); };
    }

    public void Quit()
    {
        Application.Quit();
    }
}
