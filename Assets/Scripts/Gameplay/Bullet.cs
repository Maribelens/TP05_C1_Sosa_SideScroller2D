using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] GameObject bulletPrefab;
    [SerializeField]private int damage = 20;

    private Rigidbody2D rigidBody;

    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Set (int speed, int damage)
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.DoDamage(damage);
        }
    }
}