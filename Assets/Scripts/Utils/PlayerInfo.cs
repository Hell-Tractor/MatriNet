using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    public static PlayerInfo Instance { get; private set; } = null;
    public int Money;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlayerInfo already exists.");
            return;
        }
        Instance = this;
    }
}
