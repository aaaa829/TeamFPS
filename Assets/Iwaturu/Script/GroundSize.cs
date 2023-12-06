using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSize : MonoBehaviour
{
    [HideInInspector] public Vector3 size;
    void Awake()
    {
        size = GetComponent<MeshRenderer>().bounds.size;
    }
}
