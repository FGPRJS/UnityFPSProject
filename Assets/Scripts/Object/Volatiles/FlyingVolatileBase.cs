using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingVolatileBase : MonoBehaviour
{
    private AudioClip impactSoundClip;
    private AudioSource audioSource;
    private Rigidbody rigidbody;

    protected void Awake()
    {
        this.impactSoundClip = Resources.Load<AudioClip>("Music/SoundFX/Impact/IMPACT_Bullet_Metal_01_mono");
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.impactSoundClip;
        this.rigidbody = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        
    }

    protected void OnCollisionEnter(Collision collision)
    {
        this.audioSource.Play();

        //Temp
        Object.Destroy(this);
    }

    protected void Update()
    {
        this.transform.position += (this.transform.forward - new Vector3(0, GlobalConfigs.Instance.Gravity, 0)) * Time.deltaTime;
    }
}
