using UnityEngine;

public class GameOverManager : MonoBehaviour {
    public void OnEnable() {
        Time.timeScale = 0;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
