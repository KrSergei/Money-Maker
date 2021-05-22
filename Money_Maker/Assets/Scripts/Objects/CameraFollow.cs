using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objToFollow;       //Позиция ведущего объекта
    private Vector3 deltaPosition;      //Расстояние между объектами

    void Start()
    {
        //Расчет расстояния между объектами - текущая позиция объекта минус позиция ведущего объекта
        deltaPosition = transform.position - objToFollow.position;
    }

    //Изменение позиции объекта - текущая позиция объекта плюс расстояние между объектами
    void Update()
    {
        transform.position = objToFollow.position + deltaPosition;
    }
}
