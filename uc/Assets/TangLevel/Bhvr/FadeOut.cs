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
    private Material[] fadeOutMats;
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
            for (int i = 0; i < fadeOutMats.Length; i++) {
              fadeOutMats[i].color = new Color (1f, 1f, 1f, alpha);
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


      if (fadeOutMats == null) {

        renderers = GetComponentsInChildren<Renderer> ();

        fadeOutMats = new Material[renderers.Length];
        sourceMats = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++) {

          fadeOutMats [i] = new Material (renderers[i].material);
          fadeOutMats [i].color = Color.white;
          sourceMats [i] = renderers[i].material;
          renderers[i].material = fadeOutMats [i];

        }
      }
    }

    void OnDisable ()
    {

      for (int i = 0; i < renderers.Length && i < sourceMats.Length; i++) {
        renderers [i].material = sourceMats [i];
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

