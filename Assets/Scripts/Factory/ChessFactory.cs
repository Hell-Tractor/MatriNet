using UnityEngine;

public class ChessFactory : MonoBehaviour {
    public GameObject ChipChessPrefab;
    public GameObject MirrorChessPrefab;
    public GameObject RockChessPrefab;
    public GameObject BombChessPrefab;

    public GameObject EnemyChipChessPrefab;
    public static ChessFactory Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("ChessFactory already exists.");
            return;
        }
        Instance = this;
    }

    public Chess GenerateChess(ChessType type) {
        GameObject chessPrefab = null;
        switch (type) {
            case ChessType.Chip:
                chessPrefab = ChipChessPrefab;
                break;
            case ChessType.Mirror:
                chessPrefab = MirrorChessPrefab;
                break;
            case ChessType.Rock:
                chessPrefab = RockChessPrefab;
                break;
            case ChessType.Bomb:
                chessPrefab = BombChessPrefab;
                break;
            case ChessType.EnemyChip:
                chessPrefab = EnemyChipChessPrefab;
                break;
        }
        GameObject chess = Instantiate(chessPrefab);
        Chess tmp = chess.GetComponent<Chess>();
        if (type == ChessType.EnemyChip) 
            tmp.IsEnemy = true;
        return chess.GetComponent<Chess>();
    }
}
