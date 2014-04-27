using System;
namespace TangLevel
{
  /// <summary>
  /// 场景事件参数
  /// </summary>
  public class LevelEventArgs : EventArgs
  {

    private int id;

    /// <summary>
    ///   param id : 场景ID
    /// </summary>
    public LevelEventArgs(int id)
    {
      this.id = id;
    }

    public int Id
    {
      get
	{
	  return id;
	}
    }
  }
}