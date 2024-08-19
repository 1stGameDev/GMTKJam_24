using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip baseClip;
    [SerializeField]
    private AudioClip loopClip;

    private AudioSource audioSource;
    void Awake()
    {
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
