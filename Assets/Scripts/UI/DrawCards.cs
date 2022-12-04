using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if(name == "chip")
        {
            Debug.Log("chip");
            
            ChessFactory.Instance.GenerateChess(ChessType.Chip).OnPick(Board.Instance);

        }
        else if(name == "mirror")
        {
            Debug.Log("mirror");
            ChessFactory.Instance.GenerateChess(ChessType.Mirror).OnPick(Board.Instance);
        }
        else if(name == "bomb")
        {
            Debug.Log("bomb");
            ChessFactory.Instance.GenerateChess(ChessType.Rock).OnPick(Board.Instance);
        }



    }
}


