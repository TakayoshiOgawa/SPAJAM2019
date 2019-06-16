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

	[SerializeField]
	Text metaboText;

	[SerializeField]
	InputField ageFieled;
	[SerializeField]
	InputField weightFieled;
	[SerializeField]
	InputField hightFieled;

	private void Start()
	{
		ageFieled.onEndEdit.AddListener(delegate { EndInputEdit(ageFieled); });
		weightFieled.onEndEdit.AddListener(delegate { EndInputEdit(weightFieled); });
		hightFieled.onEndEdit.AddListener(delegate { EndInputEdit(hightFieled); });
	}

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

	void EndInputEdit(InputField input)
	{
		uint _age = 0;
		uint _weight = 0;
		uint _hight = 0;

		if (age.text.Length != 0) {
			_age = uint.Parse(age.text);
		}

		if (weight.text.Length != 0) {
			_weight = uint.Parse(weight.text);
		}

		if (hight.text.Length != 0) {
			_hight = uint.Parse(hight.text);
		}

		if (_age == 0 || _weight == 0 || _hight == 0)
			return;

		var _metabo = Calculator.BasicMetabolism((uint)sex.value, _weight, _hight, _age);
		metaboText.text = _metabo.ToString();
	}
}
