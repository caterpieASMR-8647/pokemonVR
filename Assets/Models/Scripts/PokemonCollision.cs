using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PokemonCollision : MonoBehaviour
{
    [SerializeField]
    Material mat;
    [SerializeField]
    Texture happyTexture;
    [SerializeField]
    Texture baseTexture;
    [SerializeField]
    Texture caughtTexture;
    
    [SerializeField]
    AudioClip caughtSound;
    [SerializeField]
    AudioClip happySound;
    [SerializeField]
    AudioClip idleSound;

    private AudioSource _audioSource;
    private float _countdown;
    
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _countdown = Random.Range( 0, 21 );
    }

    private void OnTriggerEnter( Collider otherCollider )
    {
        switch (otherCollider.tag)
        {
            case "Pokeball":
                Debug.Log("Pokemon Caught");
                if ( _audioSource.isPlaying ) _audioSource.Stop();
                _audioSource.PlayOneShot( caughtSound );
                mat.mainTexture = caughtTexture;
                StartCoroutine( WaitAndResetTexture() );
                break;
            case "Berry":
                Debug.Log("Pokemon Eat");
                if ( _audioSource.isPlaying ) _audioSource.Stop();
                _audioSource.PlayOneShot( happySound );
                mat.mainTexture = happyTexture;
                StartCoroutine( WaitAndResetTexture() );
                break;
        }
    }

    private void FixedUpdate()
    {
        if ( !_audioSource.isPlaying )
        {
            if (_countdown <= 0)
            {
                _audioSource.PlayOneShot( idleSound );
                _countdown = Random.Range( 0, 21 );
            }
            else
            {
                _countdown -= Time.deltaTime;
            }
        }
    }

    private IEnumerator WaitAndResetTexture()
    {
        yield return new WaitForSeconds(2);
        mat.mainTexture = baseTexture;
    }
}
