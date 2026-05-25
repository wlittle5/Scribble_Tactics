using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Player player;
    
    private Animator animator;

    private const string IS_SELECTED = "IsSelected";
    private const string IS_MOVING = "IsMoving";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_SELECTED, player.IsSelected());
        animator.SetBool(IS_MOVING, player.IsMoving());
    }
}
