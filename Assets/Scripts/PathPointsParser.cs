using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointsParser
{
    public Transform[] Parse(Transform input, out int pointsCount)
    {
        pointsCount = input.childCount;
        Transform[] points = new Transform[pointsCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = input.GetChild(i);          
        }

        return points;
    }
}
