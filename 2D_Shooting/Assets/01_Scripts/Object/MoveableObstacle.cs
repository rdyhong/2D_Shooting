using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveableObstacle : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = transform.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = 5;
            rb.angularDrag = 5;
        }
    }

    public void HitEffect(Vector3 _forceDir, Vector3 _hitPos, float _power)
    {
        Vector2 curPos = new Vector3(transform.position.x, transform.position.y);
        Vector2 dir = new Vector2(_forceDir.x, _forceDir.y);
        Vector2 hitPos = new Vector2(_hitPos.x, _hitPos.y);

        rb.AddForceAtPosition(dir.normalized * (_power * 0.5f), hitPos, ForceMode2D.Impulse);
        rb.AddForce((curPos - hitPos).normalized * (_power * 0.5f), ForceMode2D.Impulse);
    }
}


