using System;
using UnityEngine;

namespace TangLevel
{
  public abstract class SpecialBhvr : MonoBehaviour
  {

    protected bool isPlay = false;

    #region PublicMethods
    public virtual void Pause ()
    {
      isPlay = false;
    }

    public virtual void Resume ()
    {
      isPlay = true;
    }

    public virtual void Release(){
      GobjManager.Release (gameObject);
    }

    public abstract void Play();
    #endregion

    #region LevelControllerCallback
    protected virtual void OnPause(object sender, EventArgs args){
      Pause ();
    }
    protected virtual void OnResume(object sender, EventArgs args){
      Resume ();
    }
    #endregion



  }
}