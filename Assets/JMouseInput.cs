using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JLib
{

    public class JMouseInput : MonoBehaviour
    {
        public enum MouseKey
        {
            None = 0,
            Left,
            Wheel,
            Right,
            LeftDrag,
            RightDrag,
            UpDrag,
            DownDrag,
        }
        [System.Serializable]
        public class MouseMap
        {
            public MouseKey input;
            public VK_Enum vk;
        }

        [SerializeField]
        List<MouseMap> mouseMappingList = new List<MouseMap>();


        // Update is called once per frame
        void Update()
        {
            #region press
            if( Input.GetMouseButton( 0 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Left , VK_State.Press );
            }

            if( Input.GetMouseButton( 1 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Right , VK_State.Press );
            }

            if( Input.GetMouseButton( 2 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Wheel , VK_State.Press );
            }
            #endregion

            #region Up
            if( Input.GetMouseButtonUp( 0 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Left , VK_State.Up );
            }

            if( Input.GetMouseButtonUp( 1 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Right , VK_State.Up );
            }

            if( Input.GetMouseButtonUp( 2 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Wheel , VK_State.Up );
            }
            #endregion

            #region Down
            if( Input.GetMouseButtonDown( 0 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Left , VK_State.Down );
            }

            if( Input.GetMouseButton( 1 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Right , VK_State.Down );
            }

            if( Input.GetMouseButton( 2 ) )
            {
                EnqueueMouseButtonEvent( MouseKey.Wheel , VK_State.Down );
            }

            #endregion
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            MouseKey whereDrag = MouseKey.None;


            if( horizontal != 0 )
            {
                whereDrag = ( horizontal > 0 ) ? MouseKey.RightDrag : MouseKey.LeftDrag;

                VirtualKeyParameter hParam = ParameterPool.GetParameter<VirtualKeyParameter>();
                hParam.key = GetVirtualKeyMapData( whereDrag );
                hParam.additionalData = horizontal;

                GlobalEventQueue.EnQueueEvent( VK_State.Press , hParam );
            }

            if( vertical != 0 )
            {
                whereDrag = ( vertical > 0 ) ? MouseKey.UpDrag : MouseKey.DownDrag;

                VirtualKeyParameter vParam = ParameterPool.GetParameter<VirtualKeyParameter>();
                vParam.key = GetVirtualKeyMapData( whereDrag );
                vParam.additionalData = vertical;

                GlobalEventQueue.EnQueueEvent( VK_State.Press , vParam );
            }

        }

        private void EnqueueMouseButtonEvent( MouseKey mouseKey , VK_State state )
        {
            VirtualKeyParameter lParam = ParameterPool.GetParameter<VirtualKeyParameter>();
            lParam.key = GetVirtualKeyMapData( mouseKey );
            GlobalEventQueue.EnQueueEvent( state , lParam );
        }

        VK_Enum GetVirtualKeyMapData( MouseKey mouseKey )
        {
            for( int i = 0 ; i < mouseMappingList.Count ; i++ )
            {
                if( mouseKey == mouseMappingList[ i ].input )
                {
                    return mouseMappingList[ i ].vk;
                }
            }

            Debug.LogErrorFormat( "MouseInput=> 매핑 데이터를 찾지 못했습니다. {0}" , mouseKey );
            return VK_Enum.None;
        }
    }
}