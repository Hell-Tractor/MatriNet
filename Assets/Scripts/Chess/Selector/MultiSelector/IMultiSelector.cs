using System.Collections.Generic;

public interface IMutiSelector {
    List<Chess> Select(ChessSet set, List<Chess> allChess);
}