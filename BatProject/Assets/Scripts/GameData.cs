using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private static GameData _Instance;
    public static GameData Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new GameData();
            return _Instance;
        }
    }

    private float infected = 0;//感染指数
    private float maxInfected = 100;//感染指数上限
    public int infectedLevel = 0;//感染等级

    private float bombSlot = 0;//炸弹槽
    private float maxBombSlot = 100;//炸弹槽上限
    public bool fireSBomb;//是否可发射炸弹

    public float maxBossHp = 10;//Boss总血量

    public bool gameOver = false;//游戏是否结束
    public bool win;//是否胜利

    public void AddInfected()//增加感染
    {
        infected = Mathf.Min(++infected, maxInfected);
        //根据感染指数确定感染等级
        if (infected >= maxInfected)//感染到达上限游戏结束,游戏失败
        {
            gameOver = true;
            win = false;
        }
        else if (infected >= maxInfected / 2)//达到感染上限一半为二级感染
            infectedLevel = 2;
        else if (infected > 0)//感染指数大于0为一级感染
            infectedLevel = 1;
        else
            infectedLevel = 0;
    }
    public void SubInfected()//减少感染
    {
        if (gameOver) return;
        infected = Mathf.Max(--infected, 0);
    }
    public float ScalingInfected()//感染比例
    {
        return infected / maxInfected;
    }
    public void AddShellSlot()//增加炮弹槽
    {
        bombSlot += 5;
        if (Mathf.Min(bombSlot, maxBombSlot) == maxBombSlot)
            fireSBomb = true;
    }
    public float ScalingShellSlot()//炮弹槽比例
    {
        return bombSlot / maxBombSlot;
    }
    public void ResetShellSlot()//重置炮弹槽
    {
        bombSlot = 0;
        fireSBomb = false;
    }
}
