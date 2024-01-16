using System;
using UnityEngine;

public abstract class TetrisBase : MonoBehaviour
{
    public static bool Pause = false;
    protected const int W = 10, H = 20;
    protected static readonly Transform[,] Grid = new Transform[W, H + W];
    protected Vector3 GetPos()
    {
        return transform.position;
    }
    protected void SetPos(Vector3 v3)
    {
        transform.position = v3;
    }

    protected void AddPos(Vector3 v3)
    {
        SetPos(GetPos() + v3);
    }
    /// <summary>
    /// 判斷邊界是否超越限制，以及是否在陣列內有方塊
    /// </summary>
    /// <returns></returns>
    protected bool ValidMove()
    {
        //先判斷邊界看看
        foreach (Transform c in transform)
        {
            var vector3 = c.transform.position;
            //Debug.Log($"vector3 : {vector3} name : {c.gameObject.name}");
            var roundX = Mathf.FloorToInt(vector3.x);
            var roundY = Mathf.FloorToInt(vector3.y);
            if (roundX < 0 || roundX >= W || roundY < 0 || roundY >= H + 10)
            {
                return false;
            }
            if (roundY < H && Grid[roundX, roundY] != null)
            {
                return false;
            }
        }
        return true;
    }
}