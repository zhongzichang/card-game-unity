using System;

namespace TangSpecial
{
  /// <summary>
  /// 资源加载结果 － 成功与否
  /// </summary>
  public class LoadResultEventArgs : LoadEventArgs
  {
    private bool success;
    public LoadResultEventArgs (string name, bool success) : base(name)
    {
      this.success = success;
    }

    public bool Success {
      get {
        return success;
      }
    }
  }
}

