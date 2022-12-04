using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject manager;
    public DrawCards chipCards;
    public DrawCards mirrorCards;
    public DrawCards bombCards;
    public Text coinNumber;

    public GameObject shopWindow;

    void Start()
    {
        coinNumber.text = manager.GetComponent<PlayerInfo>().Money.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }

        if (name == "shopchip")
        {
            Debug.Log("chip");
            if (manager.GetComponent<PlayerInfo>().Money >= 2)
            {
                manager.GetComponent<PlayerInfo>().Money -= 2;
                chipCards.count++;
                coinNumber.text = manager.GetComponent<PlayerInfo>().Money.ToString();
                chipCards.countText.text = chipCards.count.ToString();
            }

        }
        else if (name == "shopmirror")
        {
            Debug.Log("mirror");
            if (manager.GetComponent<PlayerInfo>().Money >= 3)
            {
                manager.GetComponent<PlayerInfo>().Money -= 3;
                mirrorCards.count++;
                coinNumber.text = manager.GetComponent<PlayerInfo>().Money.ToString();
                mirrorCards.countText.text = mirrorCards.count.ToString();
            }

        }
        else if (name == "shopbomb")
        {
            Debug.Log("bomb");
            if (manager.GetComponent<PlayerInfo>().Money >= 5)
            {
                manager.GetComponent<PlayerInfo>().Money -= 5;
                bombCards.count++;
                coinNumber.text = manager.GetComponent<PlayerInfo>().Money.ToString();
                bombCards.countText.text = bombCards.count.ToString();
            }

        }






    }
    public void CloseShop()
    {
        shopWindow.SetActive(false);
    }

}
