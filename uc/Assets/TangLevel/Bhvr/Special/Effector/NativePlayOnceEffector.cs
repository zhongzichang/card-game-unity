using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class NativePlaceOnceEffector : EffectorSpecialBhvr
  {
    // Use this for initialization
    void Start ()
    {
	
    }

    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnEnable(){

    }


    public override void Play ()
    {
      isPlay = true;
      this.enabled = true;
    }

    public override void Pause ()
    {
      isPlay = false;
    }

    public override void Resume ()
    {
      isPlay = true;
    }
  }
}
