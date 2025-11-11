using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement player;
    public PlayerDataSo playerData;

    // Hash optimizado del parámetro "State" del Animator (mejora el rendimiento)
    private static readonly int State = Animator.StringToHash("State");
    enum PlayerState
    {
        Idle = 1,
        Walk = 2,
        Jump = 3,
        Hurt = 4,
    };
    [SerializeField] private PlayerState playerState = PlayerState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.SetInteger(State, (int)playerState);

    }
    private void Update()
    {
        if (Input.GetKey(playerData.keyCodeRight) || Input.GetKey(playerData.keyCodeLeft))
        {
            playerState = PlayerState.Walk;
            animator.SetInteger(State, (int)playerState);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);   // Si no hay movimiento, vuelve a Idle tras 1 segundo
        }

        if (Input.GetKeyDown(playerData.keyCodeJump))
        {
            playerState = PlayerState.Jump;
            animator.SetInteger(State, (int)playerState);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);   // Si no salta, regresa a Idle tras 1 segundo
        }
    }

    // Restaura la animación a estado Idle
    private void ResetAnim()
    {
        playerState = PlayerState.Idle;
        animator.SetInteger(State, (int)playerState);
    }
}
