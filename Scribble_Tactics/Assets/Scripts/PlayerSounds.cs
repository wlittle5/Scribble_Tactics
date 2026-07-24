using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] float volume = 20f;

    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            
            if (player.IsMoving())
            {
                SoundManager.Instance.PlayFootsteps(player.transform.position, volume);
            }

        }
    }
}
