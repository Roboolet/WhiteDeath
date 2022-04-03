using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLib : MonoBehaviour
{
    public static SFXLib current;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioData[] _lib;
    Dictionary<string, AudioClip> lib = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        current = this;
        foreach(AudioData d in _lib)
        {
            lib.Add(d.name, d.clip);
        }
    }

    public void Play(string name)
    {
        if (lib.ContainsKey(name))
        {
            audioSource.PlayOneShot(lib[name], 1);
        }
    }

}

[System.Serializable]
public struct AudioData
{
    public string name;
    public AudioClip clip;
}
