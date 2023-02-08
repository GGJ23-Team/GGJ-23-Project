using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  This is a Component that can set a playlist (playOnceList) when 
  a scene is loaded or called from another script using the method
  SetOnceListMusic(). When the last clip of the playlist is played, 
  the method SetLoopListMusic() is called to set a new playlist (loopList)
   in loop-mode.

  * Load the audio clips in order of play in playOnceList and loopList.
  * Optionaly load an audio fill clip to make a smoother transition between themes.
  * This component needs a MusicManager component in the scene.
  * Set the name of the MusicManager GameObject in the inspector (default: MusicManager).
*/

public class LoopAfterListMusic : MonoBehaviour
{
    public List<AudioClip> playOnceList;
    public List<AudioClip> loopList;
    public AudioClip fillClip;
    public string musicManagerGO = "MusicManager";
    public bool changeOnStart = true;
    private MusicManager musicManager;
    
    void Start()
    {
        musicManager = GameObject.Find(musicManagerGO).GetComponent<MusicManager>();
        musicManager.onLastClip.AddListener(SetLoopListMusic);
        musicManager.onLoopEnd.AddListener(Print);

        if (changeOnStart)
        {
            SetOnceListMusic();
        } 
    }

    public void Print()
    {
      Debug.Log("Print");
    }

    public void SetOnceListMusic()
    {
        musicManager.Loop(false);
        musicManager.ChangeMusic(playOnceList);
    }
    public void SetLoopListMusic()
    {
        musicManager.Loop(true);
        musicManager.ChangeMusic(loopList);
    }
}

