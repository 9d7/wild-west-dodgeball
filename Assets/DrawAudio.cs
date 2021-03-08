using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAudio : MonoBehaviour
{
    [SerializeField] private GameObject tempAudio;
    [SerializeField] private AudioClip gunshot;

    public void DoDrawAudio()
    {
        GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.GetComponent<AudioSource>().spatialBlend = 0.0f;
         temp.audio = gunshot;
         temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
         temp.volume = 1.0f;
         temp.destination = "Player";
    }
}
