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

        
        if (round == 1) return;
        List<AbstractChess> enemyChessSet = new List<AbstractChess> {};
        int attackCount = 0;
        int chessCount = 0;

        foreach (var set in sets) {
            chessCount += set.chesses.Count ;
        }
        
        chessCount = Math.Max((int) (chessCount / 4) , round - 1);
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
            
            int side1width = UnityEngine.Random.Range(1, 2);
            int side2width = UnityEngine.Random.Range(1, 2);
            while (side1width + side2width > (4 + (int) (round / 4)) || side1width + side2width < 2) {
                side1width = UnityEngine.Random.Range(1, 2);
                side2width = UnityEngine.Random.Range(1, 2);
            }
            AbstractChess enemychess1 = new AbstractChess();
            AbstractChess enemychess2 = new AbstractChess();
            float positionX = 0;
            float positionY = 0;
            float diff = 2;
            Debug.Log("rand1:"+side1width+" rand2:"+side2width);
            switch (direction) {
                
                case 0:
                    
                    do {
                        diff++;
                        positionX = route[crosspointnum].x - diff;      
                    }
                    while (!_FindFogTile(fogTiles, route[crosspointnum].x - diff, route[crosspointnum].y + side1width) ||
                    !_FindFogTile(fogTiles, route[crosspointnum].x - diff, route[crosspointnum].y - side2width));  
                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side2width);
                    enemyChessSet.Add(enemychess2);
                break;
                case 1:
                    
                    do {
                        diff++;
                        positionX = route[crosspointnum].x + diff;      
                    }
                    while (!_FindFogTile(fogTiles, route[crosspointnum].x + diff, route[crosspointnum].y + side1width) ||
                    !_FindFogTile(fogTiles, route[crosspointnum].x + diff, route[crosspointnum].y - side2width)); 

                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(positionX, route[crosspointnum].y + side1width);
                    enemyChessSet.Add(enemychess1);
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(positionX, route[crosspointnum].y - side2width);
                    enemyChessSet.Add(enemychess2);
                break;
                case 2:
                    do {
                        diff++;
                        positionY = route[crosspointnum].y - diff;      
                    }
                    while (!_FindFogTile(fogTiles, route[crosspointnum].x + side1width, route[crosspointnum].y - diff) ||
                    !_FindFogTile(fogTiles, route[crosspointnum].x - side2width, route[crosspointnum].y - diff));

                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(route[crosspointnum].x + side1width, positionY);
                    enemyChessSet.Add(enemychess1);
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(route[crosspointnum].x - side2width, positionY);
                    enemyChessSet.Add(enemychess2);
                break;
                case 3:
                    do {
                        diff++;
                        positionY = route[crosspointnum].y + diff;      
                    }
                    while (!_FindFogTile(fogTiles, route[crosspointnum].x + side1width, route[crosspointnum].y + diff) ||
                    !_FindFogTile(fogTiles, route[crosspointnum].x - side2width, route[crosspointnum].y + diff));

                    enemychess1.type = ChessType.EnemyChip;
                    enemychess1.position = new Vector2(route[crosspointnum].x + side1width, positionY);
                    enemyChessSet.Add(enemychess1);
                    enemychess2.type = ChessType.EnemyChip;
                    enemychess2.position = new Vector2(route[crosspointnum].x - side2width, positionY);
                    enemyChessSet.Add(enemychess2);
                break;
                default :
                break;
            }
            
        
            
        }
        EnemyChessSet.AddRange(enemyChessSet);
    }
    
}