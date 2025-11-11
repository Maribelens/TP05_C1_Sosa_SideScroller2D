using UnityEngine;

public class Pickable : MonoBehaviour
{
    public enum PickableType { Coin, Diamond, Protection, Health }
    [SerializeField] private HealthSystem health;

    [SerializeField] private PickableType type;
    [SerializeField] private AudioClip pickSound;
    [SerializeField] private GameObject EffectPrefab;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = GetComponent<GameManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem health = collision.GetComponent<HealthSystem>();

            // Efecto visual y sonoro al recoger el objeto
            if (EffectPrefab != null)
            {
                GameObject effect = Instantiate(EffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
            Destroy(gameObject);

            if (pickSound != null)
            {
                AudioSource.PlayClipAtPoint(pickSound, transform.position);
            }

            // Aplica el efecto según el tipo de objeto recogido
            switch (type)
            {
                case PickableType.Coin:
                    gameManager.AddCoins(1);
                    break;

                case PickableType.Diamond:
                    gameManager.AddGems(1);
                    break;
                case PickableType.Health:
                    gameManager.AddHealth();
                    break;
                case PickableType.Protection:
                    gameManager.ActivateProtection();
                    break;
            }
            Debug.Log("Player recogio un objeto");
        }

    }
}

