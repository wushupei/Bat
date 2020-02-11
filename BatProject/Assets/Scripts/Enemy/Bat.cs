using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    protected bool alive = true;//是否存活
    public float speed;
    Material mat;
    public List<Texture> flyAnimaTex = new List<Texture>();//飞行动画图片
    public List<Texture> dieAnimaTex = new List<Texture>();//死亡动画图片
    List<Texture> animaTex;
    int timer;//计时器
    private void OnEnable()
    {
        mat = GetComponent<Renderer>().material;
        animaTex = flyAnimaTex;//初始动画图片为飞行图片
    }
    void Update()
    {
        if (GameData.Instance.gameOver) return;
        if (!alive) return;//死亡后不再往前飞

        transform.Translate(0, 0, -Time.deltaTime * speed);
        //跑到主角后方则感染主角并销毁自身
        if (transform.position.z < 0)
        {
            GameData.Instance.AddInfected();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        PlayAnima(animaTex);
    }
    public virtual void Destruction()//销毁
    {
        alive = false;
        GetComponent<Collider>().enabled = false;
        gameObject.AddComponent<Rigidbody>();
        animaTex = dieAnimaTex;
        mat.mainTexture = animaTex[0];
        transform.localScale += Vector3.up * 0.6f;
        Destroy(gameObject, 5);
    }
    void PlayAnima(List<Texture> tex)//播放动画
    {
        if (tex.Count == 0) return;
        if (timer++ % 5 == 0)
        {
            if (timer / 5 >= tex.Count)
                timer = 0;
            mat.mainTexture = tex[timer / 5];
        }
    }
}