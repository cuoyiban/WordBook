using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Word_Overview_Item : MonoBehaviour {
	public Text labelSepll;
	public Text labelCount;
	public Text labelIsLearn;
	public Text labelIsImportant;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSepll(string strSepll)
	{
		labelSepll.text = strSepll;
	}

	public void SetCount(long lCount)
	{
		labelCount.text = "计数：" + lCount.ToString();
	}

	public void SetLearned(bool bLearned)
	{
		labelIsLearn.gameObject.SetActive(bLearned);
	}

	public void SetIsImportant(bool bImportant)
	{
		labelIsImportant.gameObject.SetActive(bImportant);
	}
}
