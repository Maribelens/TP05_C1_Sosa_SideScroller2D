using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb2D;
    private HealthSystem healthSystem;
    [SerializeField] private GameObject deathEffectPrefab;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Audio")]
    public AudioClip boomSFX;
    public AudioSource sfxSource;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onDie += HealthSystem_onDie;
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);    // Aplica movimiento constante en el eje X  
    }

    private void HealthSystem_onDie()
    {
        // Genera efecto visual si está asignado
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);    // Destruye el efecto tras un breve tiempo
        }
        Destroy(gameObject);    // Elimina al enemigo de la escena
    }
}
