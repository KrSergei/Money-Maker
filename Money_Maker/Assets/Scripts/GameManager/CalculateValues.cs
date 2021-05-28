using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateValues : MonoBehaviour
{
    public GameObject uiManager;
    private UIPlaying uiPlaying;            //������� ��������

    [SerializeField]
    private int pointForKilledEnemy;  //���������� ������������ ������

    private void Start()
    {
        uiPlaying = uiManager.GetComponent<UIPlaying>();
    }

    public int GetCountPoints()
    {
        return pointForKilledEnemy;
    }

    /// <summary>
    /// ������ ����� �� ����������� ������� ���� Enemy
    /// </summary>
    /// <param name="value">���� �� ������������ ������</param>
    public void SetPoint(int value)
    {
        pointForKilledEnemy += value;
        //����� ����� ��� ���������� ���������� ������������ ���������
        gameObject.GetComponent<SpawnEnemy>().CalculateDestroedEnemy();
    }
}
