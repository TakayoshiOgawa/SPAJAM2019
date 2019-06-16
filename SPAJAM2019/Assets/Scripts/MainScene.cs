using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
	[SerializeField]
	GameObject mainLayer;
	[SerializeField]
	GameObject UserSettingLayer;
	[SerializeField]
	GameObject DestroyerLayer;

	[SerializeField]
	WeatherWriter weatherWriter;

	[SerializeField]
	Result result;

	[SerializeField]
	UserStatus userStatus;

	[SerializeField]
	Dropdown cityName;

	bool isExec = false;

	[SerializeField]
	GameObject fadeObject;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/// <summary>
	/// メインの処理だからな！
	/// </summary>
	public void Execute()
	{
		if (isExec)
			return;

		isExec = true;
		fadeObject.SetActive(true);
		weatherWriter.Request(CityNameList.cityList[(City)cityName.value]);

		Invoke("CalcResult", 3.0f);
	}

	/// <summary>
	/// ユーザー情報を設定するのだ
	/// </summary>
	public void GoToUserSetting()
	{
		mainLayer.SetActive(false);

		UserSettingLayer.SetActive(true);
	}

	/// <summary>
	/// メインレイヤーに戻る
	/// </summary>
	public void BackToMainLayer()
	{
		DestroyerLayer.SetActive(false);
		UserSettingLayer.SetActive(false);

		mainLayer.SetActive(true);
	}

	private string GetCityNameJP(int city)
	{
		string name = "";
		if (city == (int)City.Gifu)
			name = "岐阜";
		else if (city == (int)City.Nagoya)
			name = "名古屋";
		else if (city == (int)City.Mie)
			name = "三重";
		else if (city == (int)City.Sapporo)
			name = "札幌";
		else if (city == (int)City.Tokyo)
			name = "東京";
		else if (city == (int)City.Osaka)
			name = "大阪";
		else if (city == (int)City.Okinawa)
			name = "沖縄";

		return name;
	}

	private void CalcResult()
	{

		// 体感温度を算出
		result.Body = Calculator.BodyCelsius(weatherWriter.weatherData.temp, weatherWriter.weatherData.humidity);//, weatherWriter.weatherData.windSpeed);
		result.bodyText.text = result.Body.ToString("f2");

		// 不快指数を算出
		result.Fukai = Calculator.DiscomfortIndex(result.Body, weatherWriter.weatherData.humidity);
		result.fukaiText.text = result.Fukai.ToString("f2");

		// 不快指数評価ポイントを算出
		result.FukaiPoint = Calculator.GetPoint((int)result.Fukai);

		// 気温
		result.Temp = weatherWriter.weatherData.temp;
		result.tempText.text = result.Temp.ToString("f2");

		// 湿度
		result.Humi = weatherWriter.weatherData.humidity;
		result.humiText.text = result.Humi.ToString("f2");

		// 都市名
		result.City = GetCityNameJP(cityName.value);
		result.cityText.text = result.City.ToString();

		// 天気日本語表記
		result.Weather = weatherWriter.weatherData.WeatherJP();
		result.weatherText.text = result.Weather.ToString();

		// 画像
		if(userStatus.sex == 0) {
			result.image.sprite = result.sprListMen[result.ConvFukaiP((int)result.FukaiPoint)];
		} else if (userStatus.sex == 1) {
			result.image.sprite = result.sprListWomen[result.ConvFukaiP((int)result.FukaiPoint)];
		}
		result.PutSpriteColor(result.ConvFukaiP((int)result.FukaiPoint));


		mainLayer.SetActive(false);

		DestroyerLayer.SetActive(true);

		fadeObject.SetActive(false);

		isExec = false;
	}
}
