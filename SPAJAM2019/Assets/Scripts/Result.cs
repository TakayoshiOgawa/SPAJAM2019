using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
	// 体感
	public Text bodyText;
	double body;
	public double Body { get { return this.body; } set { this.body = value; } }

	// 不快指数
	public Text fukaiText;
	double fukai;
	public double Fukai { get { return this.fukai; } set { this.fukai = value; } }

	// 不快評価ポイント
	double fukaiPoint;
	public double FukaiPoint { get { return this.fukaiPoint; } set { this.fukaiPoint = value; } }

	// 気温
	public Text tempText;
	double temp;
	public double Temp { get { return this.temp; } set { this.temp = value; } }

	// 湿度
	public Text humiText;
	double humi;
	public double Humi { get { return this.humi; } set { this.humi = value; } }

	// 都市名
	public Text cityText;
	string city;
	public string City { get { return this.city; } set { this.city = value; } }

	// 天気
	public Text weatherText;
	string weather;
	public string Weather { get { return this.weather; } set { this.weather = value; } }

	// 画像
	public Image image;

	public List<Sprite> sprListMen;
	public List<Sprite> sprListWomen;
	public int ConvFukaiP(int fp)
	{
		int index = fp + 2;

		return index;
	}

	public void PutSpriteColor(int fp)
	{
		if(fp == 0) {
			image.color = new Color(0.5f,0.5f, 1.0f);
		}else if (fp == 4) {
			image.color = new Color(1.0f, 0.5f, 0.5f);
		} else {
			image.color = new Color(1.0f, 1.0f, 1.0f);
		}
	}

	[SerializeField]
	GameObject hyou;

	bool isShow = false;
	public void ShowHyou()
	{
		isShow = !isShow;
		hyou.SetActive(isShow);
	}
}
