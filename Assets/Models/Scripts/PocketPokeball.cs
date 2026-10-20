using System;
using UnityEngine;
using UnityEngine.XR;

public class PocketPokeball : MonoBehaviour
{
    private MeshRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("HandOrController") ) _renderer.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("HandOrController") ) _renderer.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // if ( grab )
    }
}
