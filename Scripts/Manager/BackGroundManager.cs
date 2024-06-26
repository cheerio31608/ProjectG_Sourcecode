using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackGroundManager : MonoBehaviour
{
    public List<Sprite> backgrounds; // 배경 이미지 리스트
    public SpriteRenderer backGround; // 배경을 표시할 SpriteRenderer
    public TextMeshProUGUI floorNumText; // 스테이지 번호 텍스트

    private void Start()
    {
        UpdateBackground();
    }

    private void Update()
    {
        UpdateBackground();
    }

    private void UpdateBackground()
    {
        int floorNum;
        if (int.TryParse(floorNumText.text, out floorNum))
        {
            int backgroundIndex = (floorNum - 1) / 10;
            if (backgroundIndex >= 0 && backgroundIndex < backgrounds.Count)
            {
                backGround.sprite = backgrounds[backgroundIndex];
            }
        }
    }
}