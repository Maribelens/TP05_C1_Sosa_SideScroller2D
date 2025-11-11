using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Eventos para comunicar estados de juego
    public event Action OnGameOver;
    public event Action OnVictory;


    [Header("References")]
    [SerializeField] private UIPickables pickablesUI;
    [SerializeField] private HealthSystem health;

    [Header("Pickables")]
    public int coins = 0;
    public int diamonds = 0;
    public int healtPlus = 20;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private AudioSource gameplayMusisSource;
    [SerializeField] private AudioSource coreGameLoopMusicSource;

    private void Awake()
    {
        if (gameplayMusisSource != null)
        {
            gameplayMusisSource.clip = gameplayMusic;
            gameplayMusisSource.Play();
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        health.onInvulnerableStart += ActivateProtection;
    }

    private void OnDestroy()
    {
        health.onInvulnerableStart -= ActivateProtection;
    }

    // Activa la protección temporal del jugador
    public void ActivateProtection()
    {
        if (health == null)
        {
            Debug.LogWarning("ActivateProtection: health es null");
            return;
        }
        Debug.Log("Proteccion activada");
        health.StartInvulnerability(7f);
    }

    // Suma monedas y actualiza la interfaz
    public void AddCoins(int amount)
    {
        coins += amount;
        pickablesUI.UpdateAmountCoins(coins);
        Debug.Log("Monedas: " + coins);
    }

    // Suma gemas y actualiza la interfaz
    public void AddGems(int amount)
    {
        diamonds += amount;
        pickablesUI.UpdateAmountDiamonds(diamonds);
        Debug.Log("Diamantes: " + diamonds);
    }

    // Restaura salud del jugador
    public void AddHealth()
    {
        health.Heal(healtPlus);
    }

    // Lógica de derrota del jugador
    public void PlayerDefeated()
    {
        Time.timeScale = 0;
        gameplayMusisSource.Stop();

        coreGameLoopMusicSource.clip = gameOverMusic;
        coreGameLoopMusicSource.Play();

        OnGameOver?.Invoke();

        Debug.Log("El alien fue derrotado");
    }

    public void PlayerVictory()
    {
        // Lógica de victoria del jugador
        Time.timeScale = 0;
        gameplayMusisSource.Stop();

        coreGameLoopMusicSource.clip = victoryMusic;
        coreGameLoopMusicSource.Play();

        OnVictory?.Invoke();

        Debug.Log("El alien ha ganado");
    }

}
