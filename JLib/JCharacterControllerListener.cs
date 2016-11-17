using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JLib
{
    [System.Serializable]
    public class CharacterControllerEventData
    {
        public VK_Enum key;
        public UnityAction<object> Listener;
    }
    [RequireComponent( typeof( CharacterController ) )]
    public class JCharacterControllerListener : JMonoBehaviour
    {
        [SerializeField]
        CharacterController cc = null;

        [SerializeField]
        List<CharacterControllerEventData> eventDataList = new List<CharacterControllerEventData>();

        [SerializeField]
        float moveSpeed  = 0f;

        [SerializeField]
        float rotateSpeed = 0f;

        [SerializeField]
        float jumpForce = 10f;


        float rotateFactor = 0f;

        Vector3 moveAccel = Vector3.zero;

        void Awake()
        {
            cc = GetComponent<CharacterController>();
            RegisterListeners();
        }

        void Update()
        {
            moveAccel.Subtract( App.Gravity );
            moveAccel.Multiply(moveSpeed);
            moveAccel.Multiply( JTime.DeltaTime );
            cc.Move( moveAccel );
            transform.Rotate( Vector3.up , rotateSpeed * rotateFactor * JTime.DeltaTime );
            rotateFactor = 0f;
            moveAccel.Multiply( 0f );
        }

        void RegisterListeners()
        {
            for( int i = 0 ; i < eventDataList.Count ; i++ )
            {
                GlobalEventQueue.RegisterListener( eventDataList[ i ].key , eventDataList[ i ].Listener );
            }
        }


        public void ListenGoFoward( object param )
        {
            moveAccel = transform.forward;
        }

        public void ListenGoBack( object param )
        {
            moveAccel.Add( -transform.forward );
        }

        public void ListenGoLeft( object param )
        {
            moveAccel.Add( -transform.right );
        }

        public void ListenGoRight( object param )
        {
            moveAccel.Add( transform.right );
        }

        public void ListenRotCounterClock( object param )
        {
            rotateFactor = -1f;
        }

        public void ListenRotClock( object param )
        {
            rotateFactor = 1f;
        }

        public void ListenJump( object param )
        {
            moveAccel += transform.up * jumpForce;
        } 
    }
}
