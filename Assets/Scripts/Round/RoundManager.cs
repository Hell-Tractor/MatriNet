using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    private List<IRoundable> _roundables;

    public int RoundCount = 15;
    // [ReadOnly]
    private int _currentRound = 0;
    public int CurrentRound { get => _currentRound; }
    public GameObject Shop;
    public GameObject GameOverUI;
    public Text RoundText;

    public static RoundManager Instance { get; private set; }

    private void Start() {
        if (Instance != null) {
            Debug.LogError("RoundManager already exists.");
            return;
        }
        Instance = this;
        StartNextRound();
    }

    public void StartNextRound() {
        _currentRound++;
        if (_currentRound > RoundCount) {
            Debug.Log("Game Over");
            GameOverUI.SetActive(true);
            return;
        }
        RoundText.text = "Round " + _currentRound + " / " + RoundCount;
        _roundables = Board.Instance.GetRoundables();
        _roundables.ForEach(roundable => roundable.OnRoundBegin(this));
        List<Lattice> temp = GameObject.FindGameObjectsWithTag("Lattice").Select(x => x.GetComponent<Lattice>()).ToList();
        EnemyAI NewRoundEnemy = new EnemyAI();
        NewRoundEnemy.GenerateEasyAttack(_currentRound, Board.Instance.chessSets, temp);
        foreach (var chess in NewRoundEnemy.EnemyChessSet) {
            var tmpchess = ChessFactory.Instance.GenerateChess(chess.type);
            tmpchess.transform.position = chess.position;
        }
        RazerManager.Instance.RemoveAllRazer();
    }

    public void EndCurrentRound() {
        _roundables = Board.Instance.GetRoundables();
        List<IRoundable> mirros = _roundables.Where(r => (r as MirrorChess) != null).ToList();
        List<IRoundable> enemy = _roundables.Where(r => (r as Chess) != null && (r as Chess).IsEnemy).ToList();
        mirros.ForEach(roundable => roundable.OnRoundEnd(this));
        enemy.ForEach(roundable => roundable.OnRoundEnd(this));
        _roundables.ForEach(roundable => roundable.OnRoundEnd(this));
        AreaSelector EndRoundArea = new AreaSelector();
        PlayerInfo.Instance.Money += Board.Instance.chessSets.Select(chessset => EndRoundArea.Select(chessset).Count).Sum();
        Debug.Log(PlayerInfo.Instance.Money);
        Board.Instance.chesses = Board.Instance.chesses.Where(chess => !chess.IsEnemy).ToList();
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => Destroy(x));
        Shop?.SetActive(true);
    }
}
