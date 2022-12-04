using UnityEngine;

public abstract class Chess : MonoBehaviour, IRoundable {
    public Vector2 position { get { return transform.position; } }
    public bool isSettled { get; private set; }
    public ISingleSelector selector { get; private set; }
    public int Id { get; private set; }
    public ChessType type { get; protected set; }
    private static int _count = 0;

    public Chess() {
        this.Id = _count++;
    }


#region ChessEvents
    public virtual void OnClick(Board board, int mouse) {}
    public virtual void OnPick(Board board) {}
    public virtual void OnPlace(Board board) {}
#endregion
#region IRoundable
    public virtual void OnRoundBegin(RoundManager round) {
        isSettled = false;
    }
    public virtual void OnRoundEnd(RoundManager round) {
        isSettled = true;
    }
#endregion
}
