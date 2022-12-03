using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    private List<IRoundable> _roundables = new List<IRoundable>();

    public int RoundCount = 15;
    [ReadOnly]
    private int _currentRound = 0;

    public void StartNextRound() {
        _currentRound++;
        if (_currentRound > RoundCount) {
            Debug.Log("Game Over");
            return;
        }

        _roundables.ForEach(roundable => roundable.OnRoundBegin(this));
    }

    public void EndCurrentRound() {
        _roundables.ForEach(roundable => roundable.OnRoundEnd(this));
    }
}
