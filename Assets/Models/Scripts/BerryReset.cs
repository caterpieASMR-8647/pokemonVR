using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BerryReset : MonoBehaviour
{
    private Vector3 ogPosition = Vector3.zero;
    private float collisionCount = 0;
    
    [SerializeField] private GameObject player;
    
    private void Start()
    {
        ogPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < 0 || Mathf.Abs((player.transform.position - transform.position).magnitude) > 20)
        {
            GetComponent<Rigidbody>().transform.position = ogPosition;
            Debug.Log("teleport distanza");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            GetComponent<Rigidbody>().transform.position = ogPosition;
            Debug.Log("teleport");
        }
    }
}
