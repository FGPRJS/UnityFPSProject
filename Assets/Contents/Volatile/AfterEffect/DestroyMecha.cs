using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMecha : MonoBehaviour
{
    private AudioClip impactSoundClip;
    private AudioSource audioSource;

    private void Awake()
    {
        impactSoundClip = UnityEngine.Resources.Load<AudioClip>("Sound/SoundFX/Explosion/Explosion1");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.impactSoundClip;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        this.audioSource.Play();
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
