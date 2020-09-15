using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PathBlock : MonoBehaviour
{

    [SerializeField] private Collider collider;


    public void SetTouched()
    {
        print("Toucheing");
        collider.enabled = false;

        GetComponent<Renderer>().material.DOColor(Color.red, 0.2f).SetEase(Ease.OutQuad);
        
        Vector3 targetPosition = transform.position - Vector3.up * 5f;
        Sequence newSequence = DOTween.Sequence();
        newSequence.PrependInterval(3f);
        var movement = transform.DOMoveY(-5f, 1f);
        newSequence.Append(movement);
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
