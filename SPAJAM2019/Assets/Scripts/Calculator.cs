using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
	// 着衣量
	const double CrossPower = 0.6;

	// 不快指数の値
	private readonly static float[] DISCOMFORT_PATTERN = new float[] {
        55F, // 寒い
        60F, // 少し寒い
        65F, // 何も感じない
        70F, // 少し暑い
        75F, // 暑い
    };
    // 不快指数の評価値
    private readonly static int[] POINT_PATTERN = new[] {
        -2, // 寒い
        -1, // 少し寒い
         0, // 何も感じない
         1, // 少し暑い
         2, // 暑い
    };

	/// <summary>
	/// 基礎代謝計算
	/// </summary>
	//ハリス・ベネディクト方程式(改良版)を使って基礎代謝量を計算しています。
	//男性： 13.397×体重kg＋4.799×身長cm−5.677×年齢+88.362
	//女性： 9.247×体重kg＋3.098×身長cm−4.33×年齢+447.593
	public static float BasicMetabolism(uint _sex, float _weight, float _hight, uint _age)
	{
		float _kcal = 0.0f;
		if(_sex == 0) {
			_kcal = 13.397f * _weight + 4.799f * _hight - 5.677f * _age + 88.362f;
		}else if (_sex == 1) {
			_kcal = 9.247f * _weight + 3.098f * _hight - 4.33f * _age + 447.593f;
		}

		return _kcal;
	}

	/// <summary>
	/// 不快指数計算
	/// </summary>
	//気温を T ℃，相対湿度を H %とする
	//不快指数＝0.81×T＋0.01×H（0.99×T－14.3）＋46.3
	public static double DiscomfortIndex(double _temperature, double _humidity)
	{
		double _index = 0.0f;

		_index = 0.81f * _temperature + 0.01f * _humidity * (0.99f * _temperature - 14.3f) + 46.3f;

		return _index;
	}

	//ミスナール（Missenard）の式の改良版
	//グレゴルチュク（Gregorczuk）は前記式に更に風速の影響を加味して次式を考案しました。
	//低温にも適用できます。
	//Tn = 37 - (37 - T) / (0.68 - 0.0014 x H + 1/A) - 0.29 x T x (1 - H/100)
	//Tn：　体感温度（℃）
	//T ：　気温（℃）
　　//H ：　湿度（％）
　　//V ：　風速（m/s）
　　//A ＝　1.76 + 1.4 x V ^ 0.75
	public static double BodyCelsius(double _T, double _H, double _V = 10)
	{
		double _Tn = 0.0;
		double _A = 1.76 + 1.4 * Mathf.Pow((float)_V, 0.75f);

		_Tn = 37 - (37 - _T) / (0.68 - 0.0014 * _H + 1 / _A) - 0.29 * _T * (1 - _H / 100);

		return _Tn;
	}

    /// <summary>
    /// 不快指数の評価値を取得
    /// </summary>
    public static int GetPoint(int discomfort)
    {
        for (int index = 0; index < DISCOMFORT_PATTERN.Length; index++)
        {
            if (discomfort < DISCOMFORT_PATTERN[index])
            {
                return POINT_PATTERN[index];
            }
        }
        // 例外ケースは「何も感じない」を返す
        return 0;
    }
}
