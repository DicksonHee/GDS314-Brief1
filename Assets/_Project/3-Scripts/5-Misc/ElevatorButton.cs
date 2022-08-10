using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    private Camera _cam;
    
    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(_cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2)), out RaycastHit hitInfo, 5f))
            {
                if (hitInfo.collider.gameObject == gameObject) StartCoroutine(StartLoading_CO());
            }
        }
    }

    private IEnumerator StartLoading_CO()
    {
        yield return new WaitForSeconds(10f);
        SceneLoad_Manager.LoadSpecificScene(SessionManager.current.GetNextRandomScene());
    }
}
