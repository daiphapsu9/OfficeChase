using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip playerHitSound, shootSound, pickupSound, typingSound,stunSound, putSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("hurt");
        shootSound = Resources.Load<AudioClip>("shoot");
        pickupSound = Resources.Load<AudioClip>("pickup");
        typingSound = Resources.Load<AudioClip>("typing");
        stunSound = Resources.Load<AudioClip>("stunned");
        putSound = Resources.Load<AudioClip>("put");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        if (audioSrc != null) {
            switch (clip)
            {
                case "hurt":
                    audioSrc.PlayOneShot(playerHitSound);
                    break;

                case "shoot":
                    audioSrc.PlayOneShot(shootSound);
                    break;

                case "pickup":
                    audioSrc.PlayOneShot(pickupSound);
                    break;

                case "typing":
                    audioSrc.PlayOneShot(typingSound);
                    break;

                case "stun":
                    audioSrc.PlayOneShot(stunSound);
                    break;

                case "put":
                    audioSrc.PlayOneShot(putSound);
                    break;

            }
           
        }
    }
}
