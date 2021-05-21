using UnityEngine;

public class LookAtMouseAndRotate : MonoBehaviour
{
    public float offset = 5f;   //Угол поворота игрока

    void Update()
    {
        //Получение значения курсора экрана в мировых координатах
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Вычисление угла поворота по оси Z
        float rotateZ = Mathf.Atan2(currentMousePos.y, currentMousePos.x) * Mathf.Rad2Deg;
        //Поворот на заданный угол по оси Z с добавлением укла поворота
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }
}
