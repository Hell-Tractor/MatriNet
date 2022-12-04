using System.Net.Mime;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public float distance = 1f;
    public int count;
    public Text countText;

    void Start()
    {
        countText.text = count.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        this.transform.Translate(new Vector3(0, distance, 0));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        this.transform.Translate(new Vector3(0, -distance, 0));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
           return;
        }
        if (count > 0)
        {
            count--;
            countText.text = count.ToString();
            if (name == "chip")
            {
                Debug.Log("chip");
                ChessFactory.Instance.GenerateChess(ChessType.Chip).OnPick(Board.Instance);
            }
            else if (name == "mirror")
            {
                Debug.Log("mirror");
                ChessFactory.Instance.GenerateChess(ChessType.Mirror).OnPick(Board.Instance);
            }
            else if (name == "bomb")
            {
                Debug.Log("bomb");
                ChessFactory.Instance.GenerateChess(ChessType.Bomb).OnPick(Board.Instance);
            }


        }



    }
}


