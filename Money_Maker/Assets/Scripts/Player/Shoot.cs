using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gameManager;      //������ ���������� �����
    public GameObject UIManager;        //������ ���������� ����
    public GameObject bullet;           //������ ����
    public GameObject gun;              //������ ������

    public float delayTimeShooting;     //�������� ����� ����������

    private ShopAmmo shopAmmo;

    private float startTimeShooting;    //����� �� ������ ��������

    private int countAmmo;              //����� �������� 
    private int ammoInMagazine;         //���������� �������� � ��������

    [SerializeField]
    private int currentCountAmmo;       //����� ��������
    [SerializeField]
    private int currentAmmoInMagazine;  //������� ���������� �������� � ��������
    private bool isReloading = false;   //���� ���������� � �������� �����������

    private string messageReload = "RELOAD";                //��������� ��������� �� ����� ��� ������������� �����������
    private string messageBuyAmmo = "NEED BUY THE AMMO!!!"; //��������� ��������� �� ����� ��� ������������� ������� �����������


    public int CurrentCountAmmo { get => currentCountAmmo; set => currentCountAmmo = value; }
    public int CurrentAmmoInMagazine { get => currentAmmoInMagazine; set => currentAmmoInMagazine = value; }

    private void Start()
    {
        //������������ ����� ��������
        countAmmo = gameManager.GetComponent<ShootControl>().MaxCountAmmo;
        //��������� ���������� �������� � �������� �� ��������
        ammoInMagazine = gameManager.GetComponent<ShootControl>().MaxCountAmmoInMagazine;

        shopAmmo = gameManager.GetComponent<ShopAmmo>();

        CurrentCountAmmo = countAmmo;
        CurrentAmmoInMagazine = ammoInMagazine;
    }

    void Update()
    {
        //�������� ������� �� ������ ��������, ���� ������� 0, �� �������� ������ �� ������ ��������, ����� ������ ������� 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //�������� �������� ��������� �������� � ��������, ���� ������ 0, �� ������ �������, ����� ���� ��������� �� ������������
                if(CurrentAmmoInMagazine > 0)
                {
                    //����� �������� ��������
                    StartCoroutine(DoShoot());
                    //����� �����������
                    startTimeShooting = delayTimeShooting;
                    //��������� �������� ���������� �������� � ��������
                    CurrentAmmoInMagazine--;
                }
                else
                {
                    //������ ������ ��� ������ ���������� �� ����������� � ��������� ������ ���������
                    UIManager.GetComponent<UIManager>().ShowAnnounceMenu(messageReload);
                }
            }

            //���� ������ ������� R � ������� ���������� �������� �� ����� ������������� ���������� �������� � ��������, ����� ������ ����������� ��������
            if (Input.GetKeyDown(KeyCode.R) && CurrentAmmoInMagazine != ammoInMagazine)
            {
                //����� �������� �����������
                StartCoroutine(ReloadAmmo(CurrentAmmoInMagazine));
            }

        }  else startTimeShooting -= Time.deltaTime;  //�������� ����� ����������

        if (Input.GetMouseButtonDown(1))
        {
            shopAmmo.TryBuyAmmo();
        }
    }

    /// <summary>
    /// ���������� �������� 
    /// </summary>
    public IEnumerator DoShoot()
    {
        //�������� ������� ���� � ����� ��������� ���� � �������� �� ����������� ��������
        Instantiate(bullet, gun.transform.position, transform.rotation).GetComponent<Bullet>();
        //������������ ����� ��������
        //shootSound.Play();
        gun.GetComponent<GunSound>().PlaySoundClip(0);
        yield return null;
    }


    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="valueAmmoInMagazine">���������� ���������� �������� � ��������</param>
    private IEnumerator ReloadAmmo(int valueAmmoInMagazine)
    {
        //�������� �� ������� ��������, ���� ������ 0, �� ����������� ��������
        if (CurrentCountAmmo > 0)
        {
            if (CurrentCountAmmo >= ammoInMagazine)
            {
                //������������ ����� �����������, ����� ������ PlaySoundClip � ������� gun � ��������� ������� �������������� ����� 1.
                gun.GetComponent<GunSound>().PlaySoundClip(1);
                //��������� �������� �������� �������� � �������� ������ ���������� ��������, ������� ������ ���� � � ��������
                CurrentAmmoInMagazine = ammoInMagazine;
                //��������� �� ������ ���������� �������� ����������, ������� ������ ���� � � ��������
                CurrentCountAmmo -= CurrentAmmoInMagazine;
                //���������� � ����������� ��������� ��������, ���������� �������� ��� �����������
                CurrentCountAmmo += valueAmmoInMagazine;
            }
            else
            {
                //��������� �������� �������� �������� � �������� ������ ������ ���������� ���������� ��������
                CurrentAmmoInMagazine += CurrentCountAmmo;
                //����� � 0 ����������� ���������� ��������
                CurrentCountAmmo = 0;
                //�������� �� ���������� ���������� �������� ��������;
                if (CurrentAmmoInMagazine > ammoInMagazine)
                {
                    //������� ������ ��������  �� �������� � ����� ���������� ��������
                    CurrentCountAmmo = CurrentAmmoInMagazine - ammoInMagazine;
                    //��������� �������� �������� �������� � �������� ������ ���������� ��������, ������� ������ ���� � � ��������
                    CurrentAmmoInMagazine = ammoInMagazine;
                }
            }
        }
        else
        {
            //��������� �������� �������� currentCountAmmo � 0
            CurrentCountAmmo = 0;
            //������ ������ ��� ������ ���������� �� ������������� ������� ����������� � ��������� ������ ���������
            UIManager.GetComponent<UIManager>().ShowAnnounceMenu(messageBuyAmmo);
        }
        yield return null;
    }
}
