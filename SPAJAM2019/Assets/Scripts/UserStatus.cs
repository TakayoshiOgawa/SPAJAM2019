using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create Scriptable", fileName = "UserStatus")]
public class UserStatus : ScriptableObject
{
	public uint age = 0;    // 年齢
	public uint hight = 0;  // 身長
	public uint weight = 0; // 体重
	public uint sex = 0;	// 性別 0:未入力	1:男性	2:女性
}
