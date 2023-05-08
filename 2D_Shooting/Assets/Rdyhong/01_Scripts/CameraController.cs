using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Camera inGameMainCam;
    static Transform target;

    private void Start()
    {
        inGameMainCam = this.GetComponent<Camera>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3( target.transform.position.x, target.transform.position.y, transform.position.z), 0.03f) ;
        }
    }

    public static void SetCameraTarget(Transform _target, Transform _parent = null)
    {
        if(_target == null)
        {
            _target = GameObject.Find("Player").transform;
            Debug.Log("Camera target is null");
        }

        target = _target;
        inGameMainCam.transform.parent = _parent;
    }

    public static void CamShotEffect(float _recoilPower)
    {
        float recoilRange = _recoilPower * 0.25f;
        Vector3 targetPos = target.transform.position + target.transform.up * -_recoilPower;
        targetPos.z = -10;
        targetPos.x += Random.Range(-recoilRange, recoilRange);
        targetPos.y += Random.Range(-recoilRange, recoilRange);

        inGameMainCam.transform.position = targetPos;
    }
}
