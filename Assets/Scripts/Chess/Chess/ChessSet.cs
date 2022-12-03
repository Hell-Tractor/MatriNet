using System.Collections.Generic;

public class ChessSet<T> : IRoundable where T : Chess {
    private List<T> _chesses = new List<T>();
    public IMutiSelector selector { get; private set; }

    public void OnRoundBegin(RoundManager round) {}

    public void OnRoundEnd(RoundManager round) {
        // TODO: Implement this method
    }
}
