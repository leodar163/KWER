using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SonManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSourceBase;

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JouerSon(AudioClip clip)
    {
        audioSourceBase.clip = clip;
        audioSourceBase.Play();
    }

    public void JouerSon(List<AudioClip> clips)
    {
        int alea = Random.Range(0, clips.Count - 1);

        JouerSon(clips[alea]);
    }

    public void JouerSon(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }

    public void JouerSon(List<AudioClip> clips, AudioSource source)
    {
        int alea = Random.Range(0, clips.Count - 1);

        JouerSon(clips[alea], source);
    }
}
