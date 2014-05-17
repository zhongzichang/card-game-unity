using UnityEngine;
using System.Collections.Generic;
using System;

namespace TangPlace
{
  public class PlaceController : MonoBehaviour
  {
    // 如果位置改变将发出事件通知
    public static EventHandler placeChangedHandler;
    private static Place place = Place.none;
    private static List<Place> history = new List<Place>();
    private static int placeIndex = -1;

    public static Place Place {
      get {
        return place;
      }

      set {
        if (place != value) {
          place = value;
          if (placeChangedHandler != null) {
            placeChangedHandler (null, EventArgs.Empty);
          }
          placeIndex++;
          if (history.Count > placeIndex) {
            history [placeIndex] = place;
            int removeStartIndex = placeIndex + 1;
            int removeCount = history.Count - removeStartIndex;
            if (removeCount > 0) {
              history.RemoveRange (removeStartIndex, removeCount);
            }
          } else {
            history.Add (place);
          }
        }
      }
    }

    /// <summary>
    /// 后退
    /// </summary>
    public static void Back(){
      if (placeIndex > 0) {
        placeIndex--;
        place = history[placeIndex];
        if (placeChangedHandler != null) {
          placeChangedHandler (null, EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// 前进
    /// </summary>
    public static void Forward(){
      if (history.Count - placeIndex >  1) {
        placeIndex++;
        place = history [placeIndex];
        if (placeChangedHandler != null) {
          placeChangedHandler (null, EventArgs.Empty);
        }
      }
    }

    public Place myPlace = Place.none;

    void Awake ()
    {
      placeChangedHandler += OnPlaceChanged;
    }

    void OnDestroy ()
    {
    
      placeChangedHandler -= OnPlaceChanged;

    }

    private void OnPlaceChanged (object sender, EventArgs args)
    {
      if (place == myPlace) {
        gameObject.SetActive (true);	
      } else {
        gameObject.SetActive (false);
      }

    }
  }
}
