using UnityEngine;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private GameObject panelSettings;

    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnCreditsBack;
    [SerializeField] private Button btnSettingsBack;

    private bool isPause;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnCreditsBack.onClick.AddListener(OnCreditsBackClicked);
        btnSettingsBack.onClick.AddListener(OnSettingsBackClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnCreditsBack.onClick.RemoveAllListeners();
        btnSettingsBack.onClick.RemoveAllListeners();
    }

      private void TogglePause()
      {
          isPause = !isPause;
          panelPause.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void OnPlayClicked()
    {
        TogglePause();
    }

    private void OnOptionsClicked()
    {
        panelSettings.SetActive(true);
    }
    private void OnExitClicked()
    {
        ExitGame();
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif 
    }

    private void OnCreditsBackClicked()
    {
        panelCredits.SetActive(false);
    }

    private void OnSettingsBackClicked()
    {
        panelSettings.SetActive(false);
    }
}
