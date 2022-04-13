using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBulletCollisionBase : MonoBehaviour
{
    private AudioClip impactSoundClip;
    private AudioSource audioSource;

    protected void Awake()
    {
        impactSoundClip = Resources.Load<AudioClip>("Sound/SoundFX/Impact/IMPACT_Bullet_Metal_04_mono");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.impactSoundClip;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!this.audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
