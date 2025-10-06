using UnityEngine;

public class Pickable : MonoBehaviour
{
    public enum PickableType { Coin, Diamond, Protection }

    [SerializeField] private PickableType type;
    [SerializeField] private AudioClip pickSound;
    [SerializeField] private GameObject EffectPrefab;
    [SerializeField] private GameManager gmManager;
    //public enum PickableType { Health, Stamina, PowerUp }
    //public PickableType type;
    //public int amount = 10;
    //public GameObject pickupEffect; // Partículas o feedback visual
    //public AudioClip pickupSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (EffectPrefab != null)
            {
                GameObject effect = Instantiate(EffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
            Destroy(gameObject);
        }
        if (pickSound != null)
        {
            AudioSource.PlayClipAtPoint(pickSound, transform.position);
            
        }

        switch (type)
        {
            case PickableType.Coin:
                gmManager.AddCoins(1);
                break;

            case PickableType.Diamond:
                gmManager.AddGems(1);
                break;

            case PickableType.Protection:
                gmManager.ActivateShield(5f); // 5 seg de protección
                break;
        }
        Debug.Log("Player recogio un objeto");
    }
}

