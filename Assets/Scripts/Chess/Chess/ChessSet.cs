using System.Collections.Generic;

public class ChessSet : IRoundable {
    private List<Chess> _chesses = new List<Chess>();
    public IMutiSelector selector { get; private set; }

    public void OnRoundBegin(RoundManager round) {}

    public void OnRoundEnd(RoundManager round) {
        // TODO: Implement this method
    }
}
