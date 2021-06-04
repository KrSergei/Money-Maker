using UnityEngine;

public class GunSound : MonoBehaviour
{
    //Массив звуковых дорожек
    public AudioClip[] soundsGun;

    private AudioSource gun;

    public void PlaySoundClip(int index)
    {
        gun = gameObject.GetComponent<AudioSource>();

        gun.clip = soundsGun[index];

        gun.Play();
    }
}
