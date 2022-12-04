using UnityEngine;

public class MouseFollower : MonoBehaviour {
    public GameObject item = null;
    public static MouseFollower Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("MouseFollower already exists.");
            return;
        }
        Instance = this;
    }
    private void Update() {
        if (item != null) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            item.transform.position = mousePosition;
        }
    }
}
