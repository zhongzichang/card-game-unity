using UnityEngine;
using System.Collections;

namespace TangSpecial
{
  public class SpecialTest : MonoBehaviour
  {
    private string spriteName = "Sprite_binghua";
    // Use this for initialization
    void Start ()
    {

      GobjManager.RaiseLoadEvent += OnBingHuaLoad;
      GobjManager.LazyLoad (spriteName);
	
    }
    // Update is called once per frame
    void Update ()
    {
	
    }

    private void OnBingHuaLoad (object sender, LoadResultEventArgs args)
    {
      GobjManager.RaiseLoadEvent -= OnBingHuaLoad;
      if (args.Success) {
        GameObject gobj = GobjManager.FetchUnused (spriteName);
        gobj.SetActive (true);
      }
    }
  }
}