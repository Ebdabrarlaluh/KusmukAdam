using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public Transform firePoint;
    public GameObject batFirePrefab;

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(batFirePrefab, firePoint.position, firePoint.rotation);
    }
}
