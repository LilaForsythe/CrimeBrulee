using UnityEngine;

public class backgroundMusicScript : MonoBehaviour
{
    public static backgroundMusicScript instance = null;
    private AudioSource audio;

    void Awake() //play no matter the scene
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void StopMusic() //for robberystart
    {
        if (audio != null && audio.isPlaying)
        {
            audio.Stop();
        }
    }

    // Optional: call this later if you want the music to start again in another phase
    public void PlayMusic()
    {
        if (audio != null && !audio.isPlaying)
        {
            audio.Play();
        }
    }
}
