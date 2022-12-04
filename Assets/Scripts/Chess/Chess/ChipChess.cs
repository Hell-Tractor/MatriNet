using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChipChess : Chess {
    public bool isInChessSet = false;

    public ChipChess() {
        this.type = ChessType.Chip;
    }
    public override void OnClick(Board board, int mouse) {
        if (mouse == 1) {
            ChessSet currentSet = board.GetCurrentChessSet();
            if (currentSet == null)
                currentSet = board.CreateNewChessSet();
            if (currentSet.chesses.Count > 0 && currentSet.chesses[0].Id == this.Id) {
                if (currentSet.chesses.Count > 1) {
                    currentSet.chesses.Add(this);
                    IMultiSelector areaSelector = new AreaSelector();
                    areaSelector.Select(currentSet).ForEach(x => {
                        Lattice lattice = board.GetLatticeAt(x);
                        if (lattice != null) {
                            lattice.GetComponent<SpriteRenderer>().color = lattice.HighLightColor;
                        }
                    });
                    board.CreateNewChessSet();
                }
            } else {
                if (isInChessSet)
                    return;
                isInChessSet = true;
                currentSet.chesses.Add(this);
            }
            if (currentSet.chesses.Count > 1) {
                Chess last = currentSet.chesses[currentSet.chesses.Count - 2];

                ChessSet tempSet = new ChessSet();
                tempSet.chesses.Add(last);
                tempSet.chesses.Add(this);
                IMultiSelector lineSelector = new LineSelector();
                IEnumerable<Lattice> vectors = lineSelector.Select(tempSet).Select(pos => board.GetLatticeAt(pos));
                if (vectors.Any(lattice => lattice?.chess?.type == ChessType.Rock)) {
                    Debug.LogWarning("Razer blocked.");
                    return;
                }
                vectors.ToList().ForEach(lattice => {
                    SpriteRenderer renderer = lattice?.GetComponent<SpriteRenderer>();
                    if (renderer != null)
                        renderer.color = lattice.HighLightColor;
                });
                RazerFactory.Instance.GenerateRazer(last.transform.position, this.transform.position);
            }
        }
    }
}
