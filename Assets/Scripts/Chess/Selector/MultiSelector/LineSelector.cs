using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSelector : IMultiSelector {
    //float halfSqrt2 = Mathf.Sqrt(2) / 2;
    private float _DistofPtoL(Vector2 lineStart, Vector2 lineEnd, Vector2 point) {
        if (lineStart.x == lineEnd.x) {
            return Mathf.Abs(point.y - lineStart.y);
        }else if (lineStart.y == lineEnd.y) {
            return Mathf.Abs(point.x - lineStart.x);
        }else {
            float A = (lineEnd.y - lineStart.y);
            float B = (lineStart.x - lineEnd.x);
            float C = (lineEnd.x - lineStart.x) * lineStart.y - (lineEnd.y - lineStart.y) * lineEnd.x ;
            return ((A * point.x + B * point.y + C) * (A * point.x + B * point.y + C) / (A * A + B * B));
        }
        
    }
    public List<Vector2> Select(ChessSet set) {
        
        List<Vector2> allpoint = new List<Vector2> {};
        
        float UpperY = set.chesses[0].position.y, DownY = set.chesses[0].position.y, LeftX = set.chesses[0].position.x, RightX = set.chesses[0].position.x;
        for (int i = 0; i < set.chesses.Count(); i++) { 
            if (set.chesses[i].position.x < LeftX) { LeftX = set.chesses[i].position.x;}
            if (set.chesses[i].position.x > RightX) { RightX = set.chesses[i].position.x;}
            if (set.chesses[i].position.y > UpperY) { UpperY = set.chesses[i].position.y;}
            if (set.chesses[i].position.y < DownY) { DownY = set.chesses[i].position.y;}
        }
        for (int axisX = (int) (LeftX + 1E-7); axisX <= (int) (RightX + 1E-7); axisX++) {
            for (int axisY = (int) (DownY + 1E-7); axisY <= (int) (UpperY + 1E-7); axisY++) {
                Vector2 targetPoint = new Vector2(axisX, axisY);
                for (int i = 0; i < set.chesses.Count() - 1; i++) {
                    if (_DistofPtoL(set.chesses[i].position, set.chesses[i + 1].position, targetPoint) < 0.5) {
                        if (!allpoint.Find(targetPoint)) {
                            allpoint.Add(targetPoint);
                        }
                        
                    }
                }
            }
        }
        for (int i = 0; i < set.chesses.Count() - 1; i++) {
            if (set.chesses[i].position.x - set.chesses[i + 1].position.x <= 1E-7) {
                float minnum = int.MinValue(set.chesses[i].position.y, set.chesses[i + 1].position.y);
                float maxnum = int.MaxValue(set.chesses[i].position.y, set.chesses[i + 1].position.y); 
                for (float j = 0; j <= maxnum - minnum + 1E-7; j++) {
                    Vector2 targetPoint=new Vector2(set.chesses[i].position.x, minnum + j);
                    if (!allpoint.Find(targetPoint)) {
                        allpoint.Add(targetPoint);
                    }
                }
                    
            }else if (set.chesses[i].position.y - set.chesses[i + 1].position.y <= 1E-7) {
                float minnum = int.MinValue(set.chesses[i].position.x, set.chesses[i + 1].position.x);
                float maxnum = int.MaxValue(set.chesses[i].position.x, set.chesses[i + 1].position.x); 
                for (float j = 0; j <= maxnum - minnum + 1E-7; j++) {
                    Vector2 targetPoint=new Vector2(minnum + j, set.chesses[i].position.y);
                    if (!allpoint.Find(targetPoint)) {
                        allpoint.Add(targetPoint);
                    }
                }
                    
            }

        } 

        return allpoint;
    }
}
        