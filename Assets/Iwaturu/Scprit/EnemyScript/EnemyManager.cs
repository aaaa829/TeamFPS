using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour

{
    public GameObject enemyPrefab, empty;
    public MeshRenderer ground;
    public GameManager GM;
    [HideInInspector] public Vector3 size;
    Vector3 posi;
    float coroTime, halfExtents;
    void Start()
    {
        coroTime = GM.timer / 2;
        posi = ground.transform.position;
        size = ground.bounds.size;
        halfExtents = empty.GetComponent<SphereCollider>().radius;
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(EnemySet(i));
        }
    }
    IEnumerator EnemySet(int col)
    {
        yield return new WaitForSeconds(coroTime * col);
        var parent = transform;
        int counter = 0;
        while (counter < 10)
        {
            float rangex = Random.Range(-(size.x / 2.0f), size.x / 2.0f);
            float rangez = Random.Range(-(size.z / 2.0f), size.z / 2.0f);
            Vector3 vec = new(rangex + posi.x, 0f, rangez + posi.z);
            if (!Physics.CheckSphere(new(vec.x, vec.y + 5.0f, vec.z), halfExtents))
            {
                Instantiate(enemyPrefab, vec, Quaternion.Euler(0, 0, 0), parent);
                counter++;

            }
        }
    }
}
