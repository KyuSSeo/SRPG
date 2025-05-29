using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


//  상태 기반으로 전투 상황 관리로 StateMachine을 이용
public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    void Start()
    {
        ChangeState<InitBattleState>();
    }
}