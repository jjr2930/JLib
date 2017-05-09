using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JLib;
public class TooltipEventCreator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//, IPointerExitHandler
{
    [SerializeField]
    bool isDownStarted = false;

    [SerializeField]
    float tooltipTime = 1f;

    [SerializeField]
    string localKey = "";

    IEnumerator coroutine = null;

    IEnumerator RoutineTooltip(Vector2 position)
    {
        yield return new WaitForSecondsRealtime( tooltipTime );
        if( isDownStarted )
        {
            Debug.LogFormat( "Tooltip : {0}" , localKey );
            TooltipParameter p = ParameterPool.GetParameter<TooltipParameter>();
            p.localKey = localKey;
            p.position = position + Vector2.up * 20;
            GlobalEventQueue.EnQueueEvent(DefaultEvent.ShowTooltip,p);
        }
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        isDownStarted = true;
        if(null != coroutine)
        {
            StopCoroutine(coroutine);
        }
        coroutine = RoutineTooltip(eventData.position);
        StartCoroutine(coroutine);
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        Debug.Log("OnPointerUp Tooltip hide");
        GlobalEventQueue.EnQueueEvent( DefaultEvent.HideTooltip , null );
        isDownStarted = false;
    }

    //public void OnPointerExit( PointerEventData eventData )
    //{
    //    Debug.Log("OnPointerExit Tooltip hide");
    //    GlobalEventQueue.EnQueueEvent( DefaultEvent.HideTooltip , null );
    //    isDownStarted = false;
    //}

}
