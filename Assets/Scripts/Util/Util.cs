using System;
using UnityEngine;
using System.Collections.Generic;

public class Util
{

	public delegate void Process<T>(int iIndex, T obj);
	public static void RefreshListGameObjectCount<T>(List<T> list, int iCount, GameObject objTemplate, Process<T> process)
	{
		if (list == null || list.Count == iCount || objTemplate == null)
		{
			return;
		}
		if (list.Count < iCount)
		{
			for (int i = list.Count; i < iCount; i++)
			{
				GameObject obj = GameObject.Instantiate(objTemplate);
				if (obj == null)
				{
					return;
				}
				T t = obj.GetComponent<T>();
				if (t == null)
				{
					Debug.LogError("Component Named T is not exist in template");
					list.Clear();
					return;
				}
				list.Add(t);
				if (process != null)
				{
					process(i, t);
				}
			}
		}
		else
		{
			list.RemoveRange(iCount, list.Count - iCount);
		}
	}

}
