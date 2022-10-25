using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform m_rectBack;
    RectTransform m_rectJoystick;

    Transform moveTarget;
    float m_fRadius;
    float m_fSpeed = 5.0f;
    float m_fSqr = 0f;

    //Vector3 m_vecMove;

    Vector2 m_vecNormal;

    bool m_bTouch = false;


    void Start()
    {
        m_rectBack = transform.Find("JoyStickBG").GetComponent<RectTransform>();
        m_rectJoystick = transform.Find("JoyStickBG/JoyStick").GetComponent<RectTransform>();

        moveTarget = GameObject.Find("Player").transform;

        // JoyStickBG
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Update()
    {
        if (m_bTouch)
        {
            //moveTarget.position += m_vecMove;
        }

    }

    void OnTouch(Vector2 vecTouch)
    {
        Debug.Log(m_fRadius);

        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
        Debug.Log(vec);

        Vector2 vecDir = new Vector2((m_fRadius - (vecTouch.x - m_rectBack.position.x)) / m_fRadius, (m_fRadius - (vecTouch.y - m_rectBack.position.y)) / m_fRadius);
        Debug.Log(vecDir);

        InputManager.Instance.dir = vec;
        
        
        // 
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // 
        float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

        //
        Vector2 vecNormal = vec.normalized;

        //m_vecMove = new Vector3(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr, 0f, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr);
        //moveTarget.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_vecNormal = eventData.position;
        Debug.Log("m_Vecvor Normal = " + m_vecNormal);
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
        InputManager.Instance.dir = Vector2.zero;
    }
}
