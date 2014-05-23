using System;
using UnityEngine;

namespace TangLevel
{
  public class FadeOut : MonoBehaviour
  {
    public const float FADE_OUT_TIME = 2f;
    public const float PREFIX = 2;
    private float remain = FADE_OUT_TIME;
    private Renderer[] renderers;
    private Material[] sourceMats;
    private bool isPlaying = false;

    void Update ()
    {

      if (isPlaying) {

        if (remain > 0) {

          remain -= Time.deltaTime;


          if (remain < FADE_OUT_TIME) {

            float alpha = remain / FADE_OUT_TIME;
            if (alpha < 0)
              alpha = 0;
            for (int i = 0; i < sourceMats.Length; i++) {
              sourceMats[i].color = new Color (1f, 1f, 1f, alpha);
            }

          }

        } else {

          GobjManager.Release (gameObject);

          isPlaying = false;
        }

      }

    }

    void OnEnable ()
    {


      if (sourceMats == null) {

        renderers = GetComponentsInChildren<Renderer> ();

        sourceMats = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++) {
          sourceMats [i] = renderers[i].material;
        }
      }
    }

    void OnDisable ()
    {

      for (int i = 0; i < renderers.Length && i < sourceMats.Length; i++) {
        sourceMats[i].color = Color.white;
      }

      isPlaying = false;
    }

    public void Play ()
    {
      isPlaying = true;
      remain = FADE_OUT_TIME + PREFIX;
    }
  }
}

