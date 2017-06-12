using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
	public class Util
	{

		public delegate void Process(int iIndex, GameObject obj);
		public  void RefreshListGameObjectCount(List<GameObject> list, int iCount ,GameObject objTemplate ,Process process)
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
					list.Add(obj);
					if (process != null)
					{
						process(i, obj);
					}
				}
			}
		}

	}
}
