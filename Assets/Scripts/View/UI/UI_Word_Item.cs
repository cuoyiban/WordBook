using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Word_Item : MonoBehaviour {
	public Text labelWord;
	public Text labelCount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetData(string strWord , string strCount)
	{
		labelWord.text = strWord;
		labelCount.text = strCount;
	} 
}
