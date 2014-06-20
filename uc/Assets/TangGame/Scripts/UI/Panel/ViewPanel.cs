using UnityEngine;
using System.Collections;

public class ViewPanel : MonoBehaviour {
	public const string NAME = "ViewPanel";
	
  private object mData;
  protected bool started = false;

	public virtual string panelName{
		get{return NAME;}
	}

  public object data{
    get{return this.mData;}
    set{this.mData = value;this.UpdateData();}
  }
	
	public virtual void Open(){
		
	}

  public virtual void Open(object param){

  }
	
	public virtual void Close(){
		
	}

  protected virtual void UpdateData(){

  }
	
}
