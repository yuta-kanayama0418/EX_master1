using UnityEngine;

public class play_sound : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private bool start_flg = true;
    private AudioSource audiosource;

    // Start is called before the first frame update
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void PlayOneShot()
    {
        if (start_flg)
        {
            Debug.LogWarning("Start!!");
            audiosource.PlayOneShot(music);
            start_flg = false;
        }
    }

    public void Play()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = data_util._sound;
        audioSource.Play();
    }

    public void Stop()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }
}
