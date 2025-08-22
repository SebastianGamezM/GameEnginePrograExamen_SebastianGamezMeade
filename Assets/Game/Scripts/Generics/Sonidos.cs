using UnityEngine;

[System.Serializable]
public class Sonidos 
{
    public string nombre;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    public bool loop;

    [HideInInspector] public AudioSource audioSource;
}
