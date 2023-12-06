using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{

    [SerializeField] private GameObject _ar;
    [SerializeField] private GameObject _smg;
    [SerializeField] private GameObject _sg;


    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            ChangeNewGun();
        }
    }

    public void ChangeNewGun()
    {

        if (GameObject.Find("_ar"))
        {
            GameObject newGun = Instantiate(_smg, transform);
            Destroy(_ar);
        }
        if (GameObject.Find("_smg"))
        {
            GameObject newGun = Instantiate(_sg, transform);
            Destroy(_smg);
        }
        if (GameObject.Find("_sg"))
        {
            GameObject newGun = Instantiate(_ar, transform);
            Destroy(_sg);
        }

    }
}
