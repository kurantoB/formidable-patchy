using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueController : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("kurantoScene");
    }
}
