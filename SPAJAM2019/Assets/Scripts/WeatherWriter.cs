using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherWriter : MonoBehaviour
{

	public UnityEngine.UI.Text text;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(SendWeb("Ogaki"));
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator SendWeb(string city)
	{
		var request = UnityEngine.Networking.UnityWebRequest.Get("http://api.openweathermap.org/data/2.5/weather?q="+ city + ",jp&units=metric&appid=8bb65e0054153cae583d41d4296b4974");

		yield return request.SendWebRequest();

		if (request.isHttpError || request.isNetworkError) {
			text.text = request.error;
			Debug.LogError(request.error);
			yield break;
		} else {
			text.text = request.downloadHandler.text;
			Debug.Log(request.downloadHandler.text);
		}
	}
}
