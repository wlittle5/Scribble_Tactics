using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipsRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Player.OnPlayerSelect += Player_OnPlayerSelect;
        Player.OnBattleInitiate += Player_OnBattleInitiate;
        
        EnemyLogic.OnEnemySelect += EnemyLogic_OnEnemySelect;
    }

    private void EnemyLogic_OnEnemySelect(object sender, EnemyLogic.OnEnemySelectArgs e)
    {
        EnemyLogic enemy = e.enemy;
        
        if (e.isSelected)
            PlaySound(audioClipRefsSO.selectedPlayer, enemy.transform.position);

        if (!e.isSelected)
            PlaySound(audioClipRefsSO.deselectedPlayer, enemy.transform.position);
    }

    private void Player_OnPlayerSelect(object sender, Player.OnPlayerSelectArgs e)
    {
        Player player = e.player;

        if (e.isSelected)
            PlaySound(audioClipRefsSO.selectedPlayer, player.transform.position);

        if(!e.isSelected)
            PlaySound(audioClipRefsSO.deselectedPlayer, player.transform.position);
    }

    private void Player_OnBattleInitiate(object sender, Player.OnBattleInitiateArgs e)
    {
        Player player = e.player;
        PlaySound(audioClipRefsSO.knightBattleInitiate, player.transform.position);
    }

    //-----------The sound playing functions themselves----------
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootsteps(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.knightFootstep, position, volume);
    }
    
}
