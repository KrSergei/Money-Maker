using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public Transform[] EnemySpotsSwpawn;    //массив точек генерации объекта типа enemy

    public int countEnemy;                  //Общее количество генерируемых объектов
    public int maxSpawnCountEnemy;          //Максимальное количество генерируемых объектов на одной точке

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
