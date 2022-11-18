using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoticer : MonoBehaviour
{
    public Image noticeUI;
    public Text text;
    Coroutine coroutine = null;

    public void Notice(string contents)
    {
        noticeUI.gameObject.SetActive(true);
        text.text = contents;
        if (coroutine == null) {
            coroutine = StartCoroutine(Fade());
        } else {
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
		Color color = new Color(0.254717f, 0.254717f, 0.254717f, 0.5019608f);
        Color textColor = Color.white;
        float time = 0;
        
        noticeUI.color = color;
        text.color = textColor;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
		while (noticeUI.color.a > 0)
		{
			color.a -= Time.deltaTime * 0.5f;
            textColor.a -= Time.deltaTime * 1f;
			noticeUI.color = color;
            text.color = textColor;
			yield return null;
		}
        noticeUI.gameObject.SetActive(false);
        noticeUI.color = new Color(0.254717f, 0.254717f, 0.254717f, 0.5019608f);
        text.color = Color.white;
        coroutine = null;
    }
}
