using UnityEngine;
using System.Collections;

namespace TangSpecial
{
  public class SpecialTest : MonoBehaviour
  {

    public GameObject source;
    public GameObject target;

    private string spriteName = "Sprite_binghua";


    // Use this for initialization
    void Start ()
    {

      GobjManager.RaiseLoadEvent += OnBingHuaLoad;
      GobjManager.LazyLoad (spriteName);
	
    }

    private void OnBingHuaLoad (object sender, LoadResultEventArgs args)
    {
      GobjManager.RaiseLoadEvent -= OnBingHuaLoad;
      if (args.Success) {
        GameObject gobj = GobjManager.FetchUnused (spriteName);

        SpecialBhvr bhvr = gobj.GetComponent<SpecialBhvr> ();
        if (bhvr != null) {
          bhvr.source = source;
          bhvr.target = target;
        }
        gobj.SetActive (true);
      }
    }
  }
}