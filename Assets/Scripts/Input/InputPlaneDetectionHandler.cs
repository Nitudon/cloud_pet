using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CloudPet.Common 
{
    /// <summary>
    /// 入力を検知して平面検出と交差させるやつ
    /// </summary>
    public class InputPlaneDetectionHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] 
        private ARRaycastManager _raycastManager;

        [SerializeField] 
        private TrackableType _trackableType = TrackableType.Planes;

        public event Action<bool, Vector3> onPointerDownCallback;
        
        private void Awake()
        {
            if (_raycastManager == null)
            {
                var raycastManager = FindObjectOfType<ARRaycastManager>();
                _raycastManager = raycastManager;
            }
        }
        
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            if (_raycastManager == null)
            {
                Debug.LogError("RaycastManagerが存在しません");
                return;
            }

            var touch = pointerEventData.position;
            var hitPosition = Vector3.zero;
            var hitList = new List<ARRaycastHit>();
            var raycast = _raycastManager.Raycast(touch, hitList, _trackableType);

            if (raycast)
            {
                var hit = hitList.FirstOrDefault().pose;
                hitPosition = hit.position;
            }
            
            onPointerDownCallback?.Invoke(raycast, hitPosition);
        }
    }
}
