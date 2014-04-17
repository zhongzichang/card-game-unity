using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class Level
  {
    public int id;
    public string name;

    public Group playerGroup;

    public SubLevel[] subLeves;


    public Level(){
    }

    public Level(int id, string name){
      this.id = id;
      this.name = name;
    }

  }
}