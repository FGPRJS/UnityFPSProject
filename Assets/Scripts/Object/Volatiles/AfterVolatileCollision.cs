using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterVolatileCollision : MonoBehaviour
{
    private AudioClip impactSoundClip;
    private AudioSource audioSource;

    void Awake()
    {
        this.impactSoundClip = Resources.Load<AudioClip>("Sound/SoundFX/Impact/IMPACT_Bullet_Metal_04_mono");
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.impactSoundClip;

        this.AfterAwake();
    }

    protected void AfterAwake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource.Play();
        this.AfterStart();
    }

    protected void AfterStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }

        AfterUpdate();
    }

    protected void AfterUpdate()
    {

    }

    
}
