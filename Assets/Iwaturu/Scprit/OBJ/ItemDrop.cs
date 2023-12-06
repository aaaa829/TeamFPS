using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    [HideInInspector] public int HP;

    void Start()
    {
        HP = transform.parent.GetComponent<OBJDirector>().OBJHP;
        int randDrop = Random.Range(2, 6);
        for (int i = 0; i < randDrop; i++)
        {
            int rand = Random.Range(0, 100);
            if (rand < 80)
            {
                // Score
                Instans(0, -90);
            }
            else
            {
                // Heal
                Instans(1, 0);
            }
        }
    }
    private void Update()
    {
        if (itemPrefabs.Length <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    void Instans(int i, int j)
    {
        var parent = this.transform;
        float rangeXZ = Random.Range(1.5f, 2f);
        float randX = Random.Range(-rangeXZ, rangeXZ);
        float randZ = Random.Range(-rangeXZ, rangeXZ);
        GameObject item = Instantiate(itemPrefabs[i], transform.position + new Vector3(0, 2.0f, 0),
            Quaternion.Euler(j, 0, 0), parent);
        Rigidbody rd = item.GetComponent<Rigidbody>();
        rd.AddForce(new Vector3(randX, 7f, randZ), ForceMode.Impulse);
    }
}
