using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject gameObjectUIManager;  //������ gameManager

    public GameObject player;               //������ Player

    private UIManager uiManager;            //��������� UIManager � ������� gameManager

    public List<Transform> EnemySpotsSpawn; //������ ����� ��������� ������� ���� enemy
    public GameObject[] typeEnemy;          //������ ����� enemy
    public int countEnemy;                  //����� ���������� ������������ ��������
    public int maxCountSpawnedEnemyInSpot;  //������������ ���������� ������������ �������� �� ����� �����
    public float timeBetweenWaves;          //����� �������� ����� ���������� ��������� ��������

    [SerializeField]
    private List<GameObject> createdEnemy;  //������ ��������� �������� Enemy

    [SerializeField]
    private int countEnemyWave;             //���������� ���� ������ 
    [SerializeField]
    private int currentWaves;               //����� ������� �������� ��������� ��������
    [SerializeField]
    private int destroedEnemy;              //���������� ������������ ������

    private int indexSpotForBoss = 0,       //������ ����� ��������� ����� �� ����� ����� ���������
                numberWaveMixedTypeEnemy = 1; //���������� �������� ������ �����, ��� ������� ���������� �������� ��������� ��������� ���� �����

    [SerializeField]
    private float ratioForMixedWave = 0.5f; //����������� ��� ������ ���� ������������� ������� ��� ����� �� ��������� ���������� ������

    private string messageAfterLAstWave = "YOU WIN!!";  //��������� ��������� � ����������� ���� ����

    private void Start()
    {
        //��������� ������� ����� � ������ ���� ������ 0
        currentWaves = 0;
        //��������� ���������� UIManager 
        uiManager = gameObjectUIManager.GetComponent<UIManager>();
    }

    /// <summary>
    /// ����� ������� ��������� ����
    /// </summary>
    public void StartSpawnEnemy()
    {
        //������ �������� �� ��������� ������ � ��������� � ��� ���� ������� �������� ���� ������������ ������
        //StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy));
    }

    /// <summary>
    /// ��������� �������� � ����������� �� ������ ������� �����, ���������� ������ � ����� � � ��������� ��������� ����� ������
    /// ��� ������ �����, ������� ����� 0
    /// </summary>
    /// <param name="currentWave">����� �����</param>
    /// <param name="countEnemyForWalve">���������� ������ � �����</param>
    /// <param name="timeDelay">�������� ����� �������</param>
    /// <returns></returns>
    IEnumerator SpawnEnemyInSpot(int currentWave, int countEnemyForWalve, float timeDelay = 0f)
    {
        //�������� ����� ��������(��� ������� ������ �����, �������� ��������� 0). ��� ����������� ����� ����������� ��������
        yield return new WaitForSeconds(timeDelay);

        //����������� ���� �����, ���� ���������, �� ��������� ����� � ������ ����� (����� ��������� � �������� 0 �� ������ ����� ���������), 
        //����� ��������� ��������� � ��������� ������
        int spawnPos = (currentWave == typeEnemy.Length - 1) ? indexSpotForBoss : RandomValue(EnemySpotsSpawn.Count);

        //���������� ��������������� ������
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

            //���� ����� ������� ����� ����� ������ ����� �� ��������� ����� ������� Enemy, �� ��������� ����� �� ���� ����� �������� Enemy
            if (currentWave == numberWaveMixedTypeEnemy)
            {
                //��������� ���������� ���� ������� � ����� �� �� ������ ��������� ����� ���������
                GameObject createdEnemy = Instantiate(typeEnemy[ChoiceSpawnTypeEnemy()], EnemySpotsSpawn[spawnPos].position, transform.rotation);
                //���������� ������� � ������ ��������� ��������
                AddCreatedObjectToList(createdEnemy);
            }
            else
            {
                //��������� ������� � ����� �� �� ������ ��������� ����� ���������
                GameObject createdEnemy = Instantiate(typeEnemy[currentWave], EnemySpotsSpawn[spawnPos].position, transform.rotation);
                //���������� ������� � ������ ��������� ��������
                AddCreatedObjectToList(createdEnemy);
            }

            //��������� ������ ���������� ��������������� �������� 
            countEnemyForSpawn--;
            //��������� ������������� ���������� ��������������� �������� �� ����� �����
            spawnedEnemy++;
            //����� ����� ���������� ������� 1 c
            yield return new WaitForSeconds(1f);
        }
    }

   /// <summary>
   /// ����� ���������� ������ ������� ���� ������� �� ������� ��������
   /// </summary>
   /// <returns></returns>
    private int ChoiceSpawnTypeEnemy()
    {
        if(Random.Range(0f, 1f) < ratioForMixedWave)
          return 0; //������� ������� 0 ��� ������� ����� ������
        else
          return 1; //������� ������� 1 ��� ������� ����� ������
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
        {
            countEnemy = 1;
        }

        //������� ������ ��������� ��������
        ClearListCreatedEnemy();

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
            //����� ����� ��������� �� ����� ������ � �������� ����� � �������� 0 (����� ��������� �����)
            return Random.Range(Mathf.FloorToInt(1), Mathf.FloorToInt(value)); 
        else
            //����� ����� ��������� �� ������ ��� ��������� ������� � �������� ����� � �������� 0 (����� ��������� �����)
            return Random.Range(Mathf.FloorToInt(1), Mathf.FloorToInt(value - 1));
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

    /// <summary>
    /// ������� ������������ ��������
    /// </summary>
    public void CalculateDestroedEnemy()
    {
        destroedEnemy++;
        //�������� �� ������ ����� �����
        //���� ���������� ������������ ������ ������ ���� ����� ���������� ������ � ����� �
        //����� ������� ����� ������, ��� ����� ������� ����� ������ - 1
        if(destroedEnemy >= countEnemy && currentWaves < typeEnemy.Length - 1)
        {
            RestarWave();
        }
        //���� ���������� ������������ ������ ������ ���� ����� ���������� ������ � ����� �
        //����� ������� ����� ����� ����� ������� ����� ������ - 1, �� ����� � ���� Game Over � ����� ��������� � ������
        if (destroedEnemy >= countEnemy && currentWaves == typeEnemy.Length - 1)
        {
            //����� ������ ��� ������ ���� ��������� ���� � ��������� ��� ��������� � ����������� ����
            uiManager.ShowGameOverMenu(messageAfterLAstWave);
            //����������� ������� Player
            player.gameObject.SetActive(false);
            //��������� �������� ���� � 0
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// ���������� ������ ��������� �������� 
    /// </summary>
    /// <param name="createdObjectEnemy"></param>
    private void AddCreatedObjectToList(GameObject createdObjectEnemy)
    {
        //���������� � ������ ���������� ������� Enemy
        createdEnemy.Add(createdObjectEnemy);
    } 

    /// <summary>
    /// ����� ���������� �������� ����� ���������
    /// </summary>
    public void DestoyAllEnemyAfterEnded()
    {
        //����������� �� ������ ��������� �������� Enemy
        for (int i = 0; i < createdEnemy.Count; i++)
        {
            //���� ������ �� ������ �� ������, �� �������� �������
            if (createdEnemy[i] != null)
                Destroy(createdEnemy[i]);
        }
        //������� ������ ��������� ��������
        ClearListCreatedEnemy();
    }
    
    /// <summary>
    /// ������� ������ ��������� �������� ���� Enemy
    /// </summary>
    private void ClearListCreatedEnemy()
    {
        //������� ������ ��������� ��������
        createdEnemy.Clear();
    }
}
