using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CutSceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;


    protected override void Awake()
    {
        base.Awake();
        //  ConversationController ��������
        conversationController = owner.GetComponentInChildren<ConversationController>();
        // ��ȭ ������ �ε�
        data = Resources.Load<ConversationData>("Conversations/IntroScene");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (data)
            Resources.UnloadAsset(data);
    }
    public override void Enter()
    {
        base.Enter();
        conversationController.Show(data);
    }

    // �̺�Ʈ ������
    protected override void AddListeners()
    {
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ConversationController.completeEvent -= OnCompleteConversation;
    }

    // ����� �Է�
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        base.OnFire(sender, e);
        conversationController.Next();
    }
    // ��ȭ�� ������ �� ȣ��
    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        owner.ChangeState<SelectUnitState>();
    }
}
