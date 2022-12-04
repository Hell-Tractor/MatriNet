using UnityEngine;

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
}