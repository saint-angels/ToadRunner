using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PathBlock : MonoBehaviour
{
    [SerializeField] private GameObject blockView = null;
    [SerializeField] private Collider collider;

    [SerializeField] private Color touchedColor;
    
    public float fallDelay = 1f;


    private bool isTouched = false;
    
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

        var shakeTween = blockView.transform.DOShakePosition(5f, Vector3.right * .025f, 40, 90f, false, false);
        destructionSequence.Insert(0f, shakeTween);
        var movementDown = transform.DOMoveY(-5f, 1f);
        destructionSequence.Insert(fallDelay, movementDown);


        destructionSequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
