using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Control Player Aim
/// </summary>
public class AimPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // When Aimpad Using -> True
    public static bool onTouch = false;
    public static float xMoveAmount;
    
    [SerializeField] private float m_sensitive = 1;

    public float sensitive
    {
        get
        {
            if (m_sensitive < 0.01f)
                return 0.1f;
            else if (m_sensitive > 30f)
                return 30f;
            else
                return m_sensitive;
        }
    }

    private float beforX = 0;
    private float curX = 0;

    private Vector2 enterVec = Vector2.zero;
    private float touchEnterInterval = 0;

    private void Start()
    {
        PadInit();
    }

    void PadInit()
    {
        StartCoroutine(PadCycle());
    }

    IEnumerator PadCycle()
    {
        while (true)
        {
            if(touchEnterInterval < 10)
                touchEnterInterval += Time.deltaTime;

            yield return null;
        }
    }


    // Update on touch
    void OnTouch(Vector2 _touchVec)
    {
        XValueUpdate(_touchVec.x);
    }

    void XValueUpdate(float _x)
    {
        curX = _x;

        xMoveAmount = (beforX - curX) * sensitive;

        // Player Rotation Set
        InGameMgr.myCharacter.SetRotate(xMoveAmount);

        beforX = curX;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        xMoveAmount = 0;

        if(touchEnterInterval < 0.3f)
            InputMgr.isFire = true;
        else
            touchEnterInterval = 0;

        enterVec = eventData.position;

        // X start value
        beforX = eventData.position.x;

        onTouch = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onTouch = false;
        InputMgr.isFire = false;
    }
}
