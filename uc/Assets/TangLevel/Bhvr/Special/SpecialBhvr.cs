using System;
using System.Collections;
using UnityEngine;

namespace TangLevel
{
  public abstract class SpecialBhvr : MonoBehaviour
  {

    protected bool isPlay = false;
    protected Animator animator;

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

    #region ProtectedMethods
    public IEnumerator PlayOnce ( string paramName )
    {

      animator.SetBool( paramName, true );
      yield return null;
      animator.SetBool( paramName, false );
    }
    #endregion

  }
}