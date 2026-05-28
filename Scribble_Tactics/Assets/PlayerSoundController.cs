using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    private AudioSource selectSound;
    private Player player;

    private bool audioToggle = false;

    void Awake()
    {
        selectSound = GetComponent<AudioSource>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        audioToggle = player.IsSelected();

        if (audioToggle == true)
        {
            selectSound.Play();
            audioToggle = false;
        }
    }
}
