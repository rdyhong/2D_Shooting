using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Camera inGameMainCam;
    static Transform camTransform;
    static Transform target;

    [SerializeField] float moveSpeed = 1f;

    bool targetSet = false;
    static bool onReciolAnimPlay = false;

    private void Start()
    {
        inGameMainCam = this.GetComponent<Camera>();
        camTransform = transform;
    }

    void Update()
    {
        if(target != null)
        {
            MoveToTarget();
            RotateToTarget();
        }
    }

    void MoveToTarget()
    {
        if (onReciolAnimPlay)
        {
            transform.position = Vector3.Slerp(transform.position,
                    new Vector3(
                        target.position.x,
                        target.position.y,
                        transform.position.z),
                    moveSpeed * TimeMgr.ObjDeltaTime);

            if (Vector3.Magnitude(new Vector3(transform.position.x, transform.position.y, 0) - 
                new Vector3(target.position.x, target.position.y, 0)) < 0.1f)
                onReciolAnimPlay = false;
        }
        else
        {
            transform.position =
                    new Vector3(
                        target.position.x,
                        target.position.y,
                        transform.position.z);
        }
    }

    void RotateToTarget()
    {
        transform.rotation = target.rotation;
    }

    public static void SetCameraTarget(Transform _target, Transform _parent = null)
    {
        if(_target == null)
        {
            DebugMgr.Log("Camera target is null");
            return;
        }

        target = _target;
        inGameMainCam.transform.parent = _parent;
    }

    public static void CamShotEffect(float _recoilPower)
    {
        float recoilRange = _recoilPower * 0.25f;
        Vector3 targetPos = target.transform.position + target.transform.up * -_recoilPower;
        targetPos.z = camTransform.position.z;
        targetPos.x += Random.Range(-recoilRange, recoilRange);
        targetPos.y += Random.Range(-recoilRange, recoilRange);

        camTransform.position = targetPos;

        onReciolAnimPlay = true;
    }
}
