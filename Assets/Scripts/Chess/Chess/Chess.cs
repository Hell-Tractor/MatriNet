using UnityEngine;

public abstract class Chess : MonoBehaviour, IRoundable {
    [SerializeField, ReadOnly]
    private Vector2 _position;
    public Vector2 position { get { return _position; } }
    public bool isSettled { get; private set; }
    public ISingleSelector selector { get; private set; }


#region ChessEvents
    public virtual void OnClick(Board board) {}
    public virtual void OnPick(Board board) {}
    public virtual void OnPlace(Board board) {}
#endregion
#region IRoundable
    public void OnRoundBegin(RoundManager round) {
        isSettled = false;
    }
    public void OnRoundEnd(RoundManager round) {
        isSettled = true;
    }
#endregion
}
