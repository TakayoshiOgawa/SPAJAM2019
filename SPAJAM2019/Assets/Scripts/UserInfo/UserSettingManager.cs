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
	
	[SerializeField]
	MainScene scene;
	
	/// <summary>
	/// 戻るボタン
	/// </summary>
	public void TouchBackButton()
	{
		// 入力情報をスクリプタブルオブジェクトへ保存。
		if(age.text != "")
			userStatus.age = uint.Parse(age.text);
		if(hight.text != "")
			userStatus.hight = uint.Parse(hight.text);
		if(weight.text != "")
			userStatus.weight = uint.Parse(weight.text);

		userStatus.sex = (uint)sex.value;

		//TODO:ここに前の画面へ戻る処理。
		scene.BackToMainLayer();
	}
}
