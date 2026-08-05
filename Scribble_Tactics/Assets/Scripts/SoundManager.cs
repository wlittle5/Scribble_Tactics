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
        Player.Instance.OnSelected += Player_OnSelected;
        Player.Instance.OnDeSelected += Player_OnDeSelected;
        EnemyLogic.Instance.EnemyOnSelected += EnemyLogic_EnemyOnSelected;
        EnemyLogic.Instance.EnemyOnDeSelected += EnemyLogic_EnemyOnDeSelected;
    }

    private void EnemyLogic_EnemyOnDeSelected(object sender, System.EventArgs e)
    {
        EnemyLogic enemy = EnemyLogic.Instance;
        PlaySound(audioClipRefsSO.deselectedPlayer, enemy.transform.position);
    }

    private void EnemyLogic_EnemyOnSelected(object sender, System.EventArgs e)
    {
        EnemyLogic enemy = EnemyLogic.Instance;
        PlaySound(audioClipRefsSO.selectedPlayer, enemy.transform.position);
    }

    private void Player_OnDeSelected(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.deselectedPlayer, player.transform.position);
    }

    private void Player_OnSelected(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.selectedPlayer, player.transform.position);
    }

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
