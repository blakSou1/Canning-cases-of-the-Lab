using UnityEngine;
using VContainer;

public class HUD : MonoBehaviour
{
    [Inject] private InputPlayer input;
    [SerializeField] private GameObject pauseMenu;

    /// <summary>
    /// Выход из игры с учетом платформы
    /// </summary>
    [System.Obsolete]
    public void QuitGame()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WebGLPlayer:
                HandleWebQuit();
                break;

            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
                Application.Quit();
                break;

            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                QuitMobilePlatform();
                break;

#if UNITY_EDITOR
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
                UnityEditor.EditorApplication.isPlaying = false;
                break;
#endif
        }
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        input.Player.Esc.started += i => pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        input.Player.Enable();
    }
    private void OnDestroy()
    {
        input.Player.Esc.started -= i => pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        input.Player.Disable();
    }

    /// <summary>
    /// Обработка закрытия WebGL версии
    /// </summary>
    [System.Obsolete]
    private void HandleWebQuit()
    {
    #if UNITY_WEBGL
            Application.ExternalEval(@"
                window.close(); 
            ");
    #elif UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }

    /// <summary>
    /// Обработка закрытия мобильных платформ
    /// </summary>
    private void QuitMobilePlatform()
    {
        #if UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                currentActivity.Call<bool>("moveTaskToBack", true);
            }
        }
        #elif UNITY_IOS
        Application.Quit();
        #endif
    }

}
