using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateValues : MonoBehaviour
{
    [SerializeField]
    private int pointForKilledEnemy;  //Количество уничтоженных врагов

    public int PointForKilledEnemy { get => pointForKilledEnemy; set => pointForKilledEnemy = value; }


    public void GetCountPoints()
    {
        Debug.Log("POINTS = " + PointForKilledEnemy);
    }

    /// <summary>
    /// Подчет очков за уничтожение объекта типа Enemy
    /// </summary>
    /// <param name="value">Цена за уничтоженный объект</param>
    public void SetPoint(int value)
    {
        pointForKilledEnemy += value;
        GetCountPoints();
    }

}
