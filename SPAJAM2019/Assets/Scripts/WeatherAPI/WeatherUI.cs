//------------------------------------------------------------------------
//
// (C) Copyright 2018 Urahimono Project Inc.
//
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{
    [SerializeField]
    private Text m_date = null;
    [SerializeField]
    private Text m_weather = null;
    [SerializeField]
    private Text m_temperatureMin = null;
    [SerializeField]
    private Text m_temperatureMax = null;

    public string Data
    {
        set
        {
            if (m_date != null)
            {
                m_date.text = value;
            }
        }
    }

    public string Weather
    {
        set
        {
            if (m_weather != null)
            {
                m_weather.text = value;
            }
        }
    }

    public string TemperatureMin
    {
        set
        {
            if (m_temperatureMin != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    m_temperatureMin.text = string.Format("最低温度:---");
                    return;
                }
                m_temperatureMin.text = string.Format("最低温度:{0}℃", value);
            }
        }
    }

    public string TemperatureMax
    {
        set
        {
            if (m_temperatureMax != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    m_temperatureMax.text = string.Format("最高温度:---");
                    return;
                }
                m_temperatureMax.text = string.Format("最高温度:{0}℃", value);
            }
        }
    }


} // class WeatherUI
