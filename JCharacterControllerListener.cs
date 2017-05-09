using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JLib
{

    [System.Serializable]
    public class CharacterControllerEventData
    {
        public VK_Enum key;
        public VK_State state;
        public CharacterMethod method;
    }
    [RequireComponent( typeof( CharacterController ) )]
    public class JCharacterControllerListener : JMonoBehaviour
    {
        [SerializeField]
        List<CharacterControllerEventData> mappingList
            = new List<CharacterControllerEventData>();

        [SerializeField]
        CharacterController cc = null;

        [SerializeField]
        float moveSpeed  = 0f;

        [SerializeField]
        float rotateSpeed = 0f;

        [SerializeField]
        float jumpForce = 10f;


        Vector3 jumpAccel = Vector3.zero;
        float rotateFactor = 0f;

        public bool IsGrounded = false;
        Vector3 moveAccel = Vector3.zero;


        Dictionary<VK_Enum, Action> PressListeners
            = new Dictionary<VK_Enum,Action>();

        Dictionary<VK_Enum, Action> UpListeners
            = new Dictionary<VK_Enum,Action>();

        Dictionary<VK_Enum, Action> DownListeners
            = new Dictionary<VK_Enum,Action>();

        void Awake()
        {
            cc = GetComponent<CharacterController>();
            MappingVirtualKey();
            RegisterListener();
        }

        void Update()
        {
            if( !cc.isGrounded )
            {
                jumpAccel += Physics.gravity * JTime.DeltaTime;
            }

            moveAccel *= moveSpeed * JTime.DeltaTime;


            cc.Move( moveAccel + jumpAccel );
            transform.Rotate( Vector3.up , rotateSpeed * rotateFactor * JTime.DeltaTime );

            IsGrounded = cc.isGrounded;
        }

        void LateUpdate()
        {
            V3Extension.SetZero( ref moveAccel );
            rotateFactor = 0f;
        }

        void MappingVirtualKey()
        {
            for( int i = 0 ; i < mappingList.Count ; i++ )
            {
                Action action = null;
                VK_Enum key = mappingList[i].key;
                Dictionary<VK_Enum,Action> foundedDic = null;
                switch( mappingList[ i ].state )
                {
                    case VK_State.Down:
                        foundedDic = DownListeners;
                        break;

                    case VK_State.Press:
                        foundedDic = PressListeners;
                        break;

                    case VK_State.Up:
                        foundedDic = UpListeners;
                        break;
                }

                switch( mappingList[ i ].method )
                {
                    case CharacterMethod.Back:
                        action = Back;
                        break;

                    case CharacterMethod.Forward:
                        action = Forward;
                        break;

                    case CharacterMethod.Jump:
                        action = Jump;
                        break;

                    case CharacterMethod.Left:
                        action = Left;
                        break;

                    case CharacterMethod.Right:
                        action = Right;
                        break;

                    case CharacterMethod.RotClock:
                        action = RotClock;
                        break;

                    case CharacterMethod.RotCounterClock:
                        action = RotCounterClock;
                        break;
                }

                foundedDic.Add( key , action );
            }
        }
        void RegisterListener()
        {
            GlobalEventQueue.RegisterListener( VK_State.Press , ListenPress );
            GlobalEventQueue.RegisterListener( VK_State.Down , ListenDown );
            GlobalEventQueue.RegisterListener( VK_State.Up , ListenUp );
        }
        void ListenPress( object p )
        {
            VK_Enum vk = (VK_Enum)p;
            Action action = null;
            if( PressListeners.TryGetValue( vk , out action ) )
            {
                action();
            }
        }

        void ListenDown( object p )
        {
            VK_Enum vk = (VK_Enum)p;
            Action action = null;
            if( DownListeners.TryGetValue( vk , out action ) )
            {
                action();
            }
        }

        void ListenUp( object p )
        {
            VK_Enum vk = (VK_Enum)p;
            Action action = null;
            if( UpListeners.TryGetValue( vk , out action ) )
            {
                action();
            }
        }

        void Forward()
        {
            V3Extension.Add( ref moveAccel , transform.forward );
        }

        void Back()
        {
            V3Extension.Subtract( ref moveAccel , transform.forward );
        }

        void Left()
        {
            V3Extension.Subtract( ref moveAccel , transform.right );
        }

        void Right()
        {
            V3Extension.Add( ref moveAccel , transform.right );
        }

        void RotCounterClock()
        {
            rotateFactor = -1f;
        }

        void RotClock()
        {
            rotateFactor = 1f;
        }

        void Jump()
        {
            jumpAccel = - Physics.gravity.normalized * jumpForce;
        }
    }

}
