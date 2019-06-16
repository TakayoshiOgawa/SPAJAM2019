using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class WeatherWriter : MonoBehaviour
{
	public Weather.WeatherData weatherData;
    
    /// <summary>
    /// リクエストを発行
    /// </summary>
    public void Request(string city)
    {
        // 都市名を指すURLを作成
        var url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + ",jp&units=metric&appid=8bb65e0054153cae583d41d4296b4974";
        // ウェブへリクエストを送る
        StartCoroutine(SendWeb(url, (txt) =>
        {
            // データ書き込み
            WriteWeatherData(txt);
        }));
    }

    /// <summary>
    /// ウェブへの送信
    /// </summary>
    IEnumerator SendWeb(string url, System.Action<string> finishCallback)
    {
        var request = UnityEngine.Networking.UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
            yield break;

        }
        else
        {
            if (finishCallback != null) finishCallback(request.downloadHandler.text);
        }
    }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="json"></param>
	void WriteWeatherData(string json)
	{
		//(1階層目)
		var jsonData = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
		//(2階層目)
		var coord = jsonData["coord"] as Dictionary<string, object>;
		var weather = (IList)jsonData["weather"];
		var main = jsonData["main"] as Dictionary<string, object>;
		var visibility = jsonData["visibility"];
		var wind = jsonData["wind"] as Dictionary<string, object>;
		var clouds = jsonData["clouds"] as Dictionary<string, object>;
		var dt = jsonData["dt"];
		var sys = jsonData["sys"] as Dictionary<string, object>;
		var timezone = jsonData["timezone"];
		var name = jsonData["name"];
		var cod = jsonData["cod"];
		//(3階層目)
		var weather_0 = (IDictionary)weather[0];

		Debug.Log(json);

		//気温
		double.TryParse(main["temp"].ToString(), out weatherData.temp);

		//最低気温
		double.TryParse(main["temp_min"].ToString(), out weatherData.tempMin);

		//最高気温
		double.TryParse(main["temp_max"].ToString(), out weatherData.tempMax);

		//天気
		weatherData.weather = weather_0["main"].ToString();

		//湿度
		double.TryParse(main["humidity"].ToString(), out weatherData.humidity);

		//風速
		double.TryParse(wind["speed"].ToString(), out weatherData.windSpeed);

		// アイコン
		weatherData.icon = weather_0["icon"].ToString();


	}
}
