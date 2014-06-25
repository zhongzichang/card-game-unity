using System;
using UnityEngine;
using TDB = TangDragonBones;
using DB = DragonBones;
using TA = TangAudio;
using System.Collections.Generic;
using TangUtils;

namespace TangLevel
{
  public class AudioBhvr : MonoBehaviour
  {

    private AudioSource[] audioSrcs;

    void OnEnable ()
    {
      if (audioSrcs == null) {
        audioSrcs = GetComponents<AudioSource> ();
      }
      DB.Events.SoundEventManager.Instance.AddEventListener (DB.Events.SoundEvent.SOUND, OnSoundEvent);
      TA.AudioManager.RaiseLoadEvent += OnLoadHandler;
    }

    void OnDisable ()
    {
      DB.Events.SoundEventManager.Instance.RemoveEventListener (DB.Events.SoundEvent.SOUND, OnSoundEvent);
      TA.AudioManager.RaiseLoadEvent -= OnLoadHandler;
    }


    private void OnSoundEvent (Com.Viperstudio.Events.Event e)
    {

      DB.Events.SoundEvent se = e as DB.Events.SoundEvent;
      string name = TextUtil.RemoveExtName(se.Sound);

      AudioClip audio = TA.AudioManager.Fetch (name);
      if (audio != null) {
        // 播放
        PlayOneShot (audio);

      } else {
        // load it
        TA.AudioManager.LazyLoad (name);
      }
    }


    private void OnLoadHandler (object sender, TA.LoadResultEventArgs args)
    {
      string name = args.Name;
      if (args.Success) {
        AudioClip audio = TA.AudioManager.Fetch (name);
        if (audio != null) {
          // 播放
          PlayOneShot (audio);
        }
      }
    }

    private void PlayOneShot (AudioClip audio)
    {

      bool enough = false;
      if (audioSrcs != null && audioSrcs.Length > 0) {
        foreach (AudioSource src in audioSrcs) {
          if (!src.isPlaying) {
            src.PlayOneShot (audio);
            enough = true;
          }
        }
      }
      if (!enough) {
        AudioSource src = gameObject.AddComponent<AudioSource> ();
        audioSrcs = gameObject.GetComponents<AudioSource> ();
        src.playOnAwake = false;
        src.PlayOneShot (audio);
      }

    }
  }
}

