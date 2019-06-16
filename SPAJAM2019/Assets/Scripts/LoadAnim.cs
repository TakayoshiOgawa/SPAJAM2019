using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnim : MonoBehaviour
{
	float[] nextPosX = { -50.0f, 0.0f, 50.0f};
	int index = 0;
	private float timeOut = 0.4f;
	private float timeElapsed = 0.0f;

	// Update is called once per frame
	void Update()
	{
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= timeOut) {
			MovePos();
			timeElapsed = 0.0f;
		}
    }

	void MovePos()
	{
		int nextIndex = index;

		nextIndex = (nextIndex + 1) % 3;

		transform.localPosition = new Vector3(nextPosX[nextIndex], 0.0f, 0.0f);

		index = nextIndex;
	}
}
