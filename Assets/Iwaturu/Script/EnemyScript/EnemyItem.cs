using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyItem : MonoBehaviour
{
    [HideInInspector] public int score;
    void Start()
    {
        score = 3000;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
