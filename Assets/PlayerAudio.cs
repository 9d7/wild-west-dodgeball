using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject tempAudio;
    [SerializeField] private float movementPerFootstep;
    [SerializeField] private List<AudioClip> footstepSounds;
    [SerializeField] private List<AudioClip> throwBallAudio;
    [SerializeField] private AudioClip dashAudio;
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private AudioClip throwEmptyAudio;
    [SerializeField] private AudioClip hurtAudio;
    

    private Vector3 lastPosition;
    private float movementAmt;
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         movementAmt += (transform.position - lastPosition).magnitude;
         lastPosition = transform.position;
         while (movementPerFootstep > 0f && footstepSounds.Count > 0 && movementAmt > movementPerFootstep)
         {
             movementAmt -= movementPerFootstep;
             GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
             TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
             temp.audio = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Count)];
             temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
             temp.volume = 0.75f;
             temp.destination = "Environment";
         }
    }
    
    // PickupBallAudio
    public void PickupBallAudio()
    {
         GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.audio = pickupAudio;
         temp.pitch = 1f;
         temp.volume = 1f;
         temp.destination = "Player";
    }
    
    // ThrowEmptyAudio
    public void ThrowEmptyAudio()
    {
         GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.audio = throwEmptyAudio;
         temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
         temp.volume = 1f;
         temp.destination = "Player";
    }
    
    // ThrowBallAudio
    public void ThrowBallAudio()
    {
         GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.audio = throwBallAudio[UnityEngine.Random.Range(0, throwBallAudio.Count)];
         temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
         temp.volume = 1f;
         temp.destination = "Player";
    }
    
    // DashAudio
    public void DashAudio()
    {
         GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.audio = dashAudio;
         temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
         temp.volume = 1f;
         temp.destination = "Player";
    }

    public void PlayerHurtAudio()
    {
         GameObject newSound = Instantiate(tempAudio, transform.position, Quaternion.identity);
         TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
         temp.audio = hurtAudio;
         temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
         temp.volume = 1f;
         temp.destination = "Player";
    }
    
}
