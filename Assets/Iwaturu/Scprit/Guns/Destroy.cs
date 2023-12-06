using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject hitPre;
    RaycastHit hit;
    Vector3 hitPos;
    private void Update()
    {
        Ray ray = new(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (!(hit.collider.gameObject.tag == "Player"))
            {
                hitPos = hit.point;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "bullet"))
        {
            Instantiate(hitPre, hitPos, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
