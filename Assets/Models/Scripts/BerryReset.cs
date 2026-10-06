using System;
using Unity.VisualScripting;
using UnityEngine;

public class BerryReset : MonoBehaviour
{
    private Vector3 ogPosition = Vector3.zero;
    
    private void Start()
    {
        ogPosition = transform.position;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Terrain")
        {
            c.gameObject.transform.position = ogPosition;
            
        }
    }
}
