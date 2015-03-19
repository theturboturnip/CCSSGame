using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageHandler : MonoBehaviour {
	static Text message;
	static GameObject Confirm,ConfirmOrDeny,MessagePanel;
	public delegate void CallBack();
	static CallBack MessageEnd;
	// Use this for initialization
	void Start () {
		GameObject MessageObject=GameObject.Find("Handlers/MessageHandler/Canvas/MessagePanel/Text");
				   MessagePanel=GameObject.Find("Handlers/MessageHandler/Canvas/MessagePanel");
				   Confirm=GameObject.Find("Handlers/MessageHandler/Canvas/MessagePanel/Confirm");
				   ConfirmOrDeny=GameObject.Find("Handlers/MessageHandler/Canvas/MessagePanel/ConfirmOrDeny");
		message=MessageObject.GetComponent<Text>();
		MessagePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}
	public static void TellPlayer(string totell,bool isQuestion,CallBack CBack){
		MessagePanel.SetActive(true);
		Time.timeScale=0.0f;
		ConfirmOrDeny.SetActive(isQuestion);
		Confirm.SetActive(!isQuestion);
		message.text=totell;
		MessageEnd=CBack;
	}
	public void MsgReceived(){
		Time.timeScale=1.0f;
		MessagePanel.SetActive(false);
		MessageEnd();
	}
	public void QuestionAnswered(bool value){
		Time.timeScale=1.0f;
		MessagePanel.SetActive(false);
		MessageEnd();
	}
}
