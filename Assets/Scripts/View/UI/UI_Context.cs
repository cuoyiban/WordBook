using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model.VO;

public class UI_Context : MonoBehaviour {
	private AddContextInfo contextInfo;
	private int m_iIndex;
	public Text labelNO;
	public Text labelTag;
	public Text labelContext;
	public InputField inputContext;
	public Button btnEditor;
	public Button btnOk;
	public Button btnCancle;
	public System.Action<int, string> action;
	private string m_strChangeInput;
	// Use this for initialization
	void Start () {
		SetTag("");
		btnEditor.onClick.AddListener(OnEditor);
		btnOk.onClick.AddListener(OnOKClick);
		btnCancle.onClick.AddListener(OnCancelClick);
		inputContext.onValueChanged.AddListener(OnInputChange);
		inputContext.onEndEdit.AddListener(OnEditorEnd);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void EditorMode()
	{
		return;
		btnEditor.gameObject.SetActive(false);
		btnOk.gameObject.SetActive(true);
		btnCancle.gameObject.SetActive(true);
		inputContext.gameObject.SetActive(true);
		inputContext.text = contextInfo.Context;
		inputContext.MoveTextEnd(true);
		m_strChangeInput = contextInfo.Context;
	}

	void ShowMode()
	{
		btnEditor.gameObject.SetActive(true);
		btnOk.gameObject.SetActive(false);
		btnCancle.gameObject.SetActive(false);
		inputContext.gameObject.SetActive(false);
		m_strChangeInput = "";
	}


	void OnEditor()
	{
		EditorMode();
	}
	void OnOKClick()
	{
		ShowMode();
		if (action != null)
		{
			action(m_iIndex, inputContext.text);
		}
	}

	void OnCancelClick()
	{
		ShowMode();
	}

	void OnInputChange(string strInput)
	{
		m_strChangeInput = strInput;
		Debug.Log(m_strChangeInput);
	}

	void OnEditorEnd(string str)
	{
		ShowMode();
	}

	public void SetContextInfo(int iIndex , AddContextInfo contextInfo)
	{
		this.contextInfo = contextInfo;
		m_iIndex = iIndex;
		SetNO(iIndex);
		SetTag("暂无");
		SetContext(contextInfo.Context);
		ShowMode();
	}

	public void SetNO(int iIndex)
	{
		labelNO.text = "NO:" + iIndex.ToString();
	}

	public void SetTag(string strTag)
	{
		labelTag.text = "Tag:" + strTag;
	}

	public void SetContext(string strContext)
	{
		labelContext.text = "Context:" + strContext;
	}
}
