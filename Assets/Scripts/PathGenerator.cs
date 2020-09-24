using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
   
    [SerializeField] private  SplineComputer[] splines = null;
    [SerializeField] private GameObject pathBlockPrefab;
    
    public void Init()
    {
        float blockLength = 2;
        
        for (var i = 0; i < splines.Length; i++)
        {
            var spline = splines[i];

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
    }
}
