using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	void Start()
	{

		//创建数据库名称为xuanyusong.db
		DbAccess db = new DbAccess("data source=xuanyusong.db");

		//创建数据库表，与字段
		db.CreateTable("momo", new string[] { "name", "qq", "email", "blog" }, new string[] { "text", "text", "text", "text" });
		//关闭对象
		db.CloseSqlConnection();
	}
}
