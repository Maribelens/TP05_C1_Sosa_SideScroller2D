using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiElements : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelCoins;
    [SerializeField] private GameObject panelGems;

    [Header("Buttons")]
    [SerializeField] private Button btnPlayAgain;
    [SerializeField] private Button btnExitGame;

    [Header("References")]
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioSource sfxSourceUI;

    private PlayerController player;

    private void Awake()
    {
        btnPlayAgain.onClick.AddListener(OnPlayAgainClicked);
        btnExitGame.onClick.AddListener(OnExitGameClicked);
        sfxSourceUI = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        sfxSourceUI.loop = false;
    }
    public void ShowGameOverScreen()
    {
        //show elements
        panelGameOver.SetActive(true);
        panelCoins.SetActive(false);
        panelGems.SetActive(false);
        //txtDistance.text = finalDistance.ToString("F2") + " m";
    }

    private void OnDestroy()
    {
        btnPlayAgain.onClick.RemoveAllListeners();
        btnExitGame.onClick.RemoveAllListeners();
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
