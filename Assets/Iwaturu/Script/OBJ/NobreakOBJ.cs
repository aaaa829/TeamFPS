using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobreakOBJ : MonoBehaviour
{
    OBJDirector OBJ;
    Vector3 size;
    bool isTimerBreak;

    void Start()
    {
        OBJ = transform.parent.gameObject.GetComponent<OBJDirector>();
        size = transform.parent.parent.GetComponent<OBJManager>().size;
        isTimerBreak = false;
    }
    void Update()
    {
        StartCoroutine(Breaker());

        if (isTimerBreak)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Breaker()
    {
        yield return new WaitForSeconds(OBJ.coroTime);
        isTimerBreak = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OBJ" || other.gameObject.tag == "FildOBJ")
        {
            float rangeX = Random.Range(-(size.x / 2.0f), size.x / 2.0f);
            float rangeZ = Random.Range(-(size.z / 2.0f), size.z / 2.0f);
            transform.parent.position = new Vector3(rangeX, transform.position.y, rangeZ);
        }
    }
}
