using UnityEngine;
using System.Collections;

public class RegionPlayer : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Vector3.up); 
    }
}
