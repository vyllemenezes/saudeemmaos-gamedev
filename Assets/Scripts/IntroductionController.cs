using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void onPlayButton()
    {
        SceneManager.LoadScene(1);
    }
}
