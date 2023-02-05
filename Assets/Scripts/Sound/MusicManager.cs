using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  This should be a singleton, so it is common for all scenes.
  It will play the audio loops in queue providing a continuous sound stream
  trough the game but allowing to switch between loops for different scenes or 
  game events.
  When we want to change the music, we call the method changeMusic() and pass
  a list of the new Theme clips and optionally a fill clip to make a smoother
  transition between themes.
*/

public class MusicManager : MonoBehaviour
{

    public static GameObject musicManagerObject = null;
    public AudioClip defaultMusicClip;
    private AudioSource musicSource;
    private AudioSource musicFillSource;

    public List<AudioClip> audioClipList;
    public AudioSource[] audioSourceArray;
    private double nextStartTime;
    private int toggle = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(musicManagerObject == null){
            musicManagerObject = this.gameObject;
        } else if(musicManagerObject != null){
            Destroy(this.gameObject);
        }        
    }

    void Start()
    {
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicFillSource = gameObject.AddComponent<AudioSource>();
        audioSourceArray = GetComponents<AudioSource>();

        audioClipList.Add(defaultMusicClip);
        audioClipList.Add(defaultMusicClip);

        nextStartTime = AudioSettings.dspTime + 0.2;
        musicSource.PlayScheduled(nextStartTime);

        double duration = (double)defaultMusicClip.samples / defaultMusicClip.frequency;
        nextStartTime = nextStartTime + duration;
    }

    void Update()
    {
        if(AudioSettings.dspTime > nextStartTime - 0.5)
        {
            AudioClip clipToPlay = audioClipList[0];

            audioSourceArray[toggle].clip = clipToPlay;
            audioSourceArray[toggle].PlayScheduled(nextStartTime);

            double duration = (double)clipToPlay.samples / clipToPlay.frequency;
            nextStartTime = nextStartTime + duration;

            toggle = 1 - toggle;
            if (audioClipList.Count <= 2) {
                int loop = audioClipList.Count-1;
                audioClipList.Add(audioClipList[loop]);
            }
            audioClipList.RemoveAt(0);
        }
    }

    public void cambioMusica(AudioClip musica, AudioClip musicaFill)
    {   
        audioClipList.Clear();

        if (musicaFill != null)
        {
            audioClipList.Add(musicaFill);
        }
        audioClipList.Add(musica);
    }
}