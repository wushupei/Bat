using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shield : Bat
{
    public int count;//销毁时生成炮弹数量
    public Shell shell;
    public override void Destruction()//销毁
    {
        alive = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        CreateShell();
    }
    void CreateShell()//生成炮弹
    {
        for (int i = 0; i < count; i++)
        {
            Shell obj = Instantiate(shell, transform.position, Quaternion.identity);
            obj.Init(transform.forward);

            //弹射方向和位置
            Vector3 dir = Quaternion.Euler(0, 0, -360 / count * i) * transform.up;
            Vector3 pos = transform.position + dir * 3;

            StartCoroutine(Ejection(obj.transform, pos));
        }
        Destroy(gameObject, 1);
    }
    IEnumerator Ejection(Transform th, Vector3 pos)//弹射
    {
        for (int i = 0; i < 10; i++)
        {
            if (th != null)
                th.position = Vector3.Lerp(th.position, new Vector3(pos.x, pos.y, th.position.z), Time.deltaTime * 10);
            yield return null;
        }
    }
}
