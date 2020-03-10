using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : JoyStick
{
    public float MoveThreshold
    {
        get
        {
            return MoveThreshold;
        }
        set
        {
            MoveThreshold = Mathf.Abs(value);
        }
        
    }
    [SerializeField]
    private float moveThreshold = 1;
    [SerializeField]
    private JoyStickType joyStickType = JoyStickType.Fixed;
    private Vector2 fixedPosition = Vector2.zero;

    public void SetMode(JoyStickType joyStickType)
    {
        this.joyStickType = joyStickType;
        if (joyStickType == JoyStickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
        {
            background.gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joyStickType);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);

        if (joyStickType != JoyStickType.Fixed)
        {

            background.anchoredPosition = ScreenPointToAnchorPosition(eventData.position);
        }

        base.OnPointerDown(eventData);

    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (joyStickType != JoyStickType.Fixed)
        {
            background.gameObject.SetActive(false);
        }
        base.OnPointerUp(eventData);
    }
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joyStickType == JoyStickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}

public enum JoyStickType
{
    Fixed, Floating, Dynamic
}
