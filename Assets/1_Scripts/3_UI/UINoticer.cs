using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UINoticer : MonoBehaviour
{
    public Image noticeUI;
    public Text text;
    Coroutine coroutine = null;

    private void Start() {
        StartCoroutine(WinningCondition());
    }

    IEnumerator WinningCondition()
    {
        yield return new WaitForSeconds(1f);
        Notice("오염도를 낮춰 승리하세요");
    }

    public void Notice(string contents)
    {
        noticeUI.gameObject.SetActive(true);
        text.text = contents;
        if (coroutine == null) {
            coroutine = StartCoroutine(Fade());
        } else {
            DOTween.Clear();
            StopCoroutine(coroutine);
            coroutine = StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        float time = 0;

        noticeUI.DOFade(0.8f, 0.5f);
        text.DOFade(1, 0.5f);
        while (time < 1.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        noticeUI.DOFade(0, 1.0f);
        text.DOFade(0, 1.0f).OnComplete(() => 
            {
                noticeUI.gameObject.SetActive(false);
                coroutine = null;
            }
        );
    }
}
