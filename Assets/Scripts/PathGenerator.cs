using System;
using System.Collections;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private BezierSpline pathSpine = null;

    [SerializeField] private GameObject pathBlockPrefab = null;

    private void Start()
    {
        float blockSize = pathBlockPrefab.transform.localScale.z;

        
        for (int i = 0; i < 100; i++)
        {
            var pos= new Vector3(0, -0.5f, blockSize * i);
            Instantiate(pathBlockPrefab, pos, Quaternion.identity);
        }
        
    }
}
