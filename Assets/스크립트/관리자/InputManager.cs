using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private bool isRegionCheck = true;
    private CameraManager cameraManager;

    protected void Awake()
    {
        cameraManager = GetComponent<CameraManager>();
    }

    protected void Update()                                                           // Update is called once per frame
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckRegion();
        }
    }

    private void CheckRegion()
    {
        if (!isRegionCheck)                                                 // is false 
            return;

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit)) return;
        if (hit.collider == null) return;
        if (!cameraManager) return;
        if (hit.collider.gameObject.GetComponent<RegionPlayer>() == null) return;

        isRegionCheck = false;
        cameraManager.RunToRegion(hit.collider);
    }
}
