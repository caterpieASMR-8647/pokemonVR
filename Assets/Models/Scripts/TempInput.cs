using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempInput : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menuTypes;
    
    [SerializeField]
    private Camera _maincamera;

    public void ToggleSelfActive() {
        if ( gameObject.activeSelf )
        {
            gameObject.SetActive( false );
            foreach ( var menuType in menuTypes )
            {
                menuType.SetActive( false );
            }
        }
        else
        {
            foreach ( var menuType in menuTypes )
            {
                if ( menuType.name == "MainMenu" ) menuType.SetActive( true );
            }
            transform.position = _maincamera.transform.position + Vector3.Normalize( new Vector3( _maincamera.transform.forward.x, 0, _maincamera.transform.forward.z ) ) * ((float)1.5);
            transform.eulerAngles = new Vector3( 0, _maincamera.transform.eulerAngles.y, 0 );
            
            gameObject.SetActive( true );
        }
    }
}
