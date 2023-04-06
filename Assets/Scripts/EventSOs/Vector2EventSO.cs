using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Create Vector2 Event SO", fileName = "Vector2Event")]
public class Vector2EventSO : ScriptableObject
{
    public UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 value)
    {
        OnEventRaised?.Invoke(value);
    }
}