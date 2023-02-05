using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  This is a Component that can change music when a scene is loaded or
  called from another script using the method ChangeMusic().

  Load the audio clips in order of play in the list audioClipList.
  Optionaly load an audio fill clip to make a smoother transition between themes.
*/

public class ChangeMusic : MonoBehaviour
{

    public List<AudioClip> audioClipList;
    public AudioClip fillClip;
    public string musicManagerName = "MusicManager";
    public bool changeOnStart = true;
    private MusicManager musicManager;
    
    void Start()
    {
        musicManager = GameObject.Find(musicManagerName).GetComponent<MusicManager>();

        if (changeOnStart)
        {
            PerformChangeMusic();
        }   
    }

    public void PerformChangeMusic()
    {
        if (fillClip != null)
        {
            musicManager.ChangeMusic(audioClipList, fillClip);
        } else {
            musicManager.ChangeMusic(audioClipList);
        }
    }
}
