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
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);  
    }

    private void HealthSystem_onDie()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        Destroy(gameObject);
    }
}
