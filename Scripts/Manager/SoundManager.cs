using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private AudioClip criticalSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //SoundManager Inspector창에서 BGM 넣은 후에 주석 해제할 것.
        audioSource.clip = BGM;
        audioSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayerAttackSound()
    {
        audioSource.PlayOneShot(attackSFX);
    }

    public void CriticalAttackSound()
    {
        audioSource.PlayOneShot(criticalSFX);
    }
}
