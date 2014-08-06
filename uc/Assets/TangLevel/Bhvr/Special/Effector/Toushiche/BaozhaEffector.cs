using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class BaozhaEffector : EffectorSpecialBhvr
  {

    public static readonly Vector3 OFFSET = new Vector3 (0F, 0F, -1F);
    public float HURT_WIDTH = 20F;

    private Transform myTransform;

    void Awake ()
    {
      myTransform = transform;
      animator = GetComponent<Animator> ();
    }

    void OnEnable ()
    {

      if (w != null && w.param != null) {

        // 定位到爆炸位置上
        Vector3 pos = (Vector3)w.param;
        if (pos != null) {
          myTransform.position = pos + OFFSET;
        }

        // 寻找一定范围内的敌人，抛出火烧作用器
        // Hit ();
        List<GameObject> gl = HeroSelector.FindEnemiesWithWidth (w.source, pos, HURT_WIDTH);
        SkillBhvr sourceSkillBhvr = w.source.GetComponent<SkillBhvr> ();
        foreach (GameObject g in gl) {
          foreach (Effector e in w.effector.subEffectors) {
            EffectorWrapper cw = EffectorWrapper.W (e, w.skill, w.source, g);
            sourceSkillBhvr.Cast (cw);
          }
        }

      } else {
        StartRelease ();
      }

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;


    }

    void OnDisable ()
    {

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;

    }

    private void OnAnimationEnd ()
    {
      StartRelease ();
    }

    public override void Play ()
    {
      isPlay = true;
      StartPlayOnce ("isPlay");
    }

    public override void Pause ()
    {
      isPlay = false;
      animator.speed = 0;
    }

    public override void Resume ()
    {
      isPlay = true;
      animator.speed = 1;
    }
  }
}
