using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI ������Ʈ�� ��ġ��Ű��, �ִϸ��̼����� �̵���Ű�� ��� ����
// LayoutAnchor ������Ʈ�� �ݵ�� �䱸
[RequireComponent(typeof(LayoutAnchor))]
public class Panel : MonoBehaviour
{
    [SerializeField] List<Position> positionList;
    Dictionary<string, Position> positionMap;
    LayoutAnchor anchor;

    // ���� ��ġ
    public Position CurrentPosition { get; private set; }
    // ���� ���� ���� �ִϸ��̼�
    public Tweener Transition { get; private set; }
    // �ִϸ��̼� ���� ����
    public bool InTransition { get { return Transition != null; } }

    // ��ġ �̸����� �ش� Position�� �������� �ε���
    public Position this[string name]
    {
        get
        {
            if (positionMap.ContainsKey(name))
                return positionMap[name];
            return null;
        }
    }


    [Serializable]
    public class Position
    {
        public string name;
        public TextAnchor myAnchor;
        public TextAnchor parentAnchor;
        public Vector2 offset;
        
        // �̸� ����
        public Position(string name)
        {
            this.name = name;
        }

        // �̸��� ��Ŀ ����
        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor) : this(name)
        {
            this.myAnchor = myAnchor;
            this.parentAnchor = parentAnchor;
        }

        // �̸�, ��Ŀ, �����±��� ����
        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name, myAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }

    private void Awake() => Init();
    private void Init()
    {
        anchor = GetComponent<LayoutAnchor>();
        positionMap = new Dictionary<string, Position>(positionList.Count);
        // ����Ʈ�� �ִ� ��ġ ������ ��ųʸ��� ���
        for (int i = positionList.Count - 1; i >= 0; --i)
            AddPosition(positionList[i]);
    }


    // �� ��ġ ���� ��ųʸ� �߰�
    public void AddPosition(Position p)
    {
        positionMap[p.name] = p;
    }

    // ��ġ ���� ��ųʸ� ����
    public void RemovePosition(Position p)
    {
        if (positionMap.ContainsKey(p.name))
            positionMap.Remove(p.name);
    }

    // ��ġ �̸���� ����
    public Tweener SetPosition(string positionName, bool animated)
    {
        return SetPosition(this[positionName], animated);
    }
    // Position���� ��ġ ����
    public Tweener SetPosition(Position p, bool animated)
    {
        // ���� ��ġ
        CurrentPosition = p;
        // ��ġ�� �����Ѱ�?  
        if (CurrentPosition == null)
            return null;

        // ���� �ִϸ��̼� ����
        if (InTransition)
            Transition.Stop();

        //  �ִϸ��̼� ���ο� ���� �ִϸ��̼� ���� ���� ����
        if (animated)
        {
            Transition = anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return Transition;
        }
        else
        {
            anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return null;
        }
    }
    private void Start()
    {
        // ��ġ ������ ������ ù ��ġ�� ����
        if (CurrentPosition == null && positionList.Count > 0)
            SetPosition(positionList[0], false);
    }
}
