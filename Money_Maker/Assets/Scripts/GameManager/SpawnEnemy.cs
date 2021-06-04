using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject gameObjectUIManager;  //Объект gameManager

    public GameObject player;               //Объект Player

    private UIManager uiManager;            //Компонент UIManager у объекта gameManager

    public List<Transform> EnemySpotsSpawn; //массив точек генерации объекта типа enemy
    public GameObject[] typeEnemy;          //массив типов enemy
    public int countEnemy;                  //Общее количество генерируемых объектов
    public int maxCountSpawnedEnemyInSpot;  //Максимальное количество генерируемых объектов на одной точке
    public float timeBetweenWaves;          //Время ожидания между китерацими генерации объектов

    [SerializeField]
    private List<GameObject> createdEnemy;  //Список созданных объектов Enemy

    [SerializeField]
    private int countEnemyWave;             //Количество волн врагов 
    [SerializeField]
    private int currentWaves;               //Номер текущей итерации генерации объектов
    [SerializeField]
    private int destroedEnemy;              //Количество уничтоженных врагов

    private int indexSpotForBoss = 0,       //Индекс точки генерации босса из списа точек генерации
                numberWaveMixedTypeEnemy = 1; //Показатель значения номера волны, для которой необходимо включить смешанную генерацию типа волны

    [SerializeField]
    private float ratioForMixedWave = 0.5f; //Коэффициент для выбора типа генерируемого объекта для волны со смешанной генерацией врагов

    private string messageAfterLAstWave = "YOU WIN!!";  //Текстовое сообщение о прохождении всех волн

    private void Start()
    {
        //Установка текущей волны в начале игра равной 0
        currentWaves = 0;
        //Получение компонента UIManager 
        uiManager = gameObjectUIManager.GetComponent<UIManager>();
    }

    /// <summary>
    /// Метод запуска генерации волн
    /// </summary>
    public void StartSpawnEnemy()
    {
        //Запуск корутины по генерации врагов с передачей в нее типа индекса имассива типа генерируемых врагов
        //StartCoroutine(SpawnEnemyInSpot(currentWaves, countEnemy));
    }

    /// <summary>
    /// Генерация объектов в зависимости от номера текущей волны, количества врагов в волне и с указанной задержкой между волнам
    /// Для первой волны, задерка равна 0
    /// </summary>
    /// <param name="currentWave">номер волны</param>
    /// <param name="countEnemyForWalve">количество врагов в волне</param>
    /// <param name="timeDelay">задержка между волнами</param>
    /// <returns></returns>
    IEnumerator SpawnEnemyInSpot(int currentWave, int countEnemyForWalve, float timeDelay = 0f)
    {
        //Задержка перед запуском(при запуске первой волны, задержка равняется 0). При перезапуске волны добавляется задержка
        yield return new WaitForSeconds(timeDelay);

        //Определение типа волны, если последняя, то генерация босса в центре карты (точка генерации с индексом 0 из списка точек генерации), 
        //иначе случайная генерация в различных точках
        int spawnPos = (currentWave == typeEnemy.Length - 1) ? indexSpotForBoss : RandomValue(EnemySpotsSpawn.Count);

        //Количество сгенерированных врагов
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

            //Если номер текущей волны равен номеру волны со смешанным типом объекта Enemy, то генерация волны из двух типов объектов Enemy
            if (currentWave == numberWaveMixedTypeEnemy)
            {
                //Генерация случайного типа объекта в точке из из списка возможных точек генерации
                GameObject createdEnemy = Instantiate(typeEnemy[ChoiceSpawnTypeEnemy()], EnemySpotsSpawn[spawnPos].position, transform.rotation);
                //Добавление объекта в список созданных объектов
                AddCreatedObjectToList(createdEnemy);
            }
            else
            {
                //Генерация объекта в точке из из списка возможных точек генерации
                GameObject createdEnemy = Instantiate(typeEnemy[currentWave], EnemySpotsSpawn[spawnPos].position, transform.rotation);
                //Добавление объекта в список созданных объектов
                AddCreatedObjectToList(createdEnemy);
            }

            //Декремент общего количества сгенерированных объектов 
            countEnemyForSpawn--;
            //Инкремент максимального количества сгенерированных объектов на одной точке
            spawnedEnemy++;
            //Пауза перед генерацией объекта 1 c
            yield return new WaitForSeconds(1f);
        }
    }

   /// <summary>
   /// Метод случайного выбора индекса типа объекта из массива объектов
   /// </summary>
   /// <returns></returns>
    private int ChoiceSpawnTypeEnemy()
    {
        if(Random.Range(0f, 1f) < ratioForMixedWave)
          return 0; //возврат индекса 0 для массива типов врагов
        else
          return 1; //возврат индекса 1 для массива типов врагов
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
        {
            countEnemy = 1;
        }

        //Очистка списка созданных объектов
        ClearListCreatedEnemy();

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
            //Выбор точки генерации из всего списка и исключая точку с индексом 0 (точка генерации босса)
            return Random.Range(Mathf.FloorToInt(1), Mathf.FloorToInt(value)); 
        else
            //выбор точки генерации из списка без последней позиции и исключая точку с индексом 0 (точка генерации босса)
            return Random.Range(Mathf.FloorToInt(1), Mathf.FloorToInt(value - 1));
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

    /// <summary>
    /// Подчсет уничтоженных объектов
    /// </summary>
    public void CalculateDestroedEnemy()
    {
        destroedEnemy++;
        //Проверка на запуск новой волны
        //Если количество уничтоженных врагов больше либо равно количеству врагов в волне и
        //номер текущей волны меньше, чем длина массива типов врагов - 1
        if(destroedEnemy >= countEnemy && currentWaves < typeEnemy.Length - 1)
        {
            RestarWave();
        }
        //Если количество уничтоженных врагов больше либо равно количеству врагов в волне и
        //номер текущей волны равен длина массива типов врагов - 1, то выход в меню Game Over и вывод сообщения о победе
        if (destroedEnemy >= countEnemy && currentWaves == typeEnemy.Length - 1)
        {
            //Вызов метода для показа меню окончания игры с передачей ему сообщения о прохождении игры
            uiManager.ShowGameOverMenu(messageAfterLAstWave);
            //Деактивация объекта Player
            player.gameObject.SetActive(false);
            //Установка скорости игры в 0
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// Заполнение списка созданных объектов 
    /// </summary>
    /// <param name="createdObjectEnemy"></param>
    private void AddCreatedObjectToList(GameObject createdObjectEnemy)
    {
        //Добавление в список созданного объекта Enemy
        createdEnemy.Add(createdObjectEnemy);
    } 

    /// <summary>
    /// Метод унитожения объектов после проигрыша
    /// </summary>
    public void DestoyAllEnemyAfterEnded()
    {
        //Прохождение по списку созданных объектов Enemy
        for (int i = 0; i < createdEnemy.Count; i++)
        {
            //Если ссылка на объект не пустая, то удаление объекта
            if (createdEnemy[i] != null)
                Destroy(createdEnemy[i]);
        }
        //Очистка списка созданных объектов
        ClearListCreatedEnemy();
    }
    
    /// <summary>
    /// Очистка списка созданных объектов типа Enemy
    /// </summary>
    private void ClearListCreatedEnemy()
    {
        //Очистка списка созданных объектов
        createdEnemy.Clear();
    }
}
