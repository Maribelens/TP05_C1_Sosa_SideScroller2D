using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    private Rigidbody2D rigidBody;
    [SerializeField] private GameObject effectPrefab;

    [Header("Audio")]
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioSource sfxSource;

    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }
    public void SetBullet (int speed, int damage)
    {
        // Activa la física y la velocidad en la direccion de la bala
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto tiene un sistema de salud, aplica daño
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.DoDamage(damage);
        }

        // Si impacta contra un enemigo, genera efecto y destruye la bala
        if (other.CompareTag("Enemy"))
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
        }
    }
}