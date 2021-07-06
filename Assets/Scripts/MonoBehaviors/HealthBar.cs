using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;
    float maxHitPoints;
    [HideInInspector]
    public Player character;
    public Image meterImage;
    public Text hpText;

    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    void Update()
    {
        meterImage.fillAmount = hitPoints.value / maxHitPoints;
        hpText.text = "HP:" + (meterImage.fillAmount * 100);
    }
}
