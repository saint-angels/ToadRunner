using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] LocalCacheManager localCacheManager = null;
    [SerializeField] UiManager uiManager = null;
    [SerializeField] private GameController gameController = null;
    [SerializeField] private PathGenerator pathGenerator = null;


    public LocalCacheManager LocalCacheManager => localCacheManager;
    public UiManager UiManager => uiManager;
    
    
    
    void Start()
    {
        pathGenerator.Init();
        gameController.Init();
        
        uiManager.Init(gameController);
    }
}
