﻿using System;
using UnityEngine;
using System.Collections.Generic;

public class Util
{

	public delegate void Process<T>(int iIndex, T obj);
	public static void RefreshListGameObjectCount<T>(List<T> list, int iCount, GameObject objTemplate, Process<T> process) where T : MonoBehaviour
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
			for (int i = iCount; i < list.Count; i++)
			{
				GameObject.Destroy(list[i].gameObject);
			}
			list.RemoveRange(iCount, list.Count - iCount);
		}
	}


	public static long GetCurTimeStamp()
	{
		TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
		return Convert.ToInt64(ts.TotalSeconds);
	}

	public static string GetUUID()
	{
		return System.Guid.NewGuid().ToString("N");
	}

	public static string StringToDBString(string str)
	{
		return "'" + str + "'";
	}

	public static string AddSpace(string str)
	{
		return " " + str + " ";
	}

	//计算sourceType 是否可以转换到destType 而不损失精度，比如int32->int64
	public static Dictionary<TypeCode, int> typeTransformWeight = new Dictionary<TypeCode, int>
	{
		{ TypeCode.Int64 , 10} , { TypeCode.Int32 ,9 } , { TypeCode.Int16 , 8}
	};
	public static bool CanTransform(TypeCode destType , TypeCode sourceType)
	{
		return typeTransformWeight[destType] > typeTransformWeight[sourceType];
	}
}
