using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField] RectTransform rect;
	Coroutine coroutine;

	public void RotateThis(float rotateVal)
	{
		if (coroutine == null)
			coroutine = StartCoroutine(Rotate(rotateVal)); 
	}

	IEnumerator Rotate(float rotateVal)
	{
		float val = 0f;
		float tmp;
		var check = rect.eulerAngles.z;
		while (val < rotateVal)
		{
			tmp = Time.deltaTime * 1000;
			val += tmp;
			rect.Rotate(new Vector3(0,0,-tmp));
			yield return null;
		}
		rect.eulerAngles = new Vector3(0, 0, check-rotateVal);
		coroutine = null;
	}
}
