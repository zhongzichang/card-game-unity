/// <summary>
///   Config for UI
/// </summary>


namespace TangUI
{
  public class Config
  {
    public const string PANEL_DIR = "Panels";

    public static bool use_packed_res = false;

    public static string PANEL_PATH
    {
      get
	{
	  return  Tang.Config.U3D_PREFAB_DIR + Tang.Config.DIR_SEP + PANEL_DIR;
	}
    }
  }
}