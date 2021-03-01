using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private GameObject tempAudio;
    [SerializeField] private float movementPerFootstep;
    [SerializeField] private float timePerIdle = -1f;

    [SerializeField] private List<AudioClip> idleSounds;
    [SerializeField] private List<AudioClip> footstepSounds;
    [SerializeField] private List<AudioClip> deathSounds;
    [SerializeField] private List<AudioClip> throwSounds;

    private Vector3 lastPosition;
    private float movementAmt;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        movementAmt = 0f;
        timer = 0f;
        if (timePerIdle > 0f)
        {
            timer = UnityEngine.Random.Range(0f, timePerIdle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
         timer += Time.deltaTime;
         while (idleSounds.Count > 0 && timePerIdle > 0f && timer > timePerIdle)
         {
             timer -= timePerIdle;
             GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
             TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
             temp.audio = idleSounds[UnityEngine.Random.Range(0, idleSounds.Count)];
             temp.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
             temp.volume = 1f;
             temp.destination = "Enemy";
         }

         movementAmt += (transform.position - lastPosition).magnitude;
         lastPosition = transform.position;
         while (movementPerFootstep > 0f && footstepSounds.Count > 0 && movementAmt > movementPerFootstep)
         {
             movementAmt -= movementPerFootstep;
             GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
             TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
             temp.audio = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Count)];
             temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
             temp.volume = 1f;
             temp.destination = "Environment";
         }

    }

    void EnemyShoot()
    {
        if (throwSounds.Count == 0) return;
        GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
        TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
        temp.audio = throwSounds[UnityEngine.Random.Range(0, throwSounds.Count)];
        temp.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        temp.volume = 1f;
        temp.destination = "Enemy";
    }

    void EnemyDie()
    {
        if (deathSounds.Count == 0) return;
        GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
        TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
        temp.audio = deathSounds[UnityEngine.Random.Range(0, deathSounds.Count)];
        temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        temp.volume = 1f;
        temp.destination = "Enemy";
    }
}
