using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Random = System.Random;

public class UiManager : MonoBehaviour
{
    [Header("Level progression")]
    [SerializeField] private TMPro.TextMeshProUGUI currentLevelLabel = null;
    [SerializeField] private TMPro.TextMeshProUGUI nextLevelLabel = null;
    [SerializeField] private UnityEngine.UI.Image levelProgressImage = null;
    
    [SerializeField] private TMPro.TextMeshProUGUI comboLabel = null;

    [Header("Combo shake")]
    public float shakeStrength = 20f;
    public int shakeVibrato = 60;    
    public float shakeRandomness = 90f;
    public Gradient comboLabelGradient;

    public float comboCooldown = 1f;

    private float currentComboCooldown;

    //Called from Root
    public void Init(GameController gameController)
    {
        
        startBlockCount = FindObjectsOfType<PathBlock>().Length;
        
        PlayerController.Instance.OnNewBlockTouched += OnNewBlockTouched;

        currentComboCooldown = comboCooldown;
    }


    private int comboCounter;
    private void OnNewBlockTouched()
    {
        currentComboCooldown = comboCooldown;
        
        comboLabel.transform.DOKill(true);
        comboLabel.DOKill(false); //Kill the previous color tween
        // comboLabel.transform.localScale = Vector3.one;
        comboCounter++;
        comboLabel.text = $"COMBO {comboCounter}";

        // comboLabel.transform.DOPunchScale(Vector3.one * .2f, .2f, 1, .1f);
        
        comboLabel.transform.DOShakePosition(.2f, shakeStrength, shakeVibrato, shakeRandomness, false, false);
        comboLabel.DOColor(comboLabelGradient.Evaluate(comboCounter / 100f), .2f);
    }


    private int startBlockCount;
    
    private void Update()
    {
        if (currentComboCooldown > 0)
        {
            currentComboCooldown -= Time.deltaTime;
        }
        else
        {
            comboCounter = 0;
        }
        
        //Getting blocks for fake progress counting
        //TODO: THROW AWAY
        int newBlockCount = FindObjectsOfType<PathBlock>().Length;
        float LevelProgress = 1f - ((float) newBlockCount / (float) startBlockCount);
        
        OnLevelProgress(Easing.Quadratic.Out(LevelProgress));
    }


    private void OnLevelProgress(float progress)
    {
        levelProgressImage.fillAmount = progress;
        // print(progress);
        // float duration = Mathf.Approximately(0f, progress) ? 0 : .2f;
        // levelProgressImage.DOFillAmount(progress, duration).SetEase(Ease.OutCubic);
    }
    
}
