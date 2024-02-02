using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex;

    public bool hasSwitched=false;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Il existe plusieurs instance de AudioManager");
            Destroy(gameObject);
            Debug.Log("autres instances détruites");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        musicIndex = 0;
        audioSource.Play();
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextSong();
        }
        else
        {
            if(CurrentSceneManager.instance.switchMusic && !hasSwitched)
            {
                hasSwitched = true;
                PlayNextSong();
            }
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        if (IsMusicIndexPresent(musicIndex,CurrentSceneManager.instance.indexMusic))
        {
            audioSource.clip = playlist[musicIndex];
            audioSource.Play();
        }
        else 
        {
            PlayNextSong();
        }

    }

    bool IsMusicIndexPresent(int musicIndex, int[] indexMusic)
    {
        HashSet<int> indexSet = new HashSet<int>(indexMusic);
        return indexSet.Contains(musicIndex);
    }

}
