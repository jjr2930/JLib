using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using JLib;
public class JCommonPopupUIManager : MonoSingle<JCommonPopupUIManager>, JIUIManager
{
    [SerializeField]
    Button okBtn1 = null;

    [SerializeField]
    LocalizeTextForUGUI okBtn1Label = null;

    [SerializeField]
    Button okBtn2 = null;

    [SerializeField]
    LocalizeTextForUGUI okBtn2Label = null;

    [SerializeField]
    Button cancelBtn = null;

    [SerializeField]
    LocalizeTextForUGUI cancelText = null;

    [SerializeField]
    LocalizeTextForUGUI title = null;

    [SerializeField]
    LocalizeTextForUGUI descript = null;

    [SerializeField]
    UnityEvent onShowEvent;

    [SerializeField]
    UnityEvent onHideEvent;

    public void SetActive(bool value)
    {
        gameObject.SetActive( value );
    }
    public void Awake()
    {
        SetDefaultListener();
        gameObject.SetActive( false );
    }

    void SetDefaultListener()
    {
        cancelBtn.onClick.AddListener( ReturnTImeScale );
        okBtn1.onClick.AddListener( ReturnTImeScale );
        okBtn2.onClick.AddListener( ReturnTImeScale );

        cancelBtn.onClick.AddListener( ClosePopup );
        okBtn1.onClick.AddListener( ClosePopup );
        okBtn2.onClick.AddListener( ClosePopup );
    }

    void RemoveAllListener()
    {
        cancelBtn.onClick.RemoveAllListeners();
        okBtn1.onClick.RemoveAllListeners();
        okBtn2.onClick.RemoveAllListeners();
    }

    void OnEnable()
    {
        onShowEvent.Invoke();
    }

    void OnDisable()
    {
        onHideEvent.Invoke();
        RemoveAllListener();
        SetDefaultListener();
    }
    public void ListenShowCommon(object param)
    {
        CommonPopupEventParameter p = param as CommonPopupEventParameter;
        if(string.IsNullOrEmpty(p.btn2LabelKey))
        {
            OnShow( p.titleKey,
                p.descriptKey,
                p.btn1LabelKey,
                p.cancelLabelKey,
                p.btn1Action,
                p.cancelAction,
                p.pauseGame );
        }
        else
        {
            OnShow( p.titleKey,
                p.descriptKey,
                p.btn1LabelKey,
                p.btn2LabelKey,
                p.cancelLabelKey,
                p.btn1Action,
                p.btn2Action,
                p.cancelAction,
                p.pauseGame );
        }
    }

    /// <summary>
    /// 화면에 띄우고 onShowEvent에 등록된 메소드들을 실행한다. 버튼 3개짜리
    /// </summary>
    /// <param name="titleKey">제목</param>
    /// <param name="descriptKey">설명</param>
    /// <param name="btn1LabelKey">첫번째 버튼에 들어갈 텍스트</param>
    /// <param name="btn2LabelKey">두번째 버튼에 들어갈 텍스트</param>
    /// <param name="cancelLabelKey">취소 버튼에 들어갈 텍스트</param>
    /// <param name="btn1Action">첫번째 버튼을 누르면 실행할 함수</param>
    /// <param name="btn2Action">두번째 버튼을 누르면 실행할 함수</param>
    /// <param name="cancelAction">취소 버튼을 누르면 실행할 함수</param>
    /// <param name="pauseGame">게임을 일시정지 시킬것인가</param>
    public void OnShow(
        string titleKey,
        string descriptKey,
        string btn1LabelKey,
        string btn2LabelKey,
        string cancelLabelKey,
        UnityAction btn1Action,
        UnityAction btn2Action,
        UnityAction cancelAction,
        bool pauseGame
        )
    {
        //set element
        this.title.Key = titleKey;
        this.descript.Key = descriptKey;
        this.okBtn1Label.Key = btn1LabelKey;
        this.okBtn2Label.Key = btn2LabelKey;
        this.cancelText.Key = cancelLabelKey;
        this.okBtn1.onClick.AddListener( btn1Action );
        this.okBtn2.onClick.AddListener( btn2Action );
        this.cancelBtn.onClick.AddListener( cancelAction );
        JTime.TimeScale = ( pauseGame ) ? 0.0f : 1f;
    }

    /// <summary>
    /// 화면에 띄우고 onShowEvent에 등록된 메소드들을 실행한다. 버튼 3개짜리
    /// </summary>
    /// <param name="titleKey">제목</param>
    /// <param name="descriptKey">설명</param>
    /// <param name="btn1LabelKey">첫번째 버튼에 들어갈 텍스트</param>
    /// <param name="cancelLabelKey">취소 버튼에 들어갈 텍스트</param>
    /// <param name="btn1Action">첫번째 버튼을 누르면 실행할 함수</param>
    /// <param name="cancelAction">취소 버튼을 누르면 실행할 함수</param>
    /// <param name="pauseGame">게임을 일시정지 시킬것인가</param>
    public void OnShow(
        string titleKey,
        string descriptKey,
        string btn1LabelKey,
        string cancelLabelKey,
        UnityAction btn1Action,
        UnityAction cancelAction,
        bool pauseGame
        )
    {
        //set element
        this.title.Key = titleKey;
        this.descript.Key = descriptKey;
        this.okBtn1Label.Key = btn1LabelKey;
        this.cancelText.Key = cancelLabelKey;
        this.okBtn1.onClick.AddListener( btn1Action );
        this.cancelBtn.onClick.AddListener( cancelAction );
        JTime.TimeScale = ( pauseGame ) ? 0.0f : 1f;
    }

    void ReturnTImeScale()
    {
        JTime.TimeScale = 1f;
    }
    void ClosePopup()
    {
        this.gameObject.SetActive( false );
    }
}

