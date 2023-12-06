using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyShot : MonoBehaviour
{
    public GameObject bulletPerfab, enemyPrefab;// 弾のプレハブ /自身の親プレハブ
    public EnemySearch enemySearch;
    public int Maxremainingbullets, remainingbullets, ShotSpeed;//マガジンの装弾数 / マガジン内の残弾 /飛ばす力
    float timer = 0.0f;
    public float interval;

    private void Start()
    {
        remainingbullets = Maxremainingbullets;
        enemySearch = transform.parent.GetChild(0).GetComponent<EnemySearch>();
    }

    void Update()
    {
        if (enemySearch.IsCapture)
        {
            if (timer <= 0.0f)
            {
                Shot();
                timer = interval;
            }

            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
        }
    }
    public void Shot()
    {
        if (remainingbullets > 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPerfab, transform.position,
                Quaternion.Euler(
                    transform.position.x + Random.Range(-1.5f, 1.5f),
                    transform.position.y + Random.Range(-1.5f, 1.5f),
                    transform.position.z
                )
            );

            Rigidbody bulletOBJ = bullet.GetComponent<Rigidbody>();
            bulletOBJ.AddForce(transform.parent.forward * ShotSpeed);
            remainingbullets -= 1;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(3.0f);
        remainingbullets = Maxremainingbullets;
    }
}
