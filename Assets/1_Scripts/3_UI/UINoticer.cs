using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoticer : MonoBehaviour
{
    public Image noticeUI;
    public Text text;
    WaitForSeconds dot1 = new WaitForSeconds(0.1f);
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
		while (noticeUI.color.a > 0)
		{
			color.a -= Time.deltaTime * 3;
            textColor.a -= Time.deltaTime * 6;
			noticeUI.color = color;
            text.color = textColor;
			yield return dot1;
		}
        noticeUI.gameObject.SetActive(false);
        noticeUI.color = Color.white;
        text.color = Color.white;
        coroutine = null;
    }
}
