using System.Collections;
using Oculus.Interaction;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

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
    
    private void OnTriggerEnter( Collider otherCollider )
    {
        switch (otherCollider.tag)
        {
            case "Pokeball":
                Debug.Log("Pokemon Caught");
                mat.mainTexture = caughtTexture;
                StartCoroutine( WaitAndResetTexture() );
                break;
            case "Berry":
                Debug.Log("Pokemon Eat");
                mat.mainTexture = happyTexture;
                StartCoroutine( WaitAndResetTexture() );
                break;
        }
    }

    private IEnumerator WaitAndResetTexture()
    {
        yield return new WaitForSeconds(2);
        mat.mainTexture = baseTexture;
    }
}
