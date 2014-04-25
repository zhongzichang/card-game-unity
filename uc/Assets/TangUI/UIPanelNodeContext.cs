/**
 * UI Panel Node Context
 * Author: zzc
 * Date: 2014/4/3
 */
using System.Collections;

namespace TangUI
{
  public class UIPanelNodeContext
  {


		/// <summary>
		/// 记录最后一次使用管理器的顺序 xbhuang
		/// </summary>
		public static Stack mgrUseStack = new Stack();

    public UIPanelCache cache;
    public int depth = 0;
    public UIAnchor anchor;
    private UIPanelNode m_currentNode;

    public UIPanelNode currentNode {
      get {
        return m_currentNode;
      }

      set {
        m_currentNode = value;
      }
    }
  }
}