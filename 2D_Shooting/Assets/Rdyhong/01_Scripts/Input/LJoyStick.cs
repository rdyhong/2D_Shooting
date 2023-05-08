using UnityEngine;
using UnityEngine.EventSystems;

public class LJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector2 baseJSPos;

    RectTransform m_rectBack;
    RectTransform m_rectJoystick;

    float m_fRadius;
    float weight = 0;

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
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);
        vec = Vector2.ClampMagnitude(vec, m_fRadius);

        // Do 0 ~ 1
        float _weight = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);
        _weight = _weight < 0.02f ? 0 : _weight;
        _weight = _weight > 0.98f ? 1 : _weight;

        weight = InputManager.isRun ? Mathf.Lerp(weight, 1, 0.2f) : Mathf.Lerp(weight, _weight, 0.2f);

        InputManager.weight = weight;
        InputManager.Instance.Dir = vec;

        m_rectJoystick.localPosition = vec;

        // Set angle if no input in RightStick
        if (!RJoyStick.rStick_Touch)
        {
            float angle = Mathf.Atan2(vecTouch.y - m_rectBack.position.y, vecTouch.x - m_rectBack.position.x) * Mathf.Rad2Deg;
            InputManager.Instance.Rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_rectBack.position = eventData.position;
        //OnTouch(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_rectBack.localPosition = baseJSPos;
        m_rectJoystick.localPosition = Vector2.zero;
        InputManager.Instance.Dir = Vector2.zero;
    }
}
