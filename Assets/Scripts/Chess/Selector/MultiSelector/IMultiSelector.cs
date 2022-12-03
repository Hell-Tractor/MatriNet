using System.Collections.Generic;

public interface IMutiSelector {
    List<Chess> Select<T>(ChessSet<T> set, List<Chess> allChess) where T : Chess;
}