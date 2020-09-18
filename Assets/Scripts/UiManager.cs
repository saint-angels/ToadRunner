using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Level progression")]
    [SerializeField] private TMPro.TextMeshProUGUI currentLevelLabel = null;
    [SerializeField] private TMPro.TextMeshProUGUI nextLevelLabel = null;
    [SerializeField] private UnityEngine.UI.Image levelProgressImage = null;
    

    //Called from Root
    public void Init(GameController gameController)
    {
        
        startBlockCount = FindObjectsOfType<PathBlock>().Length;
    }


    private int startBlockCount;
    
    private void Update()
    {
        //Getting blocks for fake progress counting
        //TODO: THROW AWAY
        int newBlockCount = FindObjectsOfType<PathBlock>().Length;
        float LevelProgress = 1f - ((float) newBlockCount / (float) startBlockCount) ;
        OnLevelProgress(LevelProgress * 5f);


    }


    private void OnLevelProgress(float progress)
    {
        levelProgressImage.fillAmount = progress;
        print(progress);
        // float duration = Mathf.Approximately(0f, progress) ? 0 : .2f;
        // levelProgressImage.DOFillAmount(progress, duration).SetEase(Ease.OutCubic);
    }
    
}
