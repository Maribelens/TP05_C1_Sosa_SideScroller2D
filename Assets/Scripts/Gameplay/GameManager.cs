using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("UI Elements")]
    public UiElements uiGameOver;
    public PlayerController player;
    

    [Header("Pickables")]
    public int coins = 0;
    public int diamonds = 0;
    private bool isProtected = false;

    [Header("Audio")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        musicSource.clip = gameplayMusic;
        musicSource.Play();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Monedas: " + coins);
    }
    public void AddGems(int amount)
    {
        diamonds += amount;
        Debug.Log("Diamantes: " + diamonds);
    }

    public void ActivateShield(float duration)
    {
        if (isProtected) return;
        StartCoroutine(ShieldRoutine(duration));
    }
    private IEnumerator ShieldRoutine(float duration)
    {
        isProtected = true;
        Debug.Log("Protección activada");
        // aquí puedes activar un efecto visual en el jugador, como un aura o escudo
        yield return new WaitForSeconds(duration);
        isProtected = false;
        Debug.Log("Protección terminada");
    }

    public void PlayerDefeated()
    {
        Time.timeScale = 0;
        musicSource.Stop();
        uiGameOver.ShowGameOverScreen();
        Debug.Log("El alien fue derrotado");
    }

}
