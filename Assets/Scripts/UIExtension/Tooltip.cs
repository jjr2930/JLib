using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JLib
{
    public class Tooltip : MonoSingle<Tooltip>, JIUIManager
    {
        Image bg = null;
        Text text = null;
        // Use this for initialization
        public void Awake()
        {
            text = transform.GetChild( 0 ).GetComponent<Text>();
            bg = GetComponent<Image>();

            GlobalEventQueue.RegisterListener(DefaultEvent.ShowTooltip, ListenShowTooltip);
            GlobalEventQueue.RegisterListener(DefaultEvent.HideTooltip, ListenHideTooltip);
            
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive( value );
        }

        /// <summary>
        /// 툴팁을 보여준다.
        /// </summary>
        /// <param name="value">표시 문자</param>
        /// <param name="screenPosition">표시할 위치</param>
        public void ShowTooltip( string value , Vector2 screenPosition )
        {
            gameObject.SetActive(true);
            float width = Mathf.Sqrt( value.Length) * 30f;
            text.text = value;
            var curSizeDelta = bg.rectTransform.sizeDelta;
            curSizeDelta.x = width;
            bg.rectTransform.sizeDelta = curSizeDelta;
            bg.rectTransform.position = screenPosition;
                        
        }
        
        /// <summary>
        /// 툴팁을 보여준다.
        /// </summary>
        /// <param name="localKey">로컬라이즈 키</param>
        /// <param name="screenPosition">표시할 위치</param>
        public void ShowTooltipUseLocalKey( string localKey, Vector2 screenPosition )
        {
            string localString = LocalizeTable.GetLocalString(localKey);
            ShowTooltip( localString , screenPosition);
        }

        public void ListenShowTooltip(object param)
        {
            TooltipParameter p = param as TooltipParameter;
            ShowTooltipUseLocalKey(p.localKey,p.position);
            //ParameterPool.ReturnPool(p);
        }
        public void ListenHideTooltip(object param)
        {
            if(null != param)
            {
                Debug.LogErrorFormat("Tooltip.ListenStopTooltip=> param({0}) is not null, it is wrong",param.GetType().ToString());
            }
            this.gameObject.SetActive(false);
        }
    }
}