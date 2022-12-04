using UnityEngine;

public class RazerFactory : MonoBehaviour {
    public GameObject RazerPrefab;

    public static RazerFactory Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("RazerFactory already exists.");
            return;
        }
        Instance = this;
    }

    public void GenerateRazer(Vector2 from, Vector2 to) {
        float length = Vector2.Distance(from, to);
        Vector2 center = (from + to) / 2;
        GameObject razer = Instantiate(RazerPrefab, center, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, to - from)));
        razer.GetComponent<SpriteRenderer>().size = new Vector2(length,
            razer.GetComponent<SpriteRenderer>().size.y
        );
    }
}
