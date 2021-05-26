using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public List<Transform> EnemySpotsSpawn; //массив точек генерации объекта типа enemy
    public GameObject[] typeEnemy;          //массив типов enemy

    public int countEnemy;                  //Общее количество генерируемых объектов
    public int maxCountSpawnedEnemyInSpot;  //Максимальное количество генерируемых объектов на одной точке
    public float timeBetweenWaves;          //Время ожидания между китерацими генерации объектов

    [SerializeField]
    private int countEnemyWave;             //Количество волн врагов 
    [SerializeField]
    private int currentWaves;               //Номер текущей итерации генерации объектов
    [SerializeField]
    private int destroedEnemy;              //Количество уничтоженных врагов

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
        //Запуск корутины по генерации врагов с передачей в нее типа индекса имассива типа генерируемых врагов
        StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy));
    }

    IEnumerator SpawnEnemyInSpot(int currentWave, int countEnemyForWalve, float timeDelay = 0f)
    {
        //Задержка перед запуском(при запуске первой волны, задержка равняется 0)
        //При перезапуске волны добавляется задерка при запуске волны
        yield return new WaitForSeconds(timeDelay);
        //Определение рандомной точки генерации. 
        //Вызов метода случайного определения точки генерации из списка возможных точек генерации
        int spawnPos = RandomValue(EnemySpotsSpawn.Count);
        //Количество сгенереированных врагов
        int spawnedEnemy = 0;
        //Количеств оставшишся объектов генерации в волне
        int countEnemyForSpawn = countEnemyForWalve;

        //Генерация врагов, пока количество врагов в волне больше 0;
        while (countEnemyForSpawn > 0)
        {
            //Подсчет оставшегося количества сгенерированных объектов, если их количество равно maxSpawnedEnemyIntoSpot,
            //то переопределение новой точки генерации
            if (spawnedEnemy == maxCountSpawnedEnemyInSpot)
            {
                //вызов метода по смене точек генерации объектов из списка возможных точек, с игнорированием отработавшей точки
                 ChangeSpawnPosition(spawnPos);
                //Сброс количества сгенерированных объктов
                spawnedEnemy = 0;
            }

            //Генерация объекта в точке из из списка возможных точек генерации
            Instantiate(typeEnemy[currentWave], EnemySpotsSpawn[spawnPos].position, transform.rotation);
            //Декремент общего количества сгенерированных объектов 
            countEnemyForSpawn--;
            //Инкремент максимального количества сгенерированных объектов на одной точке
            spawnedEnemy++;
            //Пауза перед генерацией объекта
            yield return new WaitForSeconds(1f);
        }

        ////Проверка, нужно ли запускать новую волну
        //if (destroedEnemy >= countEnemy && currentWave < typeEnemy.Length - 1)
        //{
        //    RestarWave();
        //}
    }

    /// <summary>
    /// Реализация пеерзапуска волны
    /// </summary>
    private void RestarWave()
    {
        //Сброс количества уничтоженных объектов
        destroedEnemy = 0;
        //Инкремент номера волны
        currentWaves++;
        //Если текущая волна равна длине массива генерируемых объектов,
        //то установка количества генерируемых объектов равная 1 (генерация объект типа босс уровня)
        if (currentWaves == typeEnemy.Length - 1)
            countEnemy = 1;
        //Старт корутины с передачей дополнительного параметра времени задержки перед стартом
        StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy, timeBetweenWaves));
    }

    /// <summary>
    /// Смена точеки генерации при исчерпании лимита количества объектов генерации
    /// </summary>
    /// <param name="spawnPos">Номер точки генерации</param>
    private void ChangeSpawnPosition(int spawnPos)
    {
        //вызов метода по смене точек генерации объектов из списка возможных точек, с игнорированием отработавшей точки
        SetPositionsForSpawn(spawnPos);
        //Вызов метода случайного определения точки генерации 
        //с передачей в метод количества возможных точек генерации и флагом уменьшения диапазона на 1
        spawnPos = RandomValue(EnemySpotsSpawn.Count, true);

    }

    /// <summary>
    /// Метод определения рандомной точки генерации числа.
    /// Если параметр changePos = true, то уменьшение диапазона генерации на 1, для исключения отработавшей точки
    /// <param name="value">Длинна списка точек генерации</param>
    /// <param name="changePos">Флаг уменьшения диапазона генерации чиисла</param>
    /// <returns></returns>
    private int RandomValue(int value, bool changePos = false)
    {
        if (!changePos)
            //Выбор точки генерации из всего списка
            return Random.Range(Mathf.FloorToInt(0), Mathf.FloorToInt(value)); 
        else
            //выбор точки генерации из списка без последней позиции
            return Random.Range(Mathf.FloorToInt(0), Mathf.FloorToInt(value - 1));
    }

    /// <summary>
    /// Заполнение списка возможных точек генерации из массива
    /// </summary>
    private void SetPositionsForSpawn(int indexSpot)
    {
        //Проверка на наличие ссылки на список
        if(EnemySpotsSpawn != null)
        {
            //Сохранение трансформа отработавшей точки по индексу
            var temp = EnemySpotsSpawn[indexSpot];
            //Удаление из списка по индексу
            EnemySpotsSpawn.RemoveAt(indexSpot);
            //Добавление удаленного трансформа в список точек генерации
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
