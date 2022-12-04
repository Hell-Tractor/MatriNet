using System.Linq;

public class BombChess : Chess {
    public BombChess() {
        this.type = ChessType.Bomb;
    }

    public override void OnRoundEnd(RoundManager round) {
        if (!this.isSettled) {
            base.OnRoundEnd(round);

            ChessSet tempSet = new ChessSet();
            tempSet.chesses.Add(this);
            tempSet.chesses.AddRange(Board.Instance.chesses.Where(chess => chess.type == ChessType.Chip));
            StarSelector selector = new StarSelector();
            selector.Select(tempSet).ForEach(pos => {
                Lattice lattice = Board.Instance.GetLatticeAt(pos);
                if (lattice?.chess?.type == ChessType.Rock) {
                    Chess rock = lattice.GetChess();
                    Destroy(rock.gameObject);
                }
            });

            Lattice lattice = Board.Instance.GetLatticeAt(this.position);
            lattice.GetChess();
            Destroy(this.gameObject);
        }
    }
}
