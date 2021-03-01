using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TemporaryAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public AudioClip audio;
    public string destination;
    public float pitch;
    public float volume;
    
    private AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = audio;
        source.outputAudioMixerGroup = mixer.FindMatchingGroups(destination)[0];
        source.pitch = pitch;
        source.volume = volume;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
