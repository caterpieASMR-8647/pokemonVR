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
            transform.position = _maincamera.transform.position + _maincamera.transform.forward * (float)1.5;
            transform.eulerAngles = _maincamera.transform.forward.Inverse();
            
            gameObject.SetActive( true );
        }
    }
}
