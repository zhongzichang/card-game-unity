using System;

namespace TangSpecial
{
  public class Config
  {
    public static bool use_packed_res = false;

    public const string SPECIAL_PATH = "Specials";


    public static string SpecialPath(string name){
      return SPECIAL_PATH + Tang.Config.DIR_SEP + name;
    }

  }
}

