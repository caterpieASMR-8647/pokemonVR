using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromButton : MonoBehaviour
{
    public void LoadScene( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }
}
