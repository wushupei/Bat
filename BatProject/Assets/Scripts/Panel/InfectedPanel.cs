using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedPanel : MonoBehaviour
{
    public Image slot;
    void Update()
    {
        ShowIcon();
    }
    public void ShowIcon()//显示感染程度
    {
        float scaling = GameData.Instance.ScalingInfected();
        slot.fillAmount = scaling;
        slot.color = new Color(1, 1 - Mathf.Max(scaling - 0.5f, 0) * 2, 1 - scaling * 2);
    }
}
