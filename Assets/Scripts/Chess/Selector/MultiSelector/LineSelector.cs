using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSelector : IMultiSelector {
    //float halfSqrt2 = Mathf.Sqrt(2) / 2;
    private float _DistofPtoL(Vector2 lineStart, Vector2 lineEnd, Vector2 point) {
        if (lineStart.x == lineEnd.x) {
            return Mathf.Abs(point.y-lineStart.y);
        }else if (lineStart.y == lineEnd.y) {
            return Mathf.Abs(point.x-lineStart.x);
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
        for (int i = 0; i <= set.chesses.Count(); i++) { 
            if (set.chesses[i].position.x < LeftX) { LeftX=set.chesses[i].position.x;}
            if (set.chesses[i].position.x > RightX) { RightX=set.chesses[i].position.x;}
            if (set.chesses[i].position.y > UpperY) { UpperY=set.chesses[i].position.y;}
            if (set.chesses[i].position.y < DownY) { DownY=set.chesses[i].position.y;}

        }
        for (int axisX = (int) (LeftX + 1E-7); axisX <= (int) (RightX + 1E-7); axisX++) {
            for (int axisY = (int) (DownY + 1E-7); axisY <= (int) (UpperY + 1E-7); axisY++) {
                Vector2 targetPoint=new Vector2(axisX, axisY);
                for (int i = 0, j = set.chesses.Count() - 1; i <= set.chesses.Count(); j=i, i++) {
                    if (_DistofPtoL(set.chesses[j].position, set.chesses[i].position, targetPoint) < 0.5) {
                        allpoint.Add(targetPoint);
                    }
                }  
            }
        }
        return allpoint;
    }
}
        