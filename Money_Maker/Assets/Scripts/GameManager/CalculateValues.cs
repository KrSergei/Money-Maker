using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateValues : MonoBehaviour
{
    public GameObject uiManager;
    private UIPlaying uiPlaying;            //Игровой интефейс

    [SerializeField]
    private int pointForKilledEnemy;  //Количество уничтоженных врагов

    public int PointForKilledEnemy { get => pointForKilledEnemy; set => pointForKilledEnemy = value; }

    private void Start()
    {
        uiPlaying = uiManager.GetComponent<UIPlaying>();
    }

    /// <summary>
    /// Подчет очков за уничтожение объекта типа Enemy
    /// </summary>
    /// <param name="value">Цена за уничтоженный объект</param>
    public void SetPoint(int value)
    {
        PointForKilledEnemy += value;
        //Вызов меода для определния количества уничтоженных объъектов
        gameObject.GetComponent<SpawnEnemy>().CalculateDestroedEnemy();
    }
}
