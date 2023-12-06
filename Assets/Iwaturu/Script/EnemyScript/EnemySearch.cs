using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    [HideInInspector] public bool IsCapture;
    GameObject[] targets;
    public int index;

    private void Start()
    {
        targets = transform.parent.GetComponent<EnemyAction>().targets;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (other.gameObject.name == targets[i].name)
                {
                    index = i;
                    break;
                }
            }
            IsCapture = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsCapture = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (other.gameObject.name == targets[i].name)
                {
                    index = i;
                    break;
                }
            }
            IsCapture = true;
        }

    }
}
