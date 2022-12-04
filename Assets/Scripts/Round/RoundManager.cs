using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    private List<IRoundable> _roundables;

    public int RoundCount = 15;
    [ReadOnly]
    private int _currentRound = 0;
    public int CurrentRound { get => _currentRound; }

    public static RoundManager Instance { get; private set; }

    private void Start() {
        if (Instance != null) {
            Debug.LogError("RoundManager already exists.");
            return;
        }
        Instance = this;
    }

    public void StartNextRound() {
        _currentRound++;
        if (_currentRound > RoundCount) {
            Debug.Log("Game Over");
            return;
        }
        _roundables = Board.Instance.GetRoundables();
        _roundables.ForEach(roundable => roundable.OnRoundBegin(this));
        List<Lattice> temp = GameObject.FindGameObjectsWithTag("Lattice").Select(x => x.GetComponent<Lattice>()).ToList();
        EnemyAI NewRoundEnemy = new EnemyAI();
        NewRoundEnemy.GenerateEasyAttack(_currentRound, Board.Instance.chessSets, temp);
    }

    public void EndCurrentRound() {
        _roundables.ForEach(roundable => roundable.OnRoundEnd(this));
        AreaSelector EndRoundArea = new AreaSelector();
        
        int money = EndRoundArea.Select(Board.Instance.GetCurrentChessSet()).Count;

    }
}
