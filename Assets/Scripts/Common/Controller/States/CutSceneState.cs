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
        //  ConversationController 가져오기
        conversationController = owner.GetComponentInChildren<ConversationController>();
        // 대화 데이터 로드
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

    // 이벤트 리스너
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

    // 사용자 입력
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        base.OnFire(sender, e);
        conversationController.Next();
    }
    // 대화가 끝났을 때 호출
    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        owner.ChangeState<SelectUnitState>();
    }
}
