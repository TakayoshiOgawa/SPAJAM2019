using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettingManager : SingletonMonoBehaviour<UserSettingManager>
{
	[SerializeField]
	UserStatus userStatus;  // こいつはスクリプタブルオブジェクト

	[SerializeField]
	Text age;

	[SerializeField]
	Text hight;

	[SerializeField]
	Text weight;

	[SerializeField]
	Dropdown sex;

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	/// <summary>
	/// 戻るボタン
	/// </summary>
	public void TouchBackButton()
	{
		// 入力情報をスクリプタブルオブジェクトへ保存。
		userStatus.age = uint.Parse(age.text);
		userStatus.hight = uint.Parse(hight.text);
		userStatus.weight = uint.Parse(weight.text);
		userStatus.sex = (uint)sex.value;

		//TODO:ここに前の画面へ戻る処理。
	}
}
