using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float moveAmount;
    public float m_minCamXPos = -1000;
    public float m_maxCamXPos = 1000;
    public float m_minCamYPos = -1000;
    public float m_maxCamYPos = 1000;

    public float distance = 100;
    
    public float timer=5;
    public Camera myCamera;
    public Vector2 m_camFollowPos=new Vector2(0,0);
    void Update()
    {
        timer--;

        if (Input.mousePosition.y > Screen.height-distance)
        {
            m_camFollowPos.y += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y < distance)
        {
            m_camFollowPos.y -= moveAmount * Time.deltaTime;
        }

        if (Input.mousePosition.x > Screen.width - distance)//如果鼠标位置在右侧
        {
            m_camFollowPos.x += moveAmount * Time.deltaTime;//就向右移动
        }
        if (Input.mousePosition.x < distance)
        {
            m_camFollowPos.x -= moveAmount * Time.deltaTime;
        }

        m_camFollowPos.y = Mathf.Clamp(m_camFollowPos.y, m_minCamYPos, m_maxCamYPos);
        m_camFollowPos.x = Mathf.Clamp(m_camFollowPos.x, m_minCamXPos, m_maxCamXPos);
        if(timer<0)
        {
            myCamera.transform.position = new Vector3(m_camFollowPos.x, m_camFollowPos.y, myCamera.transform.position.z);
        }




    }


}
