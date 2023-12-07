using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager instance;

    [SerializeField]
    private GameObject backButtonObject;

    /// <summary>
    /// 마지막 재고를 Pop했을때 호출
    /// </summary>
    public Action onBackButtonStackEmpty;

    private Stack<Action> backButtonActionStack;

    public int CurrentActionCount { get => backButtonActionStack.Count; }

    public bool BackButtonEnable { get => backButtonObject.activeSelf; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        backButtonActionStack = new Stack<Action>();
    }

    private void Update()
    {
        if (!BackButtonEnable)
            return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonClicked();
        }
    }

    public void AddBackButtonAction(Action act)
    {
        backButtonActionStack.Push(act);
        Debug.LogFormat("뒤로가기 액션 추가 => count: {0}", backButtonActionStack.Count);
    }

    public void OnBackButtonClicked()
    {
        if (backButtonActionStack.Count <= 0)
        {
            Debug.LogError("뒤로가기 액션이 비어있습니다.");
            return;
        }

        backButtonActionStack.Pop().Invoke();
        Debug.LogFormat("뒤로가기 액션 실행 => count: {0}", backButtonActionStack.Count);

        if (backButtonActionStack.Count <= 0)
        {
            onBackButtonStackEmpty?.Invoke();
        }
    }

    /// <summary>
    /// 액션을 실행하지않고 제거하기 위해 사용
    /// </summary>
    /// <returns></returns>
    public Action PopAction()
    {
        if (backButtonActionStack.Count <= 0)
            return null;

        Debug.LogFormat("뒤로가기 액션 제거 => count: {0}", backButtonActionStack.Count - 1);
        return backButtonActionStack.Pop();
    }

    /// <summary>
    /// 액션을 실행하지않고 전부 꺼냄
    /// </summary>
    public void PopActionUntilEmpty()
    {
        while(backButtonActionStack.Count > 0)
        {
            backButtonActionStack.Pop();
        }
    }

    /// <summary>
    /// 스택이 빌때까지 액션 실행
    /// </summary>
    public void DoActionUntileEmpty()
    {
        while(CurrentActionCount > 0)
        {
            OnBackButtonClicked();
        }
    }

    /// <summary>
    /// 백버튼 활성화 / 비활성화
    /// </summary>
    /// <param name="active"></param>
    public void SetBackkButtonEnable(bool active)
    {
        ////backButtonEnable = active;

        ////LobbyCanvasManager.instance.ActiveBackButton(active);
        backButtonObject.SetActive(active);
    }
}
