using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using Dreamteck.Splines;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;

    [SerializeField] private GameObject pathBlockPrefab = null;

    private void Start()
    {
        float blockLength = pathBlockPrefab.transform.localScale.z;

        
        // for (int i = 0; i < 100; i++)
        // {
        //     var pos= new Vector3(0, -0.5f, blockLength * i);
        //     Instantiate(pathBlockPrefab, pos, Quaternion.identity);
        // }


        
        
        float splineLength =  spline.CalculateLength();
        print(splineLength);
        int blocksOnSpline = (int)(splineLength / blockLength);
        float blockSplineFraction = blockLength / splineLength;

        for (float splineProgress = 0; splineProgress < 1f; splineProgress += blockSplineFraction)
        {
            var splineSample = spline.Evaluate(splineProgress);
            Instantiate(pathBlockPrefab, splineSample.position, splineSample.rotation);
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
