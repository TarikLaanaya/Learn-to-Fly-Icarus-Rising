using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private float transitionVolumeSpeed = 0.5f;
    private bool playMusic = true;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!musicSource.isPlaying && playMusic)
        {
            musicSource.PlayOneShot(musicClips[Random.Range(0, musicClips.Length)]);
        }
    }

    public void StopMusic()
    {
        playMusic = false;

        StartCoroutine(DecreaseVolume());
    }

    public void StartMusic()
    {
        playMusic = true;

        StartCoroutine(IncreaseVolume());
    }

    private IEnumerator DecreaseVolume()
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= transitionVolumeSpeed * Time.deltaTime;
            yield return null; // wait until next frame
        }

        musicSource.Stop();
        musicSource.volume = 1f;
    }

    private IEnumerator IncreaseVolume()
    {
        while (musicSource.volume < 1)
        {
            musicSource.volume += transitionVolumeSpeed * Time.deltaTime;
            yield return null; // wait until next frame
        }

        musicSource.volume = 1f;
    }
}
