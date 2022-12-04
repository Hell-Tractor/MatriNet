using System.Linq;
using UnityEngine;

public class RazerManager : MonoBehaviour {
    public static RazerManager Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("RazerManager already exists.");
            return;
        }
        Instance = this;
    }

    public void RemoveAllRazer() {
        GameObject.FindGameObjectsWithTag("Razer").ToList().ForEach(x => Destroy(x));
        GameObject.FindGameObjectsWithTag("Lattice").ToList().ForEach(x => x.GetComponent<SpriteRenderer>().color = Color.white);
    }
}
