using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    void Update()
    {
        // listen enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // load scene
            SceneManager.LoadScene("GameScene");
        }

    }

}
