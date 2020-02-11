using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public RectTransform aim;//准心图片
    public Shell shell;
    Animator shellSpark;
    public Bomb bomb;

    LineRenderer line;//显示瞄准方向
    LayerMask layer;//射线检测层
    public Animator fail, win;//失败动画和胜利动画
    void Start()
    {
        shellSpark = GetComponentInChildren<Animator>();
        line = GetComponent<LineRenderer>();
        layer = LayerMask.GetMask("Boss") | LayerMask.GetMask("Shield");
        Cursor.visible = false;//隐藏鼠标光标
    }
    void Update()
    {
        shellSpark.transform.forward = Vector3.forward;
        //游戏结束后根据胜负播放相应动画
        if (GameData.Instance.gameOver)
        {
            if (GameData.Instance.win)
                win.enabled = true;
            else
                fail.enabled = true;
            return;
        }
        RotateCannon();
        ShowFireDir();
        if (Input.GetMouseButtonDown(0))
        {
            shellSpark.Play("ShellSpark");
            Fire(shell);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (GameData.Instance.fireSBomb)
            {
                Fire(bomb);
                GameData.Instance.ResetShellSlot();
            }
        }
    }
    void RotateCannon()//旋转大炮
    {
        Vector3 mp = Input.mousePosition;
        //鼠标必须停留在屏幕范围内才能旋转
        if (mp.x > 0 && mp.x < Screen.width && mp.y > 0 && mp.y < Screen.height)
        {
            //获取鼠标世界坐标,并朝向该方向
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mp.x, mp.y, 50));
            transform.LookAt(pos);//身体跟随鼠标世界坐标旋转
        }
    }
    void ShowFireDir()//显示发射方向
    {
        Vector3 ori = transform.position + transform.forward * 0.5f;//起点
        line.SetPosition(0, ori);//瞄准方向起点位置
        RaycastHit hit;
        //射线检测前方Boss和盾牌
        if (Physics.Raycast(ori, transform.forward, out hit, 50, layer))
        {
            //如果射线打到物体,设置瞄准方向的终点位置,显示准心
            line.SetPosition(1, hit.point);
            //显示准心
            aim.gameObject.SetActive(true);
            aim.position = Camera.main.WorldToScreenPoint(hit.point);
        }
        else
        {
            line.SetPosition(1, ori + transform.forward * 50);
            aim.gameObject.SetActive(false);
        }
    }
    void Fire(Shell obj)//发射炮弹
    {
        Instantiate(obj, transform.position + transform.forward, Quaternion.identity).Init(transform.forward);
    }
}