using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public Transform[] EnemySpotsSwpawn;    //������ ����� ��������� ������� ���� enemy

    public int countEnemy;                  //����� ���������� ������������ ��������
    public int maxSpawnCountEnemy;          //������������ ���������� ������������ �������� �� ����� �����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyInSpot()
    {

        yield return null;
    }

    private bool CanSpawn()
    {

        return true;
    }

}
