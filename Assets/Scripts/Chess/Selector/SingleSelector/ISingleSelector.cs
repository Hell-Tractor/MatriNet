using System.Collections.Generic;

public interface ISingleSelector {
    List<Chess> Select(Chess chess, List<Chess> allChess);
}