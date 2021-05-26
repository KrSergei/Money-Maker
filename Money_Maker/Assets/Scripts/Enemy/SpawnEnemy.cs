using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public List<Transform> EnemySpotsSpawn; //������ ����� ��������� ������� ���� enemy
    public GameObject[] typeEnemy;          //������ ����� enemy

    public int countEnemy;                  //����� ���������� ������������ ��������
    public int maxCountSpawnedEnemyInSpot;  //������������ ���������� ������������ �������� �� ����� �����
    public float timeBetweenWaves;          //����� �������� ����� ���������� ��������� ��������

    [SerializeField]
    private int countEnemyWave;             //���������� ���� ������ 
    [SerializeField]
    private int currentWaves;               //����� ������� �������� ��������� ��������
    [SerializeField]
    private int destroedEnemy;              //���������� ������������ ������

    private void Start()
    {
        currentWaves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSpawnEnemy();
        }
    }

    public void StartSpawnEnemy()
    {
        //������ �������� �� ��������� ������ � ��������� � ��� ���� ������� �������� ���� ������������ ������
        StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy));
    }

    IEnumerator SpawnEnemyInSpot(int currentWave, int countEnemyForWalve, float timeDelay = 0f)
    {
        //�������� ����� ��������(��� ������� ������ �����, �������� ��������� 0)
        //��� ����������� ����� ����������� ������� ��� ������� �����
        yield return new WaitForSeconds(timeDelay);
        //����������� ��������� ����� ���������. 
        //����� ������ ���������� ����������� ����� ��������� �� ������ ��������� ����� ���������
        int spawnPos = RandomValue(EnemySpotsSpawn.Count);
        //���������� ���������������� ������
        int spawnedEnemy = 0;
        //��������� ���������� �������� ��������� � �����
        int countEnemyForSpawn = countEnemyForWalve;

        //��������� ������, ���� ���������� ������ � ����� ������ 0;
        while (countEnemyForSpawn > 0)
        {
            //������� ����������� ���������� ��������������� ��������, ���� �� ���������� ����� maxSpawnedEnemyIntoSpot,
            //�� ��������������� ����� ����� ���������
            if (spawnedEnemy == maxCountSpawnedEnemyInSpot)
            {
                //����� ������ �� ����� ����� ��������� �������� �� ������ ��������� �����, � �������������� ������������ �����
                 ChangeSpawnPosition(spawnPos);
                //����� ���������� ��������������� �������
                spawnedEnemy = 0;
            }

            //��������� ������� � ����� �� �� ������ ��������� ����� ���������
            Instantiate(typeEnemy[currentWave], EnemySpotsSpawn[spawnPos].position, transform.rotation);
            //��������� ������ ���������� ��������������� �������� 
            countEnemyForSpawn--;
            //��������� ������������� ���������� ��������������� �������� �� ����� �����
            spawnedEnemy++;
            //����� ����� ���������� �������
            yield return new WaitForSeconds(1f);
        }

        ////��������, ����� �� ��������� ����� �����
        //if (destroedEnemy >= countEnemy && currentWave < typeEnemy.Length - 1)
        //{
        //    RestarWave();
        //}
    }

    /// <summary>
    /// ���������� ����������� �����
    /// </summary>
    private void RestarWave()
    {
        //����� ���������� ������������ ��������
        destroedEnemy = 0;
        //��������� ������ �����
        currentWaves++;
        //���� ������� ����� ����� ����� ������� ������������ ��������,
        //�� ��������� ���������� ������������ �������� ������ 1 (��������� ������ ���� ���� ������)
        if (currentWaves == typeEnemy.Length - 1)
            countEnemy = 1;
        //����� �������� � ��������� ��������������� ��������� ������� �������� ����� �������
        StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy, timeBetweenWaves));
    }

    /// <summary>
    /// ����� ������ ��������� ��� ���������� ������ ���������� �������� ���������
    /// </summary>
    /// <param name="spawnPos">����� ����� ���������</param>
    private void ChangeSpawnPosition(int spawnPos)
    {
        //����� ������ �� ����� ����� ��������� �������� �� ������ ��������� �����, � �������������� ������������ �����
        SetPositionsForSpawn(spawnPos);
        //����� ������ ���������� ����������� ����� ��������� 
        //� ��������� � ����� ���������� ��������� ����� ��������� � ������ ���������� ��������� �� 1
        spawnPos = RandomValue(EnemySpotsSpawn.Count, true);

    }

    /// <summary>
    /// ����� ����������� ��������� ����� ��������� �����.
    /// ���� �������� changePos = true, �� ���������� ��������� ��������� �� 1, ��� ���������� ������������ �����
    /// <param name="value">������ ������ ����� ���������</param>
    /// <param name="changePos">���� ���������� ��������� ��������� ������</param>
    /// <returns></returns>
    private int RandomValue(int value, bool changePos = false)
    {
        if (!changePos)
            //����� ����� ��������� �� ����� ������
            return Random.Range(Mathf.FloorToInt(0), Mathf.FloorToInt(value)); 
        else
            //����� ����� ��������� �� ������ ��� ��������� �������
            return Random.Range(Mathf.FloorToInt(0), Mathf.FloorToInt(value - 1));
    }

    /// <summary>
    /// ���������� ������ ��������� ����� ��������� �� �������
    /// </summary>
    private void SetPositionsForSpawn(int indexSpot)
    {
        //�������� �� ������� ������ �� ������
        if(EnemySpotsSpawn != null)
        {
            //���������� ���������� ������������ ����� �� �������
            var temp = EnemySpotsSpawn[indexSpot];
            //�������� �� ������ �� �������
            EnemySpotsSpawn.RemoveAt(indexSpot);
            //���������� ���������� ���������� � ������ ����� ���������
            EnemySpotsSpawn.Add(temp);
        }
    }

    public void CalculateDestroedEnemy()
    {
        destroedEnemy++;
        if(destroedEnemy >= countEnemy && currentWaves < typeEnemy.Length - 1)
        {
            RestarWave();
        }
    }
}
