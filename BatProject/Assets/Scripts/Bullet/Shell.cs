using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    Vector3 dir;//飞行方向
    protected Rigidbody rigid;
    public float speed;
    public virtual void Init(Vector3 _dir)//初始化获取飞行方向
    {
        dir = transform.TransformDirection(_dir);
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Fly();
        //飞出一定位置自行销毁
        if (transform.position.z > 100)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision col)
    {
        Attack(col);
    }
    void Fly()//飞行
    {
        if (rigid != null)
            rigid.velocity = dir * speed;
    }
    protected virtual void Attack(Collision col)
    {
        //碰到蝙蝠消灭蝙蝠,碰到其它销毁自身
        if (col.gameObject.CompareTag("Bat"))
        {
            col.gameObject.GetComponent<Bat>().Destruction();
            GameData.Instance.SubInfected();//减少感染
            GameData.Instance.AddShellSlot();//增加炮弹槽
        }
        else
            Destroy(gameObject);
    }
}
