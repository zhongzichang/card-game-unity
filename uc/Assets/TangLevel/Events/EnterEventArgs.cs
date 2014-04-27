namespace TangLevel
{
  public class EnterEventArgs : LevelEventArgs
  {
    private bool success;

    public EnterEventArgs(int id, bool success) : base(id)
    {
      this.success = success;
    }

    public bool Success
    {
      get
	{
	  return success;
	}
    }
  }
}