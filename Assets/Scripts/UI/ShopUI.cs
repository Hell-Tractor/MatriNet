using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour,IPointerClickHandler
{
    public GameObject manager;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
           return;
        }
        
        
            if (name == "shopchip")
            {
                Debug.Log("chip");
                ChessFactory.Instance.GenerateChess(ChessType.Chip).OnPick(Board.Instance);
            }
            else if (name == "shopmirror")
            {
                Debug.Log("mirror");
                ChessFactory.Instance.GenerateChess(ChessType.Mirror).OnPick(Board.Instance);
            }
            else if (name == "shopbomb")
            {
                Debug.Log("bomb");
                ChessFactory.Instance.GenerateChess(ChessType.Bomb).OnPick(Board.Instance);
            }


        



    }

}
