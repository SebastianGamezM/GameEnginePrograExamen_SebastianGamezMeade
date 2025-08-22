using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sonidos[] sonidos;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sonidos s in sonidos)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string nombre)
    {
        foreach (Sonidos s in sonidos)
        {
            if (s.nombre == nombre)
            {
                s.audioSource.Play();
                return;
            }
        }
    }

    public void Stop(string nombre)
    {
        foreach (Sonidos s in sonidos)
        {
            if (s.nombre == nombre)
            {
                s.audioSource.Stop();
                return;
            }
        }
    }
}