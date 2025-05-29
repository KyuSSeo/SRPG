using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


//  ���� ������� ���� ��Ȳ ������ StateMachine�� �̿�
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