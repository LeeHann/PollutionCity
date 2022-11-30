using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] Image targetImage;
    [SerializeField] Sprite[] sprites;

    [Space(10f)]
    [Header("Text")]
    [SerializeField] TextMeshProUGUI targetTMP;
    [SerializeField] string tmpString;

    public void SwapSprite(bool isOn)
    {
        if (isOn)
            targetImage.sprite = sprites[0];
        else 
            targetImage.sprite = sprites[1];
    }

    public void SliderTMPUpdate(float value)
    {
        targetTMP.text = ((int)(value * 100)).ToString();
    }
}
