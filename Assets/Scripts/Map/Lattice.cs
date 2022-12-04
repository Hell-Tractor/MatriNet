using UnityEngine;

public class Lattice : MonoBehaviour {
    public Chess chess { get; private set; } = null;
    public Vector2 Size;
    public int PreGenerateWidth = 5;
    private bool _hasFog = true;

    private static Vector2[] _directions = new Vector2[] {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
        Vector2.up + Vector2.left,
        Vector2.up + Vector2.right,
        Vector2.down + Vector2.left,
        Vector2.down + Vector2.right,
        Vector2.up * 2,
        Vector2.down * 2,
        Vector2.left * 2,
        Vector2.right * 2
    };

    public void CloseFog() {
        // this.GetComponent<SpriteRenderer>().color = Color.red;
        ParticleSystem particleSystem = this.GetComponent<ParticleSystem>();
        if (particleSystem) {
            particleSystem.Stop();
            particleSystem.playbackSpeed = 5.0f;
        }
        _hasFog = false;
    }

    public bool HasChess() {
        return this.chess != null;
    }

    public bool SetChess(Chess chess) {
        if (this._hasFog) {
            Debug.LogWarning("Lattice has fog, can't set chess.");
            return false;
        }
        if (this.chess != null) {
            Debug.LogError("Lattice already has a chess.");
            return false;
        }
        this.chess = chess;
        chess.transform.parent = this.transform;
        chess.transform.localPosition = Vector2.zero;
        this.chess.OnPlace(Board.Instance);

        // Generate Lattice nearby
        Board.Instance.GenerateLattice(this.transform.position, this.PreGenerateWidth);

        // close fog nearby
        foreach (Vector2 direction in _directions) {
            Lattice lattice = Board.Instance.GetLatticeAt(new Vector2(transform.position.x + direction.x * Size.x, transform.position.y + direction.y * Size.y));
            if (lattice != null) {
                lattice.CloseFog();
            }
        }
        return true;
    }

    public Chess GetChess() {
        Chess chess = this.chess;
        this.chess = null;
        chess.OnPick(Board.Instance);
        return chess;
    }
}
