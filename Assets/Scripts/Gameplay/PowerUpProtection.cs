using UnityEngine;

public class PowerUpProtection : MonoBehaviour
{
    [SerializeField] HealthSystem health;
    [SerializeField] private float protectionDuration = 5f;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffectPrefab;
    //[SerializeField] private bool refreshIfActive = true; // política: refresh o ignorar

    private void Start()
    {
        health = GetComponent<HealthSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (health == null)
        {
            Debug.LogWarning("Player no tiene HealthSystem");
            return;
        }
        // Si ya está invulnerable y no queremos refrescar, salir
        if (health.isInvulnerable)
        {
            // se puede reproducir un 'bump' o negar recolección
            return;
        }

        // Activar invulnerabilidad
        //health.SetInvulnerable(protectionDuration);
    }
}
