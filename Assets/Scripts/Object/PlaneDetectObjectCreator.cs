using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudPet.Common
{
    /// <summary>
    /// タップしてオブジェクトを生成するやつ
    /// </summary>
    public class PlaneDetectObjectCreator : MonoBehaviour
    {
        [SerializeField]
        private InputPlaneDetectionHandler _handler;

        [SerializeField] 
        private GameObject _prefab;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            SetEvent();
        }
        
        private void SetEvent()
        {
            _handler.onPointerDownCallback += (isHit, position) =>
            {
                if (isHit)
                {
                    CreateObject(position);
                }
            };
        }

        private void CreateObject(Vector3 position)
        {
            var obj = Instantiate(_prefab);
            obj.transform.position = position;
        }
    }
}

