using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombPanel : MonoBehaviour
{
    public Image slot;
    public GameObject spark;
    void Update()
    {
        ShowSlot();
    }
    void ShowSlot()//显示炸弹槽
    {
        slot.fillAmount = GameData.Instance.ScalingShellSlot();
        if (slot.fillAmount == 1)
            spark.SetActive(true);
        else
            spark.SetActive(false);
    }
}
