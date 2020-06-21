using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public UnityEvent mouseHover;
    public UnityEvent ConfirmSelection;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mouseHover.Invoke();
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        ConfirmSelection.Invoke();
        Debug.Log("mouseClick");
    }
}
