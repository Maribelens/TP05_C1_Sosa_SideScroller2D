using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("State");
    public enum PlayerState
    {
        Idle = 1,
        Walk = 2,
        Jump = 3,
        Falling = 3,
        Attack = 4,
        // Run = 5,
        // Dash = 6,
        // ... = 7,
        Die = 99
    }
    public enum JumpState
    {
        None,
        Jumping,
        Falling
    }


    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private PlayerDataSo playerData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UiElements uiGameOver;
    [SerializeField] private Transform firePoint;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D rigidBody;
    private bool isGrounded;
    private float jumpForce = 10f;
    public float speed = 5f;

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private int damage = 20;
    [SerializeField] private float damageCooldown = 1f;
    private float lastDamageTime;

    [Header ("Audio")]
    public AudioClip jumpSFX;
    public AudioClip slideSFX;
    public AudioClip crunchSFX;
    public AudioSource sfxSource;

    [SerializeField] Animator animator;

    private PlayerState playerState = PlayerState.Idle;
    //[SerializeField] private GameManager gmManager;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (healthSystem == null)
        {
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.onDie += HealthSystem_onDie;
        }
        if (healthSystem != null)
        {
            healthSystem.onDie += HealthSystem_onDie;
        }
        else
        {
            Debug.LogError("HealthSystem no encontrado en el Player.");
        }

        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        if(gameManager == null)
        {
            gameManager = GetComponent<GameManager>();
        }

    }

    private void Start()
    {
        animator.SetInteger(State, (int)playerState);
    }

    private void Update()
    {
        Movement();
        TryJump();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            //jumpState = JumpState.None;
        }
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown) // asegúrate de que el enemigo tenga el tag "Enemy"
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;// le quita vida al jugador
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
    private void Movement()
    {
        float moveInput = 0f;

        if (Input.GetKey(playerData.keyCodeLeft))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(playerData.keyCodeRight))
        {
            moveInput = 1f;
        }

        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
    }

    private void TryJump()
    {
        if (isGrounded && Input.GetKeyDown(playerData.keyCodeJump))
        {
            //animator.SetInteger(State, (int)playerState);
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            sfxSource.clip = jumpSFX;
            sfxSource.Play();
            isGrounded = false;
        }
    }

    private void Fire()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // Para saber si le pego a un objeto de UI
            return;

        Bullet bullet = Instantiate(playerData.bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.layer = LayerMask.NameToLayer("Player");

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bullet.transform.LookAt(targetPos);
        bullet.Set(20, 30);
    }

    private void HealthSystem_onDie()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        Destroy(gameObject);
        gameManager.PlayerDefeated();
        Debug.Log("Murió el player");
    }

}
