using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{
    [SerializeField] private float duration; // 버튼 애니메이션 시간
    [SerializeField] private float targetSizeScale;

    Vector2 originalSize;
    Vector2 targetSize;

    // Test
    public Button DrawBtn;
    public Button ConfirmBtn;

    private ItemStat itemStat;

    public TextMeshProUGUI[] ItemCountText;

    // Start is called before the first frame update
    void Start()
    {

        DrawBtn.onClick.AddListener(() => ButtonClickAnimation(DrawBtn));
        ConfirmBtn.onClick.AddListener(() => ButtonClickAnimation(ConfirmBtn));

        itemStat = FindObjectOfType<ItemStat>();
    }

    // Update is called once per frame
    void Update()
    {
        ConvertCountToText();
    }

  
    // itemCount를 text로 변경
    private void ConvertCountToText()
    {
        for(int i = 0; i < itemStat.ItemCounts.Length; i++)
        {
            ItemCountText[i].text = itemStat.ItemCounts[i].ToString();
        }
    }

    // 버튼 클릭 애니메이션
    private void ButtonClickAnimation(Button button)
    {
        Image targetImage = button.gameObject.GetComponent<Image>();
        RectTransform rectTransform = targetImage.rectTransform;
        StartCoroutine(SizeUpDown(rectTransform));
    }

    IEnumerator SizeUpDown(RectTransform rectTransform)
    {
        yield return StartCoroutine(SizeUp(rectTransform));
        yield return StartCoroutine(SizeDown(rectTransform));
    }

    // 버튼 크기 확대
    IEnumerator SizeUp(RectTransform rectTransform)
    {
        float timeElapsed = 0f;
        originalSize = rectTransform.sizeDelta;
        targetSize = originalSize * targetSizeScale;

        while (timeElapsed < duration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        rectTransform.sizeDelta = targetSize;
    }

    // 버튼 크기 축소
    IEnumerator SizeDown(RectTransform rectTransform)
    {
        float timeElapsed = 0f;
        originalSize = rectTransform.sizeDelta;
        targetSize = originalSize / targetSizeScale;

        while(timeElapsed < duration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        rectTransform.sizeDelta = targetSize;
    }
}
