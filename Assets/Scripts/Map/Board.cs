using UnityEngine;

public class Board : MonoBehaviour {
    public GameObject LatticePrefab;
    public Vector2Int LatticeSize;
    public int InitSize;
    public int NonFogSize;

    public static Board Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Board already exists.");
            return;
        }
        Instance = this;

        this.GenerateLattice(Vector2.zero, this.InitSize, this.NonFogSize);
    }

    private void Update() {
        // if (Input.GetMouseButtonDown(0)) {
        //     Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Lattice lattice = this.GetLatticeAt(mousePosition);
        //     if (lattice != null)
        //         lattice.SetChess(null);
        // }
    }

    public void GenerateLattice(Vector2 position) {
        GameObject lattice = Instantiate(LatticePrefab, position, Quaternion.identity);
        lattice.transform.SetParent(transform);
    }

    public void GenerateLattice(Vector2 center, int width) {
        this.GenerateLattice(center, width, 0);
    }

    public void GenerateLattice(Vector2 center, int width, int nonFogSize) {
        Vector2 offset;
        if (width % 2 == 0)
            offset = -(new Vector2(width * LatticeSize.x, width * LatticeSize.y)) / 2;
        else
            offset = -(new Vector2((width - 1) * LatticeSize.x, (width - 1) * LatticeSize.y)) / 2;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < width; j++) {
                Vector2 position = center + new Vector2(i * LatticeSize.x, j * LatticeSize.y) + offset;
                if (this.GetLatticeAt(position) == null) {
                    this.GenerateLattice(position);
                }
                if (i >= (width - nonFogSize) / 2 && i < (width + nonFogSize) / 2 && j >= (width - nonFogSize) / 2 && j < (width + nonFogSize) / 2) {
                    this.GetLatticeAt(position).CloseFog();
                }
            }
        }
    }

    public Lattice GetLatticeAt(Vector2 position) {
        return Physics2D.Raycast(position, Vector2.zero, 1f, LayerMask.GetMask("Lattice")).collider?.GetComponent<Lattice>();
    }
}
