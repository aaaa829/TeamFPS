using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    const int MaxHp = 100;
    public float speed;
    public GameObject itemPrefab;
    [HideInInspector] public int hp, index;
    [HideInInspector] public GameObject[] targets;
    EnemySearch enemySearch;
    Rigidbody rb;
    Animator animator;
    Vector3 result;

    private void Awake()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
        hp = MaxHp;
        speed = 10.0f;
        enemySearch = transform.GetChild(0).GetComponent<EnemySearch>();
        rb = GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        bool capture = enemySearch.IsCapture;
        if (capture == false)
        {
            rb.velocity = transform.forward * speed;
            animator.SetBool("walk", true);
        }
        else
        {
            index = enemySearch.index;
            PlayerLook(index);
            rb.velocity = Vector3.zero;

            animator.SetBool("walk", false);
        }
        if (targets.Length <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void PlayerLook(int index)
    {
        Vector3 relativePos = targets[index].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "OBJ" || other.gameObject.tag == "FildOBJ")
        {
            // 入射ベクトル（速度）
            var inDirection = rb.velocity;
            // 法線ベクトル
            var inNormal = transform.up;
            // 反射ベクトル（速度）
            result = Vector3.Reflect(inDirection, inNormal);
        }

    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "FildOBJ" || other.gameObject.tag == "OBJ")
        {
            float rote;
            if (result.y < 90.0f)
            {
                rote = 3.0f;
            }
            else
            {
                rote = -3.0f;
            }
            transform.Rotate(0, rote, 0);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Bullet bulletATK = other.gameObject.GetComponent<Bullet>();
            hp -= bulletATK.attack;
            if (hp <= 0)
            {
                float rangeXZ = Random.Range(1.5f, 2f);
                float randX = Random.Range(-rangeXZ, rangeXZ);
                float randZ = Random.Range(-rangeXZ, rangeXZ);
                GameObject item = Instantiate(itemPrefab, transform.position,
                    Quaternion.Euler(-90, 0, 0));
                Rigidbody rd = item.GetComponent<Rigidbody>();
                rd.AddForce(new Vector3(randX, 7f, randZ), ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
    //アニメーションエラー対応用
    private void OnFootstep()
    {

    }

    //アニメーションエラー対応用
    private void OnLand()
    {

    }
}
