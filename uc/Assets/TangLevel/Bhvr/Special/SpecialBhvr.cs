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

    public virtual void Release ()
    {
      OnRelease ();
      GobjManager.Release (gameObject);
    }

    public abstract void Play ();

    #endregion

    #region ProtectedMethods

    protected void StartPlayOnce(string paramName){
      StartCoroutine (PlayOnce (paramName));
    }

    /// <summary>
    /// 开始施放，不是马上，而是等当前帧计算完毕之后
    /// </summary>
    protected void StartRelease(){
      StartCoroutine (WaitRelease());
    }

    #endregion

    #region PrivateMethods

    /// <summary>
    /// 播放一次
    /// </summary>
    /// <returns>The once.</returns>
    /// <param name="paramName">Parameter name.</param>
    private IEnumerator PlayOnce (string paramName)
    {
      if (animator != null) {
        animator.SetBool (paramName, true);
        yield return null;
        animator.SetBool (paramName, false);
      }
    }

    private IEnumerator WaitRelease ()
    {
      yield return new WaitForEndOfFrame ();
      Release();
    }
     
    #endregion


    #region LevelControllerCallback

    protected virtual void OnPause (object sender, EventArgs args)
    {
      Pause ();
    }

    protected virtual void OnResume (object sender, EventArgs args)
    {
      Resume ();
    }

    /// <summary>
    /// 在 Disable 之前被调用
    /// </summary>
    protected virtual void OnRelease(){

    }

    #endregion
  }
}