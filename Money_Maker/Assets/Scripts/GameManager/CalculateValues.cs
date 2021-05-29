using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateValues : MonoBehaviour
{
    public GameObject uiManager;
    private UIPlaying uiPlaying;            //������� ��������

    [SerializeField]
    private int pointForKilledEnemy;  //���������� ������������ ������

    public int PointForKilledEnemy { get => pointForKilledEnemy; set => pointForKilledEnemy = value; }

    private void Start()
    {
        uiPlaying = uiManager.GetComponent<UIPlaying>();
    }

    /// <summary>
    /// ������ ����� �� ����������� ������� ���� Enemy
    /// </summary>
    /// <param name="value">���� �� ������������ ������</param>
    public void SetPoint(int value)
    {
        PointForKilledEnemy += value;
        //����� ����� ��� ���������� ���������� ������������ ���������
        gameObject.GetComponent<SpawnEnemy>().CalculateDestroedEnemy();
    }
}
