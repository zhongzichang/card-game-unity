using System;

namespace TangSpecial
{
  /// <summary>
  /// 资源加载事件参数
  /// </summary>
  public class LoadEventArgs : EventArgs
  {
    private string name;

    public LoadEventArgs (string name)
    {
      this.name = name;
    }

    public string Name {
      get{
        return name;
      }
    }
  }
}

