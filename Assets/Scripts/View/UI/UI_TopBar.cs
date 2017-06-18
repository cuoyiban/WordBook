using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_TopBar : MonoBehaviour {

	public Button[] buttons;


	public void SetCallBack(int iIndex ,string strTitle ,System.Action<int> action)
	{
		buttons[iIndex].onClick.AddListener(delegate() { action(iIndex); });
		buttons[iIndex].transform.Find("Text").GetComponent<Text>().text = strTitle;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
