using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class OBJManager : MonoBehaviour
{
    public GameManager GM;
    public MeshRenderer ground;
    public GameObject OBJDirecterPrefabs, empty;
    [HideInInspector] public float coroTime;
    [HideInInspector] public int Yrand;
    [HideInInspector] public Vector3 size;
    float halfExtents;
    Vector3 posi;
    void Start()
    {
        coroTime = GM.timer / 3;
        posi = ground.transform.position;
        size = ground.bounds.size;
        halfExtents = empty.GetComponent<SphereCollider>().radius;

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(StageSet(i));
        }
    }
    IEnumerator StageSet(int col)
    {
        yield return new WaitForSeconds(coroTime * col);
        var parent = transform;
        int counter = 0;
        while (counter < 50)
        {
            float rangex = Random.Range(-(size.x / 2.0f), size.x / 2.0f);
            float rangez = Random.Range(-(size.z / 2.0f), size.z / 2.0f);
            Vector3 vec = new(rangex + posi.x, 0.5f, rangez + posi.z);
            Yrand = Random.Range(1, 7) * 30;
            if (!Physics.CheckSphere(new(vec.x, vec.y + 5.0f, vec.z), halfExtents))
            {
                Instantiate(OBJDirecterPrefabs, vec, Quaternion.Euler(0, Yrand, 0), parent);
                counter++;
            }
        }
    }
}

