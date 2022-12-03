using System.Collections.Generic;
using System.Linq;

public class RightSelector : ISingleSelector {
    public List<Chess> Select(Chess chess, List<Chess> allChess) {
        return allChess.Where(c => c.position.x > chess.position.x && c.position.y == chess.position.y).ToList();
    }
}