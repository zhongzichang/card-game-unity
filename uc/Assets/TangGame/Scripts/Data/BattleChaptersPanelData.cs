using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  public enum StageType{
    /// 没什么
    None,
    /// 新Stage
    New,
    /// 掉落指引
    Guide
  }

  /// <summary>
  /// BattleChapterPanel 使用的数据
  /// </summary>
  public class BattleChaptersPanelData {

    /// 当前章节
    public int chapter;
    /// 当前章节下的Stage
    public int stage;
    /// 当前打开面板下，指定stage的附加动画类型，比如新Stage，或者掉落指引的动画
    public StageType type = StageType.New;
  }
}