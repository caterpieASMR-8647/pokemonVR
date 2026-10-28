using System;
using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PokeballCollision : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private const string CaughtPokemon = "CaughtPokemon";
    
    // private void Awake()
    // {
    //     Instantiate(gameObject, Vector3.zero, Quaternion.identity);
    // }

    // void Start()
    // {
    //     gameObject.SetActive( false );
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag( "Terrain" ) ) Destroy(gameObject);
        
        if ( collision.gameObject.CompareTag( "Pokemon" ) && collision.collider.TryGetComponent( CapsuleCollider, out _ ) )
        {
            _rigidbody.linearVelocity.Inverse();
            StartCoroutine(RotateToIdentity());
            StartCoroutine(PokeballAnimation());
        }
    }

    private IEnumerator RotateToIdentity()
    {
        yield return new WaitForSeconds(0f);
        LerpRotate( Quaternion.identity );
    }
    
    private IEnumerator PokeballAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        _rigidbody.isKinematic = true;
        _animator.SetTrigger( CaughtPokemon );
        _rigidbody.freezeRotation = true;
        yield return new WaitForSeconds(2f);
        _rigidbody.isKinematic = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive( false );
    }

    private void LerpRotate( Quaternion endRotation )
    {
        do
        {
            Quaternion.Lerp( _rigidbody.rotation, endRotation, 1f * Time.deltaTime );
        } while ( _rigidbody.rotation != endRotation );
    }
}
