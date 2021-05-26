using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateValues : MonoBehaviour
{
    [SerializeField]
    private int pointForKilledEnemy;  //���������� ������������ ������

    public int PointForKilledEnemy { get => pointForKilledEnemy; set => pointForKilledEnemy = value; }


    public void GetCountPoints()
    {
        Debug.Log("POINTS = " + PointForKilledEnemy);
    }

    /// <summary>
    /// ������ ����� �� ����������� ������� ���� Enemy
    /// </summary>
    /// <param name="value">���� �� ������������ ������</param>
    public void SetPoint(int value)
    {
        pointForKilledEnemy += value;
        GetCountPoints();
    }

}
