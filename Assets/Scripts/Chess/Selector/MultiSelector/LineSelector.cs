using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSelector : IMultiSelector {
    public List<Vector2> Select(ChessSet set) {
        List<Vector2> result = new List<Vector2>();
        if (set.chesses.Count == 0) return result;
        for (int i = 0; i < set.chesses.Count - 1; ++i) {
            result.AddRange(
                Physics2D.LinecastAll(set.chesses[i].position, set.chesses[i + 1].position, LayerMask.GetMask("Lattice"))
                .Select(hit => new Vector2(hit.transform.position.x, hit.transform.position.y))
            );
        }
        return result.Distinct().ToList();
    }
}