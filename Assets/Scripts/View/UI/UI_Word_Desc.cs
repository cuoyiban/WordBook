using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Model.VO;
public class UI_Word_Desc : MonoBehaviour {
	public Text labelSpell;
	public Text labelAlreadyLearn;
	public Text labelExplain;
	public Text labelThreshold;
	public Text labelImportant;
	public Text labelCount;
	public InputField searchWordInputField;
	public InputField addContextInputField;
	public VerticalLayoutGroup contextContainer;
	public Button btnSearch;
	public Button btnAddContext;
	public Toggle checkBoxSearchAutoAdd;
	public GameObject objContextTemplate;
	private long m_lThreshold;
	private List<UI_Context> m_listContexts = new List<UI_Context>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowWord(WordVO wordVO)
	{
		if (wordVO == null)
		{
			labelSpell.text = "无";
			labelAlreadyLearn.text = "无";
			labelExplain.text = "暂无";
			labelImportant.text = "无";
			labelCount.text = "无";
		}
		else
		{
			labelSpell.text = wordVO.Spell;
			labelAlreadyLearn.text = wordVO.IsAlreadyLearn ? "已学会":"未学会";
			labelExplain.text = "暂无";
			labelImportant.text = wordVO.Count < m_lThreshold ? "不重要" : "重要";
			labelCount.text = string.Format("次数：{0}" + (wordVO.Count <= 0 ? "(没有添加记录)" : ""), wordVO.Count);
		}

		Util.RefreshListGameObjectCount<UI_Context>(m_listContexts, wordVO != null ? wordVO.Contexts.Count : 0, objContextTemplate,
			delegate (int iIndex, UI_Context obj)
			{
				obj.transform.SetParent(contextContainer.transform);
				obj.action = OnEditorContext;
			});
		for (int i = 0; i < wordVO.Contexts.Count; i++)
		{
			m_listContexts[i].SetContextInfo(i + 1, wordVO.Contexts[i]);
		}
		
	}

	public void SetThreshold(long lThreshold)
	{
		m_lThreshold = lThreshold;
		labelThreshold.text = "阀值：" + m_lThreshold;
	}

	public string GetSearchWord()
	{
		return searchWordInputField.text;
	}

	public void CleanSearchWord()
	{
		searchWordInputField.text = "";
	}

	public string GetContext()
	{
		return searchWordInputField.text;
	}

	public void CleanContext()
	{
		searchWordInputField.text = null;
	}

	public void OnEditorContext(int iIndex , string strContext)
	{
		Debug.Log(iIndex.ToString() + " Editor " + strContext);

	}
}
