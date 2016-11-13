using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class JMonoBehaviour : MonoBehaviour
    {
        public new Transform transform
        {
            get
            {
                if(null == _transform )
                {
                    _transform = base.transform;
                }
                return transform;
            }
        }

        public new GameObject gameObject
        {
            get
            {
                if(null == _gameOjbect)
                {
                    _gameOjbect = base.gameObject;
                }
                return _gameOjbect;
            }
        }

        private GameObject _gameOjbect = null;
        private Transform _transform = null;
    }
}
