using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

public class KAKOGO_HUJA : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
    }
}