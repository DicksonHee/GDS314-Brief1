using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Animator screenAnimator;
    public GameObject buttonLight;
    
    private Camera _cam;
    private bool isPressed;
    
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
                if (hitInfo.collider.gameObject == gameObject)
                {
                    if (!isPressed) StartCoroutine(StartLoading_CO());
                }
            }
        }
    }

    private IEnumerator StartLoading_CO()
    {
        buttonLight.SetActive(false);
        screenAnimator.SetTrigger("Fall");
        yield return new WaitForSeconds(1f);
        SceneLoad_Manager.LoadSpecificScene(SessionManager.current.GetNextRandomScene());
    }
}
