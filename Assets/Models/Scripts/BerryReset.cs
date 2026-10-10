using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BerryReset : MonoBehaviour
{
    private Vector3 ogPosition = Vector3.zero;
    
    [SerializeField] private Transform player;
    
    private Rigidbody rigidbody = null;
    
    private void Start()
    {
        ogPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0 || Mathf.Abs((player.transform.position - transform.position).magnitude) > 20)
        {
            rigidbody.transform.position = ogPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            rigidbody.transform.position = ogPosition;
        }
    }
}
