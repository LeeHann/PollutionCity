using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoticer : MonoBehaviour
{
    public Image noticeUI;
    public Text text;
    WaitForSeconds dot5 = new WaitForSeconds(0.5f);
    Coroutine coroutine = null;

    public void Notice(string contents)
    {
        noticeUI.gameObject.SetActive(true);
        text.text = contents;
        if (coroutine == null)
            coroutine = StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
		Color color = new Color(0.254717f, 0.254717f, 0.254717f, 0.5019608f);
		while (noticeUI.color.a > 0)
		{
			color.a -= Time.deltaTime;
			noticeUI.color = color;
			yield return dot5;
		}
        noticeUI.gameObject.SetActive(false);
        noticeUI.color = Color.white;
    }
}
