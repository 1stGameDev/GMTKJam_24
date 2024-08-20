using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScript : MonoBehaviour
{

    private static SongScript instance = null;

    [SerializeField]
    private AudioClip baseClip;
    [SerializeField]
    private AudioClip loopClip;

    private AudioSource audioSource;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // If another instance of this MusicManager exists, destroy it
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = baseClip;
        audioSource.Play();
        StartCoroutine(WaitForAudioToFinish());
    }


    private IEnumerator WaitForAudioToFinish()
    {
        // Wait for the clip's length
        yield return new WaitForSeconds(baseClip.length);
        audioSource.clip = loopClip;
        audioSource.Play();
        audioSource.loop = true;
    }
}
