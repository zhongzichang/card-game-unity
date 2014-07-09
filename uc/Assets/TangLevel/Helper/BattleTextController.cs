using System;
using TG = TangGame;
using UnityEngine;
using PureMVC.Core;
using PureMVC.Patterns;

namespace TangLevel
{
  public class BattleTextController
  {

    public delegate void BubblingHandler(TG.BattleTxt text);

    public static BubblingHandler RaiseBubbing;

    /// <summary>
    /// 战斗冒字
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="crit">If set to <c>true</c> crit.</param>
    /// <param name="value">Value.</param>
    /// <param name="self">If set to <c>true</c> self.</param>
    /// <param name="screenPos">Screen position.</param>
    public static void Bubbling (TG.BattleTxtType type, int value, Vector3 screenPos, bool crit, bool self)
    {
      TG.BattleTxt battleTxt = new TG.BattleTxt ();
      battleTxt.type = type;
      battleTxt.value = value;
      battleTxt.crit = crit;
      battleTxt.self = self;
      battleTxt.position = screenPos;
      Facade.Instance.SendNotification (TG.BattleCommand.BattleTxt, battleTxt);

      if (RaiseBubbing != null) {
        RaiseBubbing (battleTxt);
      }

    }

    public static void Bubbling (TG.BattleTxt battleTxt){
      Facade.Instance.SendNotification (TG.BattleCommand.BattleTxt, battleTxt);
      if (RaiseBubbing != null) {
        RaiseBubbing (battleTxt);
      }
    }
  }
}

