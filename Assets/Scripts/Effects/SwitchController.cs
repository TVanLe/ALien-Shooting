using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour, IDragHandler
{
    [SerializeField] private int maxPage;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform modePageRect;
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button preBtn;
    
    private int curPage;
    private Vector3 targetPos;

    private float dragThreshold;
    private void Awake()
    {
        curPage = 1;
        targetPos = modePageRect.localPosition;
        dragThreshold = Screen.width /15;
    }
    
    public void NextPage()
    {
        if (curPage < maxPage)
        {
            curPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void PreviousPage()
    {
        if (curPage > 1)
        {
            curPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        modePageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if(eventData.position.x > eventData.pressPosition.x) PreviousPage();
            else NextPage();
        }
    }

    private void Update()
    {
        UpdateArrowButton();
    }

    private void UpdateArrowButton()
    {
        if (curPage == 1)
        {
            preBtn.interactable = false;
        }
        else
        {
            preBtn.interactable = true;
        }

        if (curPage == maxPage)
        {
            nextBtn.interactable = false;
        }
        else
        {
            nextBtn.interactable = true;
        }
    }
    
}
