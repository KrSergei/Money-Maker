using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gameManager;      //������ ���������� �����

    public GameObject bullet;           //������ ����
    public Transform  shootPos;         //����� ��������� ����

    public float delayTimeShooting;     //�������� ����� ����������
    public float offset;
    private float startTimeShooting;    //����� �� ������ ��������

    private int maxCountAmmo;           //����� ���������� ���������� �������� 
    private int ammoInMagazine;         //���������� �������� � ��������, ������� ������ ���� � � ��������

    [SerializeField]
    private int currentCountAmmo;       //����� ���������� ��������
    [SerializeField]
    private int currentAmmoInMagazine;  //������� ���������� �������� � ��������

    private void Start()
    {
        //������������ ���������� ��������
        maxCountAmmo = gameManager.GetComponent<ShootControl>().MaxCountAmmo;
        //��������� ���������� �������� � �������� �� ��������
        ammoInMagazine = gameManager.GetComponent<ShootControl>().MaxCountAmmoInMagazine;

        currentCountAmmo = maxCountAmmo;
        currentAmmoInMagazine = ammoInMagazine;

        Debug.Log("currentCountAmmo : " + currentCountAmmo);
        Debug.Log("currentAmmoInMagazine : " + currentAmmoInMagazine);
    }

    void Update()
    {
        //�������� ������ �� ������, ���� ������� 0, �� �������� ������ �� ������ ��������, ����� ������ ������� 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //�������� �������� ��������� �������� � ��������, ���� ������ 0, �� ������ �������, ����� ���� ��������� �� ������������
                if(currentAmmoInMagazine > 0)
                {
                    //����� �������� ��������
                    StartCoroutine(DoShoot());
                    //����� �����������
                    startTimeShooting = delayTimeShooting;
                    //��������� �������� ���������� �������� � ��������
                    currentAmmoInMagazine--;
                }
                else
                {
                    Debug.Log("NEED RELOAD");
                }
            }

            //���� ������ ������� R � ������� ���������� �������� �� ����� ������������� ���������� �������� � ��������, ����� ������ ����������� ��������
            if (Input.GetKeyDown(KeyCode.R) && currentAmmoInMagazine != ammoInMagazine)
            {
                Debug.Log("RELOAD");
                ReloadAmmo(currentAmmoInMagazine);
            }

        }  else startTimeShooting -= Time.deltaTime;  //�������� ����� ����������
    }

    /// <summary>
    /// ���������� �������� 
    /// </summary>
    public IEnumerator DoShoot()
    {
        //�������� ������� ���� � ����� ��������� ���� � �������� �� ����������� ��������
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>();
        yield return null;
    }

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="valueAmmoInMagazine">���������� ���������� �������� � ��������</param>
    private void ReloadAmmo(int valueAmmoInMagazine)
    {
        //�������� �� ������� ��������, ���� ������ 0, �� ����������� ��������
        if (currentCountAmmo > 0)
        {
            if (currentCountAmmo >= ammoInMagazine)
            {
                //��������� �������� �������� �������� � �������� ������ ���������� ��������, ������� ������ ���� � � ��������
                currentAmmoInMagazine = ammoInMagazine;
                //��������� �� ������ ���������� �������� ����������, ������� ������ ���� � � ��������
                currentCountAmmo -= currentAmmoInMagazine;
                //���������� � ����������� ��������� ��������, ���������� �������� ��� �����������
                currentCountAmmo += valueAmmoInMagazine;
            }
            else
            {
                //��������� �������� �������� �������� � �������� ������ ������ ���������� ���������� ��������
                currentAmmoInMagazine += currentCountAmmo;
                //����� � 0 ����������� ���������� ��������
                currentCountAmmo = 0;
                //�������� �� ���������� ���������� �������� ��������;
                if (currentAmmoInMagazine > ammoInMagazine)
                {
                    //������� ������ ��������  �� �������� � ����� ���������� ��������
                    currentCountAmmo = currentAmmoInMagazine - ammoInMagazine;
                    //��������� �������� �������� �������� � �������� ������ ���������� ��������, ������� ������ ���� � � ��������
                    currentAmmoInMagazine = ammoInMagazine;
                }
            }
        }
        else
        {
            //��������� �������� �������� currentCountAmmo � 0
            currentCountAmmo = 0;
            Debug.Log("NEED BUY THE AMMO!!!");
        }
    }
}
