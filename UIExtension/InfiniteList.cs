using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace JLib
{
    public enum CollisionPosition
    {
        None,
        Left,
        Right
    }

    public interface IItemData
    {

    }

    public interface IItem 
    {
        IItemData GetData();
        void SetData( IItemData data );
        RectTransform rectTransform { get; }
        int Width { get; }
        int Height { get; }
    }

    public class InfiniteList : JMonoBehaviour
    {
        public LibUIID id;
        public int countOfSee;
        public ScrollRect scrollRect = null;
        public IItem Prefab = null;
        public List<IItem> items = new List<IItem>();
        public string path = "";
        public Transform contents = null;

        List<IItemData> itemData = null;
        RectTransform transScrollRect = null;
        //디버깅을 위해 보여야함
        [SerializeField]
        int lastIndex;

        [SerializeField]
        int firstIndex;

        [SerializeField]
        int itemCount;

        [SerializeField]
        int createdCount;


       
        void Awake()
        {
            transScrollRect = scrollRect.transform as RectTransform;
            createdCount = countOfSee + 2;
            scrollRect.onValueChanged.AddListener( ScrollCheck );
            GlobalEventQueue.RegisterListener( JLib.DefaultEvent.SetItemScrollRect, ListenSetItems );
        }
        /// <summary>
        /// 목록을 갱신해달라는 요청을 듣는 리스너
        /// </summary>
        /// <param name="param">List가 넘어온다.</param>
        public void ListenSetItems( object param )
        {
            var p = param as ScrollRectSetItemParameter;
            if (id != p.UIID)
            {
                return;
            }

            itemData = p.itemList;
            if (null == itemData)
            {
                Debug.LogError( "parameter is not List<Item>" );
            }
            //현재 생성되어있는 목록이 모자르면 더 생성해주고, 많으면 삭제해준다.
            if (countOfSee >= itemData.Count)
            {
                int interval = items.Count - itemData.Count;
                for (int i = 0; i < interval; i++)
                {
                    var selected = items.GetLast();
                    GameObject.Destroy( selected.rectTransform.gameObject );
                }
            }
            //모자른 경우, countofSee + 2만큼의 갯수가 될때 까지만 만들어준다.
            else
            {
                int interval = countOfSee - items.Count;
                for (int i = 0; i < interval; i++)
                {
                    CreateItem();
                }
            }

            //모두에게 데이터를 넣어준다.
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetData( itemData[i] );
            }
        }

        IItem CreateItem()
        {
            UnityEngine.Object obj = JResources.Load( path );
            GameObject instance = Instantiate( obj ) as GameObject;
            IItem item = instance.GetComponent<IItem>();
            items.Add( item );
            instance.transform.parent = contents;
            instance.transform.localScale = Vector3.one;
            return item;
        }
        void ScrollCheck(Vector2 eventParam )
        {
            //x의 움직임만을 검사함
            if (0f == eventParam.x)
            {
                return;
            }

            var where = WhereCollision();
            switch( where )
            {
                case CollisionPosition.Left:
                    lastIndex = Mathf.Clamp( ++lastIndex, 0, items.Count - 1 );
                    InsertItemDataToItem( items[0], itemData[lastIndex] );
                    MoveLeftEndToRightEnd();
                    break;

                case CollisionPosition.Right:
                    firstIndex = Mathf.Clamp( --firstIndex, 0, items.Count - 1 );
                    InsertItemDataToItem( items.GetLast(), itemData[firstIndex] );
                    MoveRightEndToLeftEnd();
                    break;

                case CollisionPosition.None:
                    //do nothing
                    break;

                default:
                    Debug.LogErrorFormat( "{0} is not supported yet" , where.ToString() );
                    break;
            }
        }

        void MoveLeftEndToRightEnd()
        {
            var lastItem = items.GetLast();
            var oldItem = items[0];
            //Vector2 newPosition = lastItem.rectTransform;
            //oldItem.rectTransform.anchoredPosition -= lastItem.rectTransform.anchoredPosition + lastItem.Width
            oldItem.rectTransform.SetAsLastSibling();

            items.RemoveAt( 0 );
            items.Add( oldItem );
        }

        void MoveRightEndToLeftEnd()
        {
            var oldItem = items.GetLast();
            oldItem.rectTransform.SetAsFirstSibling();
            items.RemoveAt( items.Count - 1 );
            items.Insert( 0, oldItem );

        }

        CollisionPosition WhereCollision()
        {
            //drag to left and contact leftLimit
            if(items[1].rectTransform.rect.xMax <  transScrollRect.rect.xMin
                && scrollRect.velocity.x < 0 )
            {
                return CollisionPosition.Left;
            }
            //drag to right and contact rightlimit
            if(items[items.Count - 2].rectTransform.rect.xMin<  transScrollRect.rect.xMax
                && scrollRect.velocity.x >=0 )
            {
                return CollisionPosition.Right;
            }

            return CollisionPosition.None;            
        }

        void InsertItemDataToItem(IItem item, IItemData data)
        {
            item.SetData( data );
        }
        
    }
    

}

