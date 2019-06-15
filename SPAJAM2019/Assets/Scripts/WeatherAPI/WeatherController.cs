//------------------------------------------------------------------------
//
// (C) Copyright 2018 Urahimono Project Inc.
//
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeatherController : MonoBehaviour
{
    private static readonly string DEFINITION_TABLE_URL = "http://weather.livedoor.com/forecast/rss/primary_area.xml";
    private static readonly System.Xml.Linq.XName CITY_TITLE_ATTRIBUTE = System.Xml.Linq.XName.Get("title");
    private static readonly System.Xml.Linq.XName CITY_ID_ATTRIBUTE = System.Xml.Linq.XName.Get("id");

    private static readonly string API_URL = "http://weather.livedoor.com/forecast/webservice/json/v1";
    private static readonly string API_CITY_PARAMETER = "city";

    [SerializeField]
    private Dropdown m_dropdownUI = null;
    [SerializeField]
    private WeatherUI[] m_weatherUIs = null;


    private CityData[] m_cities = null;


    private void Start()
    {
        StartCoroutine(Initialize());
    }


    #region Initialize

    private IEnumerator Initialize()
    {
        if (m_dropdownUI == null)
        {
            yield break;
        }

        // 都市情報が取得できるかわからないから、それまではUIを隠しておこうか。
        m_dropdownUI.gameObject.SetActive(false);

        // 日付ごとの天気情報UIは天気を取得してから表示するようにしよう。
        foreach (var weatherUI in m_weatherUIs)
        {
            if (weatherUI != null)
            {
                weatherUI.gameObject.SetActive(false);
            }
        }

        CityData[] cities = null;
        {
            System.Action<CityData[]> onFinished = (i_cities) =>
            {
                cities = i_cities;
            };
            yield return GetCityTableProcess(onFinished);
        }

        if (cities == null || cities.Length == 0)
        {
            Debug.LogErrorFormat("都市情報取ってこれんかったわ。フォーマットでも変わったのかなぁ。");
            yield break;
        }

        // 都市名をリストで表示できるようにしよう。
        m_dropdownUI.ClearOptions();
        m_dropdownUI.AddOptions(cities.Select(value => value.Name).ToList());
        m_dropdownUI.gameObject.SetActive(true);

        m_cities = cities;
    }

    private IEnumerator GetCityTableProcess(System.Action<CityData[]> i_onFinished)
    {
        if (i_onFinished == null)
        {
            Debug.LogErrorFormat("コールバックが指定されてないぜ。これじゃ結果がわからないぜ。");
            yield break;
        }

        string xmlText = null;
        {
            System.Action<string> onFinished = (i_text) =>
            {
                xmlText = i_text;
            };
            yield return CallWebRequest(DEFINITION_TABLE_URL, onFinished);
        }

        if (string.IsNullOrEmpty(xmlText))
        {
            Debug.LogErrorFormat("1次細分区定義表の取得に失敗だ！");
            i_onFinished(null);
            yield break;
        }

        Debug.LogFormat("1次細分区定義表(XML):\n{0}", xmlText);
        CityData[] cities = ParseCityList(xmlText);

        i_onFinished(cities);
    }

    private CityData[] ParseCityList(string i_xmlText)
    {

        var cities = new List<CityData>();

        try
        {
            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Parse(i_xmlText);
            System.Xml.Linq.XElement root = xml.Root;

            // cityタグに都市情報があるはずだ！
            // フォーマットが変わったら、この方法じゃ無理になるかもしれないかもね。
            var cityElements = root.Descendants("city");
            if (cityElements != null)
            {
                foreach (var element in cityElements)
                {
                    var titleAttribute = element.Attribute(CITY_TITLE_ATTRIBUTE);
                    var idAttribute = element.Attribute(CITY_ID_ATTRIBUTE);
                    if (titleAttribute == null || idAttribute == null)
                    {
                        continue;
                    }

                    cities.Add(new CityData(idAttribute.Value, titleAttribute.Value));
                }
            }
        }
        catch (System.Exception i_exception)
        {
            Debug.LogErrorFormat("うーむ、このXML情報は読み込めなかったらしい。エラーの詳細を添付しておくよ。{0}", i_exception);
        }

        return cities.ToArray();
    }

    #endregion // Initialize


    #region GetWeather

    public void OnGetWeather()
    {
        // UI上で取得ボタンを押したら呼ばれるぜ！

        if (m_cities == null || m_cities.Length == 0)
        {
            Debug.LogErrorFormat("都市情報を取得できてないから、天気取得なんて無理無理！");
            return;
        }

        if (m_dropdownUI == null ||
            !m_dropdownUI.gameObject.activeInHierarchy)
        {
            Debug.LogErrorFormat("都市のUI表示されてなくね？");
            return;
        }

        string selectedCityName = m_dropdownUI.captionText.text;
        if (string.IsNullOrEmpty(selectedCityName))
        {
            Debug.LogErrorFormat("どこの都市を選択したか、わかんないんだけど……。");
            return;
        }

        CityData selectedCity = m_cities.FirstOrDefault(value => value.Name == selectedCityName);
        if (selectedCity == null)
        {
            Debug.LogErrorFormat("知らない都市なんだが……。この都市知ってる？ {0}", selectedCityName);
            return;
        }

        StartCoroutine(GetWeatherProcess(selectedCity.ID));
    }

    private IEnumerator GetWeatherProcess(string i_cityID)
    {
        if (string.IsNullOrEmpty(i_cityID))
        {
            Debug.LogErrorFormat("ちゃんと都市IDを入れてくれよ！");
            yield break;
        }

        Debug.Log("City ID:" + i_cityID);

        string requestURL = string.Format("{0}?{1}={2}", API_URL, API_CITY_PARAMETER, i_cityID);
        string jsonText = null;
        {
            System.Action<string> onFinished = (i_text) =>
            {
                jsonText = i_text;
            };
            yield return CallWebRequest(requestURL, onFinished);
        }

        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.LogErrorFormat("天気情報の取得に失敗だ！");
            yield break;
        }


        Debug.LogFormat("天気情報(Json):\n{0}", jsonText);

        var weatherData = JsonUtility.FromJson<WeatherData>(jsonText);
        SetWeatherUI(weatherData);
    }

    private void SetWeatherUI(WeatherData i_weatherData)
    {
        if (i_weatherData == null)
        {
            Debug.LogErrorFormat("天気情報が空のようだぜ！");
            return;
        }

        var forecasts = i_weatherData.forecasts;
        if (forecasts == null || forecasts.Length == 0)
        {
            Debug.LogErrorFormat("天気情報が空のようだぜ！");
            return;
        }

        if (m_weatherUIs == null || m_weatherUIs.Length == 0)
        {
            Debug.LogErrorFormat("天気情報を表示するUIの準備が出来ていないようだね");
            return;
        }

        // 日付毎のUIの数より天気情報の日付数の方が多い場合は、UI分だけ表示。
        // 逆に日付毎のUIの数の方が多い場合は、天気情報分だけ表示。
        for (int i = 0; i < m_weatherUIs.Length; ++i)
        {
            var ui = m_weatherUIs[i];
            if (ui == null)
            {
                continue;
            }

            if (forecasts.Length <= i)
            {
                ui.gameObject.SetActive(false);
                continue;
            }

            ui.gameObject.SetActive(true);
            var forecast = forecasts[i];

            // 温度は入っていないこともあるみたいなので、一応チェックしておこう。
            ui.Data = forecast.date;
            ui.Weather = forecast.telop;
            ui.TemperatureMin = forecast.temperature.min != null ? forecast.temperature.min.celsius : null;
            ui.TemperatureMax = forecast.temperature.max != null ? forecast.temperature.max.celsius : null;
        }

    }

    #endregion // GetWeather


    private IEnumerator CallWebRequest(string i_requestURL, System.Action<string> i_onFinished)
    {
        if (i_onFinished == null)
        {
            Debug.LogErrorFormat("コールバックが指定されてないぜ。これじゃ結果がわからないぜ。");
            yield break;
        }

        if (string.IsNullOrEmpty(i_requestURL))
        {
            Debug.LogErrorFormat("URLぐらい指定してくれよ……。");
            i_onFinished(null);
            yield break;
        }

        var request = UnityEngine.Networking.UnityWebRequest.Get(i_requestURL);
        yield return request.SendWebRequest();

        // ドキュメントなどでは"isError"を使って判定しているようだけど、
        // "isNetworkError"に名前が変わったようなので、こっちを使ってくれよな！
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogErrorFormat("残念ながらエラーが発生しちまったらしい。詳細を添付しておく。{0}", request.error);
            i_onFinished(null);
            yield break;
        }

        i_onFinished(request.downloadHandler.text);
    }


} // class WeatherController
