using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [HideInInspector] public int HealHP;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealHP = 100;
            Destroy(gameObject);
        }
    }
}
