using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiMainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private GameObject panelSettings;

    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnCreditsBack;
    [SerializeField] private Button btnSettingsBack;

    [Header("Settings Player 1")]
    //[SerializeField] private player player1;
    //[SerializeField] private Slider speedPlayer1;
    [SerializeField] private TextMeshProUGUI sliderText1 = null;

    [Header("Settings Player 2")]
    //[SerializeField] private Movement player2;
    //[SerializeField] private Slider speedPlayer2;
    [SerializeField] private TextMeshProUGUI sliderText2 = null;

    private float initialValue = 0.001f;
    private bool showDecimalSpeed = true;
    private bool isPause;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnCreditsBack.onClick.AddListener(OnCreditsBackClicked);
        btnSettingsBack.onClick.AddListener(OnSettingsBackClicked);

        //speedPlayer1.onValueChanged.AddListener(OnSpeedP1Changed);
        //speedPlayer2.onValueChanged.AddListener(OnSpeedP2Changed);
    }

    private void Start()
    {
        //speedPlayer1.value = initialValue;
        //speedPlayer2.value = initialValue;
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
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnCreditsBack.onClick.RemoveAllListeners();
        btnSettingsBack.onClick.RemoveAllListeners();
        //speedPlayer1.onValueChanged.RemoveAllListeners();
        //speedPlayer2.onValueChanged.RemoveAllListeners();

    }

      private void TogglePause()
      {
          isPause = !isPause;
          panelPause.SetActive(isPause);
        if (isPause)
        {
            Time.timeScale = 0f;
            //player1.enabled = false;
            //player2.enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            //player1.enabled = true;
            //player2.enabled = true;
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

    private void OnCreditsClicked()
    {
        panelCredits.SetActive(false);
        panelCredits.SetActive(true);
    }
    private void OnExitClicked()
    {
        //TogglePause();
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
    private void OnSpeedP1Changed(float speed)
    {
        //player1.speed = speed;
        if (showDecimalSpeed)
            sliderText1.SetText(speed.ToString("0.001"));
        else
            sliderText1.SetText(speed.ToString("0.05"));
    }

    private void OnSpeedP2Changed(float speed)
    {
        //player2.speed = speed;
        if (showDecimalSpeed)
            sliderText2.SetText(speed.ToString("0.001"));
        else
            sliderText2.SetText(speed.ToString("0.05"));
    }

}
