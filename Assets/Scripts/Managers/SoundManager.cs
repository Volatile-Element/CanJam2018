using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    GameObject SoundsContainer;

    private void Awake()
    {
        if (!IsMainInstance())
        {
            return;
        }

        SoundsContainer = new GameObject("Sounds Container");
    }

    public void PlaySingleFireRandom(string resourceFolder, float range, float volume = 0.5f, bool is3D = false)
    {
        var clips = Resources.LoadAll<AudioClip>(resourceFolder);
        var clip = clips.Skip(Random.Range(0, clips.Length - 1)).FirstOrDefault();

        PlaySound(clip, false, range, volume, is3D);
    }

    public void PlayLoopRandom(string resourceFolder, float range, float volume = 0.5f)
    {
        var clips = Resources.LoadAll<AudioClip>(resourceFolder);
        var clip = clips.Skip(Random.Range(0, clips.Length - 1)).FirstOrDefault();

        PlaySound(clip, true, range, volume);
    }

    public void PlaySingleFire(string resourceLocation, float volume = 0.5f)
    {
        PlaySound(resourceLocation, false, volume);
    }

    public void PlayLoop(string resourceLocation, float range = 1, float volume = 0.5f)
    {
        PlaySound(resourceLocation, true, range, volume);
    }

    private void PlaySound(string resourcesLocation, bool loop, float range, float volume = 0.5f)
    {
        var audioClip = Resources.Load<AudioClip>(resourcesLocation);

        PlaySound(audioClip, loop, range, volume);
    }

    public void PlaySound(AudioClip clip, bool loop, float range, float volume = 0.5f, bool is3D = false)
    {
        var sound = new GameObject($"Sound: {clip.name} - Created At: {Time.realtimeSinceStartup}");
        sound.transform.parent = SoundsContainer.transform.parent;

        var audioSource = sound.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = true;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.maxDistance = range;
        audioSource.spatialBlend = is3D ? 1 : 0;

        if (!loop)
        {
            var destroySelf = sound.AddComponent<DestroySelfIn>();
            destroySelf.SecondsToLive = clip.length + 0.5f; //A buffer
        }

        audioSource.Play();
    }
}