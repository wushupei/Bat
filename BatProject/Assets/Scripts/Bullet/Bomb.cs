using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Shell
{
    public GameObject spark;
    public Renderer render;
    public List<Texture> boomAnimaTex = new List<Texture>();//爆炸动画图片
    public override void Init(Vector3 _dir)
    {
        base.Init(_dir);
    }
    protected override void Attack(Collision col)
    {
        Destruction();
        if (col.transform.CompareTag("Shield"))//碰到盾牌销毁盾牌
            col.transform.GetComponent<Shield>().Destruction();
        else if (col.transform.CompareTag("Boss"))//碰到Boss扣血
            col.gameObject.GetComponent<Boss>().SubBossHp();
    }
    void Destruction()//销毁自身
    {
        GetComponent<Collider>().enabled = false;//关闭碰撞器
        Destroy(rigid);//销毁刚体
        spark.SetActive(false);//禁用火花
        StartCoroutine(PlayBlastAninm());
    }
    IEnumerator PlayBlastAninm()//播放爆炸动画
    {
        render.transform.position = transform.position;
        for (int i = 0; i < boomAnimaTex.Count; i++)
        {
            render.material.mainTexture = boomAnimaTex[i];
            transform.localScale *= 1.2f;
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
