using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EnemyAI {
    
    public class AbstractChess{
        public ChessType type;
        public Vector2 position;
    }
    public List<AbstractChess> EnemyChessSet = new List<AbstractChess> {};
     
    private bool _FindFogTile(List<Lattice> fogtiles, float x, float y) {
        foreach (var tile in fogtiles) {
            if (Mathf.Abs(tile.transform.position.x - x) <= 1E-5 && Mathf.Abs(tile.transform.position.y - y) <= 1E-5) {
                return tile.HasFog;
            }
        }
        return false;
    }

    public void GenerateEasyAttack(int round, List<ChessSet> sets, List<Lattice> fogTiles){
        List<AbstractChess> enemyChessSet = new List<AbstractChess> {};
        int attackCount = 0;
        int chessCount = 0;
        foreach (var set in sets) {
            chessCount += set.chesses.Count ;
        }
        chessCount = Math.Max((int) (chessCount / 4) + 1, round);
        List<Vector2> route = new List<Vector2> {};
        for (int i = 0; i < sets.Count; i++) { 
            LineSelector lineSelector = new LineSelector();
            List<Vector2> newroute =  lineSelector.Select(sets[i]);
            route.AddRange(newroute);  
        } 
        route = route.Distinct().ToList();
        for (; attackCount < chessCount; attackCount++) {
            int crosspointnum = UnityEngine.Random.Range(0, route.Count);
            int direction = UnityEngine.Random.Range(0, 4);
            int side1width = UnityEngine.Random.Range(0, 2);
            int side2width = UnityEngine.Random.Range(0, 2);
            AbstractChess enemychess1 = new AbstractChess();
            AbstractChess enemychess2 = new AbstractChess();
            float positionX = 0;
            float positionY = 0;
            switch (direction) {
                
                case 0:
                                        
                    for (float i = 3; !_FindFogTile(fogTiles, route[crosspointnum].x - i, route[crosspointnum].y + side1width) &&
                    !_FindFogTile(fogTiles, route[crosspointnum].x - i, route[crosspointnum].y - side2width); i++ ) {
                        positionX = route[crosspointnum].x - i;
                    }
                    
                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side1width);
                    enemyChessSet.Add(enemychess2);
                break;
                case 1:
                 
                    for (float i = 3; !_FindFogTile(fogTiles, route[crosspointnum].x + i, route[crosspointnum].y + side1width) &&
                    !_FindFogTile(fogTiles, route[crosspointnum].x + i, route[crosspointnum].y - side2width); i++ ) {
                        positionX = route[crosspointnum].x + i;
                    }
                    //AbstractChess enemychess1 = new AbstractChess();
                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    //AbstractChess enemychess2 = new AbstractChess();
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side1width);
                    enemyChessSet.Add(enemychess2);
                break;
                case 2:
           
                    for (float i = 3; !_FindFogTile(fogTiles, route[crosspointnum].x + side1width, route[crosspointnum].y - i) &&
                    !_FindFogTile(fogTiles, route[crosspointnum].x - side2width, route[crosspointnum].y - i); i++ ) {
                        positionY = route[crosspointnum].y - i;
                    }
                    //AbstractChess enemychess1 = new AbstractChess();
                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    //AbstractChess enemychess2 = new AbstractChess();
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side1width);
                    enemyChessSet.Add(enemychess2);
                break;
                case 3:
                   
                    for (float i = 3; !_FindFogTile(fogTiles, route[crosspointnum].x + side1width, route[crosspointnum].y + i) &&
                    !_FindFogTile(fogTiles, route[crosspointnum].x - side2width, route[crosspointnum].y + i); i++ ) {
                        positionY = route[crosspointnum].y + i;
                    }
                    //AbstractChess enemychess1 = new AbstractChess();
                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    //AbstractChess enemychess2 = new AbstractChess();
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side1width);
                    enemyChessSet.Add(enemychess2);
                break;
                default :
                break;
            }
            
        
            
        }
        EnemyChessSet.AddRange(enemyChessSet);
    }
    
}