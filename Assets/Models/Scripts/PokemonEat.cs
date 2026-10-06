using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonEat : MonoBehaviour
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
