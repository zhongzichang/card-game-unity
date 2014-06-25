using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  /// 进入游戏界面，包括登陆，和服务器选择
  public class EnterGamePanel : ViewPanel {
    public const string NAME = "EnterGamePanel";

    public GameObject loginGroup;
    public GameObject serverListGroup;

    //============================================
    public UILabel versionLabel;
    public UIEventListener enterBtn;
    public UILabel enterBtnLabel;
    public GameObject serverGroup;
    public UILabel loginIdLabel;
    public UILabel loginNameLabel;
    public UILabel loginChangeLabel;
    public UIEventListener loginChangeBtn;
    //============================================
    public UILabel idLabel;
    public UILabel nameLabel;
    public UILabel statusLabel;
    public UILabel loginLabel;

    public UILabel allServerLabel;
    public ServerInfoItem serverInfoItem;

    void Start(){
      serverListGroup.SetActive(false);
      enterBtn.gameObject.SetActive(true);
      loginIdLabel.text = "";
      loginNameLabel.text = "";
      loginChangeBtn.onClick += ChangeServerClickHandler;
      enterBtn.onClick += EnterBtnClickrHandler;
    }

    private void ChangeServerClickHandler(GameObject go){

    }

    private void EnterBtnClickrHandler(GameObject go){
      Application.LoadLevel("Home");
    }
  }
}