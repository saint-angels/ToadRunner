using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathBlock : MonoBehaviour
{
    [SerializeField] private GameObject blockView = null;

    [SerializeField] private GameObject coin1 = null;
    [SerializeField] private GameObject coin2 = null;
    [SerializeField] private GameObject spikes1 = null;
    [SerializeField] private GameObject spikes2 = null;
    
    [SerializeField] private Color touchedColor;
    
    public float fallDelay = 1f;


    public bool CanTouch => isTouched == false;

    private bool isTouched = false;

    private void Awake()
    {
        spikes1.SetActive(false);
        spikes2.SetActive(false);
        coin1.SetActive(false);
        coin2.SetActive(false);

        if (Random.value < .1)
        {
            if (Random.value < .5f)
            {
                coin1.SetActive(true);        
            }
            else
            {
                coin2.SetActive(true);
            }
        }
        if (Random.value < .025f)
        {
            if (Random.value < .5f)
            {
                spikes1.SetActive(true);        
            }
            else
            {
                spikes2.SetActive(true);
            }
        }
    }

    public void SetTouched()
    {
        if (isTouched == false)
        {
            isTouched = true;
        }
        else
        {
            return;
        }
        
        blockView.GetComponent<Renderer>().material.DOColor(touchedColor, 0.2f).SetEase(Ease.OutQuad);
        
        Vector3 targetPosition = transform.position - Vector3.up * 5f;
        Sequence destructionSequence = DOTween.Sequence();

        var shakeTween = blockView.transform.DOShakePosition(1f, Vector3.right * .025f, 40, 90f, false, false);
        destructionSequence.Insert(0f, shakeTween);
        var movementDown = transform.DOMoveY(-5f, .5f);
        destructionSequence.Insert(fallDelay, movementDown);


        destructionSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
