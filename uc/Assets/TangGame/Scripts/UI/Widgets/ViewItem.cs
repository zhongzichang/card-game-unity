using UnityEngine;
using System.Collections;

public class ViewItem : MonoBehaviour {

	private string mId;
	private int mIndex;
	private object mData;
	private bool mSeleted;
	
	protected bool isStart = false;

	public string id{
		get{return this.mId;}
		set{this.mId = value;}
	}
	
	public int index{
		get{return this.mIndex;}
		set{this.mIndex = value;}
	}
	
	public object data{
		get{return this.mData;}
		set{this.mData = value;this.UpdateData();}
	}
	
	public bool selected{
		get{return this.mSeleted;}
		set{this.mSeleted = value;this.UpdateSelected();}
	}
	
	public virtual void UpdateData(){
		
	}
	
	public virtual void UpdateSelected(){
		
	}
	
	public virtual void Start(){
		this.isStart = true;
	}
	
	public virtual void OnDestroy(){
		this.mData = null;
	}
}