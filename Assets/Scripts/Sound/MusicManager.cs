using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/**
  This should be a singleton, so it is common for all scenes.
  It will play the audio loops in queue providing a continuous sound stream
  trough the game but allowing to switch between loops for different scenes or 
  game events.
  When we want to change the music, we call the method ChangeMusic() and pass
  a list of the new Theme clips and optionally a fill clip to make a smoother
  transition between themes.
*/

public class MusicManager : MonoBehaviour
{

    public static GameObject musicManagerObject = null;
    public AudioMixerGroup musicMixerGroup;
    public AudioClip defaultMusicClip;
    private AudioSource musicSource;
    private AudioSource musicFillSource;

    public List<AudioClip> audioClipList;
    public AudioSource[] audioSourceArray;
    private double nextStartTime;
    private int toggle = 0;
    private int clipsInLoop = 1;

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

        foreach (AudioSource source in audioSourceArray)
        {
            source.outputAudioMixerGroup = musicMixerGroup;
        }

        if(defaultMusicClip == null && audioClipList.Count == 0)
        {
            Debug.LogError("ERROR: No music clips implemented for MusicManager.");
        } 
        else if (defaultMusicClip != null && audioClipList.Count == 0)
        {
            audioClipList.Add(defaultMusicClip);
        } 
        else if (audioClipList.Count > 0)
        {
            Debug.Log("Clips ready to play.");
        } else {
            Debug.LogError("ERROR: Something went wrong with the music clips.");
        }

        nextStartTime = AudioSettings.dspTime + 0.2;
        musicSource.PlayScheduled(nextStartTime);

        if(defaultMusicClip != null)
        {
          double duration = (double)defaultMusicClip.samples / defaultMusicClip.frequency;
          nextStartTime = nextStartTime + duration;
        }
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
            if (audioClipList.Count <= clipsInLoop) {
                audioClipList.Add(audioClipList[0]);
  
            }
            audioClipList.RemoveAt(0);
        }
    }

    public void ChangeMusic(List<AudioClip> musica, AudioClip musicaFill = null)
    {   
        audioClipList.Clear();

        clipsInLoop = musica.Count;

        if (musicaFill != null)
        {
            audioClipList.Add(musicaFill);
        }

        foreach (AudioClip clip in musica)
        {
            audioClipList.Add(clip);
        }
    }
}