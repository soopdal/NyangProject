using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private Transform target;
    private bool      isRun;
    private float     halfDistance;
    private float     speed;
    private const float InitSpeed = 1.0f;
    private const float AddSpeed = 1.0f;
    private const float CameraSpare = 0.1f;

    void Start()
    {
        speed = InitSpeed;
    }

    void LateUpdate ()                                                                              // 카메라는 떨리지 않도록 오브젝트 이동이 끝난 뒤에 계산하므로 여기서 한다. 
    {
        if (!isRun) return;

        var cam = Camera.main.transform;
        var distance = Vector3.Distance(cam.position, target.position);

        if (distance <= halfDistance)                                                               // Lerp으로 다가갈 때 target이 지구의 움직임으로 가까이 못가는 현상을 방지하기 위해, Leap 속도를 점점 높인다.
            speed += AddSpeed * Time.deltaTime;

        cam.position = Vector3.Lerp(cam.position, target.position, speed * Time.deltaTime);
        distance = Vector3.Distance(cam.position, target.position);                                 // Debug.Log(distance + " " + speed + " " + target.position + " " + cam.position + "\n");

        if (distance > Camera.main.nearClipPlane + CameraSpare) return;

        isRun = false;
        speed = InitSpeed;
        StartCoroutine(LoadIRegionScene());                     
    }

    public void RunToRegion(Collider collision)
    {
        target = collision.gameObject.transform;

        halfDistance = Vector3.Distance(Camera.main.transform.position, target.position) * 0.5f;
        isRun = true;
    }

    private static IEnumerator LoadIRegionScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("main");                                        // The Application loads the Scene in the background at the same time as the current Scene. //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        
        while (!asyncLoad.isDone)                                                                   //Wait until the last operation fully loads to return anything
        {
            yield return null;
        }
    }
}
