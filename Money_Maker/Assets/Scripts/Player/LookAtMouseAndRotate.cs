using UnityEngine;

public class LookAtMouseAndRotate : MonoBehaviour
{
    public float offset = 5f;   //Угол поворота игрока

    //private Vector3 currentMousePos;    //Текущая позиция курсора мышки
    private Vector2 currentMousePos;    //Текущая позиция курсора мышки
    private Vector2 playerPos;

    void Update()
    {
        //Получение значения курсора экрана в мировых координатах
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Вычисление угла поворота по оси Z
        float rotateZ = Mathf.Atan2(currentMousePos.y, currentMousePos.x) * Mathf.Rad2Deg;
        //Поворот на заданный угол по оси Z с добавлением укла поворота
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// Возврат текущей позиции мышки
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMousePos()
    {
        return currentMousePos;
    }
}
