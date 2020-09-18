using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BezierSolution;
using Dreamteck.Splines;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    // [SerializeField] private SplineComputer spline;
    
    [System.Serializable]
    public struct PathBlockSetting
    {
        public SplineComputer spline;
        public GameObject pathBlock;
    }
    
    [SerializeField] private  PathBlockSetting[] pathSettings = null;

    public void Init()
    {
        float blockLength = 2;
        
        for (var i = 0; i < pathSettings.Length; i++)
        {
            var spline = pathSettings[i].spline;
            var pathBlockPrefab = pathSettings[i].pathBlock;
            
            float splineLength = spline.CalculateLength();
            // print(splineLength);
            int blocksOnSpline = (int) (splineLength / blockLength);
            float blockSplineFraction = blockLength / splineLength;

            for (float splineProgress = 0; splineProgress < 1f; splineProgress += blockSplineFraction)
            {
                var splineSample = spline.Evaluate(splineProgress);
                Instantiate(pathBlockPrefab, splineSample.position, splineSample.rotation);
            }
        }


        // double previousPointPercent = 0;
        // for (int pointIndex = 1; pointIndex < spline.pointCount; pointIndex++)
        // {
        //     double pointPercent = spline.GetPointPercent(pointIndex);
        //     
        //     float splineLength =  spline.CalculateLength(previousPointPercent, pointPercent);
        //     
        //     float blockSplineFraction = blockLength / splineLength;
        //
        //     for (float splineProgress = 0; splineProgress < 1f; splineProgress += blockSplineFraction)
        //     {
        //         var splineSample = spline.Evaluate(splineProgress);
        //         Instantiate(pathBlockPrefab, splineSample.position, splineSample.rotation);
        //     }
        //
        //     previousPointPercent = pointPercent;
        // }

        
    }
}
