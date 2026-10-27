using System;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VectorGraphics;
using UnityEngine;

public class PocketPokeball : MonoBehaviour
{
    [SerializeField] private Scene cityScene;
    
    [SerializeField] private GameObject pokeball;
    [SerializeField] private string _handTag = "PlayerHand";

    private class HandState
    {
        public GrabInteractor GrabInteractor;
        public HandGrabInteractor HandGrabInteractor;
        public bool WasSelecting;
    }

    private readonly Dictionary<Collider, HandState> _inside = new();
    
    private MeshRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_handTag) || _inside.ContainsKey(other)) return;

        _inside[other] = new HandState
        {
            // Cerca l'interactor sul collider o sui parent della mano
            GrabInteractor = other.GetComponentInParent<GrabInteractor>(),
            HandGrabInteractor = other.GetComponentInParent<HandGrabInteractor>(),
            WasSelecting = false
        };
    }

    private void OnTriggerExit(Collider other)
    {
        _inside.Remove(other);
    }
    
    // private void Update()
    private void OnTriggerStay(Collider other)
    {
        if (!_inside.TryGetValue(other, out HandState state)) return;
        
        bool selecting = IsSelecting(state);
        
        if (selecting && !state.WasSelecting)
        {
            SpawnAndGrab(state, other.transform);
        }
        state.WasSelecting = selecting;
        
        // foreach (var kv in _inside)
        // {
        //     var state = kv.Value;
        //     bool selecting = IsSelecting(state);
        //
        //     // Fronte di salita: grab appena premuto
        //     if (selecting && !state.WasSelecting)
        //     {
        //         // SpawnAndGrab(state, kv.Key.transform);
        //         SpawnAndGrab(state, other.transform);
        //     }
        //     state.WasSelecting = selecting;
        // }
    }
    
    private bool IsSelecting(HandState s)
    {
        if (s.GrabInteractor != null)
            return s.GrabInteractor.State == InteractorState.Select;
        if (s.HandGrabInteractor != null)
            return s.HandGrabInteractor.State == InteractorState.Select;
        return false;
    }

    private void SpawnAndGrab(HandState s, Transform handTransform)
    {
        if ( GameObject.FindGameObjectWithTag( "Pokeball" ) ) return;
        
        var go = Instantiate(pokeball, handTransform.position, handTransform.rotation);

        if (s.GrabInteractor != null &&
            go.TryGetComponent(out GrabInteractable gi))
        {
            s.GrabInteractor.ForceSelect(gi);
            Debug.Log("grabbed");
        }
        else if (s.HandGrabInteractor != null &&
                 go.TryGetComponent(out HandGrabInteractable hgi))
        {
            s.HandGrabInteractor.ForceSelect(hgi);
            Debug.Log("hand grabbed");
        }
    }
}
    // private void OnTriggerEnter(Collider other)
    // {
    //     // if ( other.CompareTag("HandOrController") ) _renderer.enabled = true;
    //     _renderer.enabled = false;
    //     
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     // if ( other.CompareTag("HandOrController") ) _renderer.enabled = false;
    //     _renderer.enabled = true;
    // }
