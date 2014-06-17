using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场显示项
  /// </summary>
  public class ArenaItem : ViewItem {

    public UISprite icon;
    public UILabel levelLabel;
    public UILabel nameLabel;
    public UILabel rankingValueLabel;
    public UILabel battleValueLabel;
    public UIEventListener btn;
    public UILabel btnLabel;

  }
}