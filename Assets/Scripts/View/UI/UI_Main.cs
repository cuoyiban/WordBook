using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Model.VO;

public class UI_Main : MonoBehaviour {
	public GameObject templateWordOverview;
	public UI_Menu menuFilter;
	public VerticalLayoutGroup wordList;
	private List<UI_Word_Overview_Item> listObjWords = new List<UI_Word_Overview_Item>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshWordList(List<WordVO> listWord , long lThreshold)
	{
		Util.RefreshListGameObjectCount<UI_Word_Overview_Item>(listObjWords, listWord.Count, templateWordOverview,
			delegate (int iIndex , UI_Word_Overview_Item obj)
			{
				obj.transform.SetParent(wordList.transform);
			});
		for (int i = 0; i < listWord.Count; i++)
		{
			listObjWords[i].SetSepll(listWord[i].Spell);
			listObjWords[i].SetCount(listWord[i].Count);
			listObjWords[i].SetIsImportant(listWord[i].Count >= lThreshold);
			listObjWords[i].SetLearned(false);
		}
	}
}
