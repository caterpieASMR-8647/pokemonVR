using System;
using UnityEngine;

public class PocketPokeball : MonoBehaviour
{
    private MeshRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if ( other.tag == )
        _renderer.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _renderer.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // if ( )
    }
}
