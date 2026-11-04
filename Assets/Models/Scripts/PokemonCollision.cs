using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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
    private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _caughtStar;
    [SerializeField] private Material _starMaterial;
    [SerializeField] private Material _heartMaterial;
    
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _countdown = Random.Range( 0, 21 );
        _particleSystem = GetComponent<ParticleSystem>();
        if (SceneManager.GetActiveScene().name == "City")
        {
            if ( PlayerPrefs.HasKey( gameObject.name ) && PlayerPrefs.GetInt( gameObject.name ) == 1 )
            {
                _caughtStar.gameObject.SetActive(true);
            }
            _particleSystem.GetComponent<ParticleSystemRenderer>().material = _starMaterial;
        }
        else
        {
            _particleSystem.GetComponent<ParticleSystemRenderer>().material = _heartMaterial;
            
        }
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
                StartCoroutine( CaughtParticles() );
                PlayerPrefs.SetInt( gameObject.name, 1 );
                Debug.Log(PlayerPrefs.GetInt( gameObject.name ) );
                break;
            case "Berry":
                Debug.Log("Pokemon Eat");
                if ( _audioSource.isPlaying ) _audioSource.Stop();
                _audioSource.PlayOneShot( happySound );
                mat.mainTexture = happyTexture;
                StartCoroutine( WaitAndResetTexture() );
                StartCoroutine( CaughtParticles() );
                otherCollider.GetComponent<BerryReset>().ResetPosition();
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
    
    private IEnumerator CaughtParticles()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(.5f);
        _particleSystem.Stop();
        _caughtStar.gameObject.SetActive(true);
    }
}
