using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameManager gmManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activa la derrota si el jugador entra en la zona
        if (collision.CompareTag("Player"))
        {
            gmManager.PlayerDefeated();
        }
    }
}
