using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using JLib;

namespace JLibEditor
{
    /// <summary>
    /// 간단한 테이블 에디터들의 베이스 클래스
    /// </summary>
    /// <typeparam name="T1">데이터의 리스트를 갖고있는 클래스</typeparam>
    /// <typeparam name="T2">데이터 클래스</typeparam>
    public abstract class BaseTableEditor<T1, T2> : EditorWindow
        where T1 : class, ITable<T2>, new()
        where T2 : class, new()
    {
        public const int DELETE_BTN_SIZE = 15;
        public const int DELETE_HEIGHT_FACTOR = 1;
        public const int HEIGHT_SIZE = 20;
        Vector2 scroll = Vector2.zero;  //스크롤용
        protected string tablePath = "";//테이블 주소
        protected T1 table; //테이블
        protected int COLUMN_COUNT = 5; //컬럼 숫자

        /// <summary>
        /// 초기화 함수
        /// </summary>
        protected void Initialize()
        {
            PreInitialze();
            TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>( tablePath );
            if( null == asset )
            {
                //create directory and file
                if( !Directory.Exists( @"./Assets/Resources/Tables" ) )
                {
                    Directory.CreateDirectory( @"./Assets/Resources/Tables" );
                }
                FileStream stream = new FileStream( @"./" + tablePath,
                     FileMode.OpenOrCreate,
                     FileAccess.ReadWrite );
                //clear inside
                ClearSteam( stream );

                table = new T1();
                table.List = new List<T2>();
                table.List.Add( new T2() );

                //create default data

                string json = JsonUtility.ToJson(table);
                byte[] bytes = Encoding.UTF8.GetBytes( json );

                stream.Write( bytes, 0, bytes.Length );
                stream.Close();
            }
            else
            {
                table = JsonUtility.FromJson<T1>( asset.text );
                //ToObject(asset.text);
            }
        }

        /// <summary>
        /// 스트림 지우기용
        /// </summary>
        /// <param name="stream">지울 파일 스트림</param>
        void ClearSteam( FileStream stream )
        {
            byte[] emptyBytes = new byte[ stream.Length ];
            stream.Write( emptyBytes, 0, emptyBytes.Length );
        }

        /// <summary>
        /// 화면에 표시될 내용
        /// </summary>
        private void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView( scroll );
            {
                EditorGUILayout.BeginHorizontal();
                {
                    for( int i = 0; i < COLUMN_COUNT; i++ )
                    {                       
                        EditorGUILayout.BeginVertical( "CN Box" );
                        {
                            if( i == COLUMN_COUNT - 1 )   //마지막 칸은 지우기 UI표시용
                            {
                                DrawEmpty();
                            }
                            else
                            {
                                OnGUI_ComlumnName( i );
                            }
                            GUILayout.Space( 10 );
                            for( int j = 0; j < table.List.Count; j++ )
                            {
                                if( i == COLUMN_COUNT - 1 ) //마지막 칸은 지우기 UI
                                {
                                    DrawDeleteButton( j );
                                }
                                else
                                {
                                    OnGUI_Body_Element( i, table.List[ j ] );
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            OnGUI_ETC();
        }

        protected void DrawEmpty()
        {
            EditorGUILayout.LabelField( "",GUILayout.MaxWidth(DELETE_BTN_SIZE) );
        }

        /// <summary>
        /// 지우기 버튼 그리기용
        /// </summary>
        /// <param name="listIndex">지울 리스트의 인덱스</param>
        protected void DrawDeleteButton(int listIndex)
        {
            if( GUILayout.Button( "-",
                    GUILayout.MaxWidth( DELETE_BTN_SIZE ),
                    GUILayout.MinHeight( HEIGHT_SIZE - DELETE_HEIGHT_FACTOR ),
                    GUILayout.MaxHeight( HEIGHT_SIZE - DELETE_HEIGHT_FACTOR ) ) )
            {
                table.List.RemoveAt( listIndex );
            }
        }

        /// <summary>
        /// 하단에 그릴 기타 UI들
        /// </summary>
        protected void OnGUI_ETC()
        {
            if(GUILayout.Button("+"))
            {
                table.List.Add( new T2() );
            }

            if(GUILayout.Button("Save"))
            {
                Save();
            }
        }

        /// <summary>
        /// 말 그대로 저장용
        /// </summary>
        void Save()
        {
            string json = ToJson();
            Debug.Log( json );
            if( !File.Exists( tablePath ) )
            {
                var opened = File.Open( tablePath, FileMode.OpenOrCreate );
                opened.Close();
            }
            File.WriteAllText( tablePath, json );
        }

        /// <summary>
        /// 자식마다 다르게 정의할 선초기화 함수 이곳에서 테이블의 주소와
        /// 컬럼 갯수를 정의한다.
        /// </summary>
        protected abstract void PreInitialze();

        /// <summary>
        /// 컬럼(열)의 이름을 출력한다.
        /// </summary>
        /// <param name="column">컴럼(열)번호</param>
        protected abstract void OnGUI_ComlumnName(int column);

        /// <summary>
        /// 자식 클래스에서 그려주어야할 테이블의 요소들
        /// </summary>
        /// <param name="column">컬럼(열) 번호</param>
        /// <param name="data">해당 열의 데이터</param>
        protected abstract void OnGUI_Body_Element(int column, T2 data);
        
        /// <summary>
        /// Unity JsonUtility is not supported generic like T, T1
        /// </summary>
        /// <returns>json</returns>
        protected abstract string ToJson(); //must be deleted
        /// <summary>
        ///  Unity JsonUtility is not supported generic like T, T1
        /// </summary>
        /// <param name="json">json to be a object</param>
        protected abstract void ToObject(string json); //must be deleted
    }
}
