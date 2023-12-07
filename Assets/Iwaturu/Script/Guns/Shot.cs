using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shot : MonoBehaviour
{
    public GameObject bulletPerfab;// 弾のプレハブ
    public int Maxremainingbullets, remainingbullets, ShotSpeed;//マガジンの装弾数 / マガジン内の残弾 /飛ばす力
    public float interval;
    public AudioClip[] sound;
    AudioSource SE;
    float timer;
    private void Start()
    {
        remainingbullets = Maxremainingbullets;
        SE = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetButton("Fire1") && timer <= 0.0f)
        {
            Shoot();
            timer = interval;
        }

        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

    }
    public void Shoot()
    {
        if (remainingbullets > 0)
        {
            GameObject bullet = Instantiate(bulletPerfab, transform.position,
                Quaternion.Euler(
                    transform.position.x + Random.Range(-1.5f, 1.5f),
                    transform.position.y + Random.Range(-1.5f, 1.5f),
                    transform.position.z
                )
            );
            Rigidbody bulletOBJ = bullet.GetComponent<Rigidbody>();
            bulletOBJ.AddForce(transform.forward * ShotSpeed);
            ShotSE();
            remainingbullets -= 1;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    void ShotSE()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            SE.PlayOneShot(sound[i]);
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(3.0f);
        remainingbullets = Maxremainingbullets;
    }

}
