using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [Header("Scripts")]
    [SerializeField] private PlayerDataSo playerData;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameManager gameManager;

    [Header("Refernces")]
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Movement")]
    [SerializeField] private Animator animator;

    [Header("Jump")]
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector2 boxDimensions;
    [SerializeField] private LayerMask jumpLayers;
    private bool isGrounded;
    [SerializeField] private bool canMoveDuringJump;
    private bool jumpInput;

    [Header("Fire")]
    [SerializeField] private Transform firePoint;

    [Header("Damage")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip throwSFX;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.onDie += HealthSystem_onDie;
    }

    private void Start()
    {
        Debug.Log($"Velocidad del jugador: {playerData.speed}");
    }
    private void Update()
    {
        // Detecta entrada de salto
        if (Input.GetKeyDown(playerData.keyCodeJump))
            jumpInput = true;

        // Verifica si el jugador está tocando el suelo
        isGrounded = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, jumpLayers);

        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    private void FixedUpdate()
    {
        Movement();
        TryJump();
        jumpInput = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && Time.time > lastDamageTime + damageCooldown) // asegúrate de que el enemigo tenga el tag "Enemy"
        {
            healthSystem.DoDamage(damage);
            lastDamageTime = Time.time;// le quita vida al jugador
        }
    }

    private void Movement()
    {
        float moveInput = 0f;

        // Impide moverse si está en el aire (según configuración)
        if (!isGrounded && !canMoveDuringJump) { return; }

        // Detecta entrada de movimiento horizontal
        if (Input.GetKey(playerData.keyCodeLeft))
            moveInput = -1f;
        else if (Input.GetKey(playerData.keyCodeRight))
            moveInput = 1f;

        // Cambia la dirección visual del personaje si corresponde
        if ((moveInput > 0 && !LookingRight()) || (moveInput < 0 && LookingRight()))
        {
            TurnAround();
        }
        rigidBody.velocity = new Vector2(moveInput * playerData.speed, rigidBody.velocity.y);

    }

    private void TurnAround()
    {
        // Invierte la escala en X para voltear el sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool LookingRight()
    {
        // Devuelve si el jugador está mirando hacia la derecha
        return transform.localScale.x == 1;
    }

    private void TryJump()
    {
        // Verifica condiciones de salto antes de ejecutarlo
        if (!jumpInput) { return; }
        if (!isGrounded) { return; }
        Jump();
    }

    private void Jump()
    {
        // Aplica fuerza de salto y reproduce el sonido correspondiente
        jumpInput = false;
        rigidBody.AddForce(Vector2.up * playerData.jumpForce, ForceMode2D.Impulse);
        sfxSource.clip = jumpSFX;
        sfxSource.Play();
    }

    private void OnDrawGizmos()
    {
        // Dibuja en el editor el área de detección del suelo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
    }

    private void Fire()
    {
        // Evita disparar si el clic se hizo sobre un elemento de la UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Instancia y configura la bala
        Bullet bullet = Instantiate(playerData.bulletPrefab);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.layer = LayerMask.NameToLayer("Player");

        // Calcula dirección hacia el puntero del mouse
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bullet.transform.LookAt(targetPos);
        bullet.SetBullet(20, 30);

        // Reproduce sonido de lanzamiento
        sfxSource.clip = throwSFX;
        sfxSource.Play();
    }

    private void HealthSystem_onDie()
    {
        // Reproduce efecto visual y programa la eliminación del jugador
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        Invoke(nameof(HandleDeath), 0.5f);
    }

    private void HandleDeath()
    {
        // Informa al GameManager y destruye el objeto jugador
        gameManager.PlayerDefeated();
        Destroy(gameObject);
        Debug.Log("Murió el player");
    }
}
