using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private GameObject deathEffectPrefab;


    private void Awake ()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onDie += HealthSystem_onDie;
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
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown) // asegúrate de que el enemigo tenga el tag "Enemy"
    //    {
    //        healthSystem.DoDamage(damage);// le quita vida al jugador
    //    }
    //}
}
