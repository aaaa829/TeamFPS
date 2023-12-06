using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJDirector : MonoBehaviour
{
    public GameObject[] itemDropPrefabs, OBJPrefabs, NoOBJPrefabs;
    [HideInInspector] public int OBJHP;
    [HideInInspector] public float coroTime;
    bool isTimerBreak;
    int rot;

    private void Awake()
    {
        rot = transform.parent.GetComponent<OBJManager>().Yrand;
        coroTime = transform.parent.GetComponent<OBJManager>().coroTime;
        int rand = Random.Range(0, 100);
        if (rand < 40)
        {
            Instans(OBJPrefabs);
        }
        else
        {
            Instans(NoOBJPrefabs);
        }
    }
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Drop()
    {
        if (isTimerBreak == false)
        {
            Instans(itemDropPrefabs);
        }
    }
    public void SetOBJHp(int hp, bool timerB)
    {
        OBJHP = hp;
        isTimerBreak = timerB;
    }
    void Instans(GameObject[] prefab)
    {
        var parent = transform;
        int index = Random.Range(0, prefab.Length);

        Instantiate(prefab[index], transform.position, Quaternion.Euler(0, rot, 0), parent);
    }
}
