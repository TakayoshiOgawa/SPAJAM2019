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
                { "clouds", "雲" },
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
	}
}
