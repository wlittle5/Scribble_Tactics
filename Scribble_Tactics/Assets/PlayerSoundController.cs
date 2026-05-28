using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClipsRefsSO audioClipRefsSO;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Player.Instance.OnSelected += Player_OnSelected;
        Player.Instance.OnDeSelected += Player_OnDeSelected;
    }

    private void Player_OnDeSelected(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.selectedPlayer, Camera.main.transform.position);
    }

    private void Player_OnSelected(object sender, System.EventArgs e)
    {
        //PlaySound();
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[audioClipArray.Length], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
}
