using UnityEngine;
using System.Collections;

public class ViewPanel : MonoBehaviour {
	public const string NAME = "ViewPanel";
	
  protected bool started = false;

	public virtual string panelName{
		get{return NAME;}
	}
	
	public virtual void Open(){
		
	}

  public virtual void Open(object param){

  }
	
	public virtual void Close(){
		
	}
	
}
