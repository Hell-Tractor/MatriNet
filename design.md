```puml
class Chess <<abstract>> {
    + Vector2 position
    - ISingleSelector selector

    + OnClick()
    + OnPlace()
    + OnPick()
    + OnSettle()
}

interface ISingleSelector {
    List<Chess> Select(Chess, List<Chess>)
}

interface IMultiSelector {
    List<Chess> Select(ChessSet, List<Chess>)
}

class AreaSelector implements IMultiSelector {}
class LineSelector implements IMultiSelector {}
class DirectionSelector implements ISingleSelector {}

class ChipChess extends Chess {}
class MirrorChess extends Chess {}
class RockChess extends Chess {}
class BombChess extends Chess {}

class ChessSet<? extends Chess> {
    - List<?> _chesses;
    - IMultiSelector selector;
}

class Board {
}

Chess --> ISingleSelector
ChessSet --> IMultiSelector
Chess <--  ChessSet
Board --> Chess
Board --> ChessSet
```