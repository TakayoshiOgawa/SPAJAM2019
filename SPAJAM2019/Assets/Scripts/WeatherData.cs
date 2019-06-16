using System.Collections;
using System.Collections.Generic;

namespace Weather
{
    public struct WeatherData
	{
		public double temp;
		public double tempMin;
		public double tempMax;
		public string weather;
		public double humidity;
		public double windSpeed;
		public string icon;
		public double sunPower;

        public string WeatherJP()
        {
            // 日本語名（全てではない
            var jp_dic = new Dictionary<string, string>()
            {
                { "thunderstorm ", "雷" },
                { "drizzle", "雨" },
                { "rain", "雨" },
                { "snow", "雪" },
                { "clear", "晴れ" },
                { "clouds", "雲り" },
				{ "Thunderstorm ", "雷" },
				{ "Drizzle", "雨" },
				{ "Rain", "雨" },
				{ "Snow", "雪" },
				{ "Clear", "晴れ" },
				{ "Clouds", "雲り" },
			};

            // 天気の名称に含まれるものがあれば日本語で返す
            foreach (var jp in jp_dic)
            {
                if(weather.Contains(jp.Key))
                {
                    return jp.Value;
                }
            }

            // 見つからない場合は英語のまま返す
            return weather;
        }

		public double SunPower()
		{
			double sp = 0.0f;
			var sunpowDic = new Dictionary<string, double>()
			{
				{ "雨", 0.1},
				{ "雲り",0.5},
				{ "晴れ", 0.8},
			};

			foreach (var sunpow in sunpowDic) {
				if (weather.Contains(sunpow.Key)) {
					return sunpow.Value;
				}
			}

			return sp;
		}
	}
}
