using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InjuredPanel : MonoBehaviour
{
    public Sprite spr1, spr2;//受伤图片
    Animator anima;
    Image image;
    void Start()
    {
        anima = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
    void Update()
    {
        ShowInjurePanel();
    }
    public void ShowInjurePanel()//显示受伤图片
    {
        //不同等级显示不同受伤图片
        if (GameData.Instance.infectedLevel == 2)
        {
            image.sprite = spr2;
            anima.enabled = true;//闪动动画
        }
        else
        {
            anima.enabled = false;
            image.sprite = spr1;
            image.color = new Color(1, 1, 1, GameData.Instance.ScalingInfected());
        }
    }
}
