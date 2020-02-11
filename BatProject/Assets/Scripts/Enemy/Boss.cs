using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    bool up;
    public float limit;//飞行边界
    public float speed;//飞行速度
    public GameObject bat, shield;
    public Image hpImage;
    float hp;//血量
    void Start()
    {
        hp = GameData.Instance.maxBossHp;
        InvokeRepeating("CreateBat", 0, 0.1f);
        InvokeRepeating("CreateShield", 5, 5);
    }
    void Update()
    {
        Fly();
    }
    void Fly()
    {
        //上下浮动
        if (up)
        {
            transform.Translate(0, Time.deltaTime * (limit - Mathf.Abs(transform.position.y)) * speed, 0);
            if (transform.position.y > limit - 1)
                up = false;
        }
        else
        {
            transform.Translate(0, -Time.deltaTime * (limit - Mathf.Abs(transform.position.y)) * speed, 0);
            if (transform.position.y < 1 - limit)
                up = true;
        }
    }
    void CreateBat()//生成蝙蝠
    {
        CreateObjFromRandomPos(bat);
    }
    void CreateShield()//生成盾牌
    {
        CreateObjFromRandomPos(shield);
    }
    void CreateObjFromRandomPos(GameObject obj)//随机位置生成物体
    {
        if (GameData.Instance.gameOver) return;

        //计算随机生成位置
        Vector3 randPos = new Vector3(Random.Range(-10, 10f), transform.position.y, transform.position.z);
        Transform th = Instantiate(obj, transform.position, Quaternion.identity).transform;
        StartCoroutine(EjectionBat(th, randPos));
    }
    IEnumerator EjectionBat(Transform th, Vector3 pos)//弹射
    {
        for (int i = 0; i < 30; i++)
        {
            if (th == null)
                break;
            th.position = Vector3.Lerp(th.position, new Vector3(pos.x, pos.y, th.position.z), Time.deltaTime * 5);
            yield return null;
        }
    }
    public void SubBossHp()//扣血
    {
        //血量扣光,游戏结束,游戏胜利,销毁自身
        if (Mathf.Max(--hp, 0) <= 0)
        {
            GameData.Instance.gameOver = true;
            GameData.Instance.win = true;
            Destroy(gameObject);
        }
        hpImage.fillAmount = hp / GameData.Instance.maxBossHp;
    }
}