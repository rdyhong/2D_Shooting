using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public static Camera cam;
    private Transform target;


    private void Start()
    {
        cam = this.GetComponent<Camera>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3( target.transform.position.x, target.transform.position.y, transform.position.z), 0.03f) ;
        }
    }

    public void SetCameraTarget(Transform _target, Transform _parent = null)
    {
        if(_target == null) target = GameObject.Find("Player").transform;
        target = _target;
        transform.parent = _parent;
    }    
}
