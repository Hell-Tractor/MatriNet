using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public GameObject LatticePrefab;
    public Vector2Int LatticeSize;
    public int InitSize;
    public int NonFogSize;
    public List<Chess> chesses = new List<Chess>();
    public List<ChessSet> chessSets = new List<ChessSet>();

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
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Lattice lattice = this.GetLatticeAt(mousePosition);
            if (lattice != null) {
                Debug.Log(lattice.HasChess());
                if (lattice.HasChess() && MouseFollower.Instance.item == null) {
                    if (lattice.chess.type == ChessType.Chip) {
                        RazerManager.Instance.RemoveAllRazer();
                        this.chessSets.Clear();
                        this.chesses.ForEach(chess => {
                            if (chess.type == ChessType.Chip)
                                (chess as ChipChess).isInChessSet = false;
                        });
                    }
                    lattice.chess.OnClick(this, 0);
                    lattice.chess.OnPick(this);
                    lattice.GetChess();
                } else if (!lattice.HasChess() && MouseFollower.Instance.item != null) {
                    Chess chess = MouseFollower.Instance.item.GetComponent<Chess>();
                    if (lattice.SetChess(chess))
                        chesses.Add(chess);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Lattice lattice = this.GetLatticeAt(mousePosition);
            if (lattice != null) {
                if (lattice.HasChess()) {
                    lattice.chess.OnClick(this, 1);
                }
            }
        }
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

    public ChessSet CreateNewChessSet() {
        ChessSet chessSet = new ChessSet();
        this.chessSets.Add(chessSet);
        return chessSet;
    }

    public ChessSet GetCurrentChessSet() {
        if (this.chessSets.Count == 0)
            return null;
        return this.chessSets[this.chessSets.Count - 1];
    }

    public List<IRoundable> GetRoundables() {
        List<IRoundable> result = new List<IRoundable>();
        result.AddRange(this.chesses);
        result.AddRange(this.chessSets);
        return result;
    }
}
