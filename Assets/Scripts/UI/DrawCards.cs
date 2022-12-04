using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float distance = 1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        this.transform.Translate(new Vector3(0,distance,0));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        this.transform.Translate(new Vector3(0, -distance,0));
    }

}


