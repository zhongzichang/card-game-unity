using UnityEngine;
using System.Collections;
using System;

public class PlaceController : MonoBehaviour {

  // 如果位置改变将发出事件通知
  public static EventHandler placeChangedHandler;

  private static Place place;
  public static Place Place
  {
    get
      {
	return place;
      }

    set
      {
	if( place != value )
	  {
	    place = value;
	    if( placeChangedHandler != null)
	      {
		placeChangedHandler(null, EventArgs.Empty);
	      }
	  }
      }
  }

  public Place myPlace = Place.none;

  void Start()
  {
    placeChangedHandler += OnPlaceChanged;
  }

  void OnDestroy()
  {
    
    placeChangedHandler -= OnPlaceChanged;

  }

  private void OnPlaceChanged(object sender, EventArgs args)
  {
    if( place == myPlace )
      {
	gameObject.SetActive(true);	
      }
    else
      {
	gameObject.SetActive(false);
      }

  }

}
