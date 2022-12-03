using System.Collections.Generic;

public class ChessSet : IRoundable {
    public List<Chess> chesses = new List<Chess>();
    public IMultiSelector selector { get; private set; }

    public void OnRoundBegin(RoundManager round) {}

    public void OnRoundEnd(RoundManager round) {
        // TODO: Implement this method
    }
}
