using System;
using System.Collections;
using System.Collections.Generic;
using Helpers.StatefulEvent;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        NONE,
        PLAYING,
        GAME_OVER,
    }
    
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform playerStartPoint;
    
    
    public IStatefulEvent<GameState> State => _state;
    private readonly  StatefulEventInt<GameState> _state = StatefulEventInt.CreateEnum<GameState>(GameState.NONE);


    private void Awake()
    {
        State.OnValueChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.NONE:
                break;
            case GameState.PLAYING:
                player.ResetPlayer(playerStartPoint);
                break;
            case GameState.GAME_OVER:
                //hacky way of toggling PLAYING state
                _state.Set(GameState.PLAYING);
                break;
        }
    }

    public void Init()
    {
        _state.Set(GameState.PLAYING);
    }

    void Update()
    {
        switch (_state.Value)
        {
            case GameState.NONE:
                break;
            case GameState.PLAYING:
                if (player.transform.position.y <= -3f) 
                {
                    _state.Set(GameState.GAME_OVER);
                }
                
                break;
        }
    }
}
