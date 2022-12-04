using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MirrorChess : Chess {
    protected static readonly Vector2[] directions = new Vector2[] {
        Vector2.right,
        Vector2.down,
        Vector2.left,
        Vector2.up
    };

    protected int _direction = 3;
    public Vector2 Direction {
        get {
            return directions[_direction];
        }
    }

    public MirrorChess() {
        this.type = ChessType.Mirror;
    }

    public override void OnClick(Board board, int mouse) {
        if (mouse == 1) {
            this._direction = (this._direction + 1) % directions.Length;
            this.transform.rotation = Quaternion.Euler(0, 0, -this._direction * 90);
        }
    }

    public override void OnRoundEnd(RoundManager round) {
        if (isSettled == false) {
            base.OnRoundEnd(round);

            ISingleSelector selector;
            if (this.Direction == Vector2.right)
                selector = new LeftSelector();
            else if (this.Direction == Vector2.down)
                selector = new UpSelector();
            else if (this.Direction == Vector2.left)
                selector = new RightSelector();
            else
                selector = new DownSelector();

            IEnumerable<Chess> chessList = selector.Select(this, Board.Instance.chesses)
                            .Where(chess => chess?.type == ChessType.Chip);
            if (!chessList.Any()) {
                Debug.LogWarning("No chip chesses found.");
                return;
            }
            Chess origin = chessList.Aggregate((a, b) => Vector2.Distance(a.position, this.position) < Vector2.Distance(b.position, this.position) ? a : b);

            Vector2 position = this.position - origin.position + this.position;
            Lattice lattice = Board.Instance.GetLatticeAt(position);
            if (lattice == null) {
                Debug.LogWarning("MirrorChess: Lattice not found");
                return;
            }
            if (lattice.HasChess()) {
                Debug.LogWarning("MirrorChess: Lattice has chess");
                return;
            }
            lattice.SetChess(ChessFactory.Instance.GenerateChess(ChessType.Rock), true);
        }
    }
}