using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiElements : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelVictory;
    [SerializeField] private GameObject panelCoins;
    [SerializeField] private GameObject panelGems;

    [Header("Panels")]
    [SerializeField] private GameObject panelPowerUp; // icono en canvas
    [SerializeField] private TextMeshProUGUI timerText;
    private float protectionTimeRemaining;
    private bool isProtectionActive = false;

    [Header("Game Over Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Victory Buttons")]
    [SerializeField] private Button winPlayAgainButton;
    [SerializeField] private Button winMainMenuButton;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioSource sfxSourceUI;

    private void Awake()
    {
        if (!panelGameOver || !panelCoins || !panelGems)
        {
            Debug.LogError("Faltan referencias de paneles en UiElements.");
        }
            
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        mainMenuButton.onClick.AddListener(OnExitGameClicked);

        winPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        winMainMenuButton.onClick.AddListener(OnExitGameClicked);

        sfxSourceUI = GetComponent<AudioSource>();

    }

    private void Start()
    {
        sfxSourceUI.loop = false;
    }

    private void Update()
    {
        if (isProtectionActive)
        {
            protectionTimeRemaining -= Time.deltaTime;
            if (protectionTimeRemaining < 0f)
            {
                protectionTimeRemaining = 0f;
            }
            timerText.text = protectionTimeRemaining.ToString("F1") + "s";
        }
    }

    public void UpdatedCoins(int amount)
    {
        coinText.text = amount.ToString();
    }
    public void UpdatedDiamonds(int amount)
    {
        gemText.text = amount.ToString();
    }

    public void StartProtectionScreen(float duration)
    {
        panelPowerUp.SetActive(true);
        protectionTimeRemaining = duration;
        isProtectionActive = true;
        //if (uiCoroutine != null) StopCoroutine(uiCoroutine);
        //uiCoroutine = StartCoroutine(ShowTimer(duration));
    }

    public void EndProtectionScreen()
    {
        isProtectionActive = false;
        panelPowerUp.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        //show elements
        panelGameOver.SetActive(true);
        //panelCoins.SetActive(false);
        //panelGems.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        //show elements
        panelVictory.SetActive(true);
        //panelCoins.SetActive(false);
        //panelGems.SetActive(false);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();

        winPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        winMainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    private void OnPlayAgainClicked()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    private void OnExitGameClicked()
    {
        SceneManager.LoadScene(0);   
    }
}
