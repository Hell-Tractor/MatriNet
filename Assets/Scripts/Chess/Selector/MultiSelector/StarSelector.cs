using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarSelector : IMultiSelector {
    public List<Vector2> Select(ChessSet set) {
        LineSelector lineSelector = new LineSelector();
        List<Vector2> result = new List<Vector2>();
        if (set.chesses.Count <= 1)
            return result;
        Chess center = set.chesses[0];
        set.chesses.Skip(1).ToList().ForEach(chess => {
            ChessSet temp = new ChessSet();
            temp.chesses.Add(center);
            temp.chesses.Add(chess);
            result.AddRange(lineSelector.Select(temp));
        });
        return result;
    }
}
