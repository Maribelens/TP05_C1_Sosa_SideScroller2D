using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;
    public PlayerDataSo playerData;
    private static readonly int State = Animator.StringToHash("State");
    enum PlayerState
    {
        Idle = 1,
        RightWalk = 2,
        LeftWalk = 3,
        Jump = 4,
        Duck = 5,
        Hurt = 6,
        Die = 7
    };
    [SerializeField] private PlayerState playerState = PlayerState.Idle;

    //Walk, Jump, Slide, Attack, Hurt, Die

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
        if (Input.GetKey(playerData.keyCodeRight))
        {
            playerState = PlayerState.RightWalk;
            animator.SetInteger(State, (int)playerState);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);
        }

        if (Input.GetKey(playerData.keyCodeLeft))
        {
            playerState = PlayerState.LeftWalk;
            animator.SetInteger(State, (int)playerState);
            //Invoke(nameof(ResetAnim), 1);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);
        }

        if (Input.GetKeyDown(playerData.keyCodeJump))
        {
            playerState = PlayerState.Jump;
            animator.SetInteger(State, (int)playerState);
            //Invoke(nameof(ResetAnim), 1);
        }
        else
        {
            Invoke(nameof(ResetAnim), 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerState = PlayerState.Duck;
            animator.SetInteger(State, (int)playerState);
            Invoke(nameof(ResetAnim), 1);
        }
    }

    private void ResetAnim()
    {
        playerState = PlayerState.Idle;
        animator.SetInteger(State, (int)playerState);
    }
}
