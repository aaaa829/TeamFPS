using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class OBJDestroy : MonoBehaviour
{
    [HideInInspector] public int MAXHP;
    [HideInInspector] public bool isTimerBreak;
    int HP, breakDamage;
    Bullet BulletATK;
    OBJDirector OBJ;
    Vector3 Object, size;

    private void Awake()
    {
        OBJ = transform.parent.GetComponent<OBJDirector>();
        Object = gameObject.GetComponent<Transform>().position;
        size = transform.parent.parent.GetComponent<OBJManager>().size;

    }

    private void Start()
    {
        isTimerBreak = false;
        breakDamage = 0;
        MAXHP = Random.Range(1, 7) * 100;
        HP = MAXHP;
        transform.parent.GetComponent<OBJDirector>().SetOBJHp(MAXHP, isTimerBreak);
    }

    private void Update()
    {
        StartCoroutine(Breaker());
        Breaker();
        Object = new Vector3(Object.x, Object.y - 1, Object.z);
        HP -= breakDamage;
        if (isTimerBreak)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            BulletATK = other.gameObject.GetComponent<Bullet>();
            HP -= BulletATK.attack;
            if (HP <= 0 && HP >= -99)
            {
                OBJ.Drop();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "OBJ" || other.gameObject.tag == "FildOBJ")
        {
            float rangeX = Random.Range(-(size.x / 2.0f) - 2.05f, size.x / 2.0f - 2.05f);
            float rangeZ = Random.Range(-(size.z / 2) + 1.06f, size.z / 2 + 1.06f);
            transform.parent.position = new Vector3(rangeX, transform.position.y, rangeZ);
        }
    }
    IEnumerator Breaker()
    {
        yield return new WaitForSeconds(OBJ.coroTime);
        isTimerBreak = true;
    }
}
