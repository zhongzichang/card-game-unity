using System;
using UnityEngine;

namespace TangLevel
{
  public class MaterialBhvr : MonoBehaviour
  {
    private Renderer[] renderers;
    private Material[] sourceMats;
    private Color[] colors;
    private static readonly Color DEFAULT_COLOR = Color.white;
    private Color mColor = DEFAULT_COLOR;

    #region Properties

    public Color color {
      get {
        return mColor;
      }
      set {
        mColor = value;
        for (int i = 0; i < sourceMats.Length; i++) {
          sourceMats [i].color = mColor;
        }
      }
    }

    #endregion

    #region MonoMethods

    void OnEnable ()
    {
      if (sourceMats == null) {
        renderers = GetComponentsInChildren<Renderer> ();
        sourceMats = new Material[renderers.Length];
        colors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++) {
          sourceMats [i] = renderers [i].material;
          colors [i] = renderers [i].material.color;
        }
      }
    }

    void OnDisable ()
    {
      RestoreColor ();
    }

    #endregion

    #region PublicMethods

    /// <summary>
    /// 还原颜色
    /// </summary>
    public void RestoreColor ()
    {
      if (renderers != null && sourceMats != null && colors != null) {
        for (int i = 0; i < renderers.Length && i < sourceMats.Length; i++) {
          sourceMats [i].color = colors [i];
        }
        mColor = DEFAULT_COLOR;
      }
    }

    #endregion
  }
}

