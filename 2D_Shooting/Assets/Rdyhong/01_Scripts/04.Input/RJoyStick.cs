using UnityEngine;
using UnityEngine.EventSystems;

public class RJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector2 baseJSPos;

    RectTransform m_rectBack;
    RectTransform m_rectJoystick;

    float m_fRadius;

    public static bool rStick_Touch = false;

    void Start()
    {
        m_rectBack = transform.Find("JoyStickBG").GetComponent<RectTransform>();
        m_rectJoystick = transform.Find("JoyStickBG/JoyStick").GetComponent<RectTransform>();

        //Set Base Position
        baseJSPos = m_rectBack.localPosition;

        // JoyStickBG
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void OnTouch(Vector2 vecTouch)
    {
        // Clamp Stick In BG
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // Set Angle
        float angle = Mathf.Atan2(vecTouch.y - m_rectBack.position.y, vecTouch.x - m_rectBack.position.x) * Mathf.Rad2Deg;
        InputMgr.Inst.Rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_rectBack.position = eventData.position;
        //OnTouch(eventData.position);
        rStick_Touch = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        rStick_Touch = true;
    }    

    public void OnPointerUp(PointerEventData eventData)
    {
        m_rectBack.localPosition = baseJSPos;
        m_rectJoystick.localPosition = Vector2.zero;
        rStick_Touch = false;
    }
}
