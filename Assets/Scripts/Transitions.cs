using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("kurantoScene");
    }
}
