using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace CloudPet.Common 
{
    /// <summary>
    /// 入力を検知して平面検出と交差させるやつ
    /// </summary>
    public class InputPlaneDetectionHandler : MonoBehaviour
    {
        [SerializeField] 
        private ARRaycastManager _raycastManager;

        [SerializeField] 
        private TrackableType _trackableType = TrackableType.Planes;

        private List<ARRaycastHit> _hitListPool = new List<ARRaycastHit>();

        public event Action<bool, Vector3> onPointerDownCallback;
        
        private void Start()
        {
            if (_raycastManager == null)
            {
                var raycastManager = FindObjectOfType<ARRaycastManager>();
                _raycastManager = raycastManager;
            }

            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += finger => RaycastCheck(finger.screenPosition);
        }
        
        private void RaycastCheck(Vector2 position)
        {
            if (_raycastManager == null)
            {
                Debug.LogError("RaycastManagerが存在しません");
                return;
            }

            var hitPosition = Vector3.zero;
            _hitListPool.Clear();
            var raycast = _raycastManager.Raycast(position, _hitListPool, _trackableType);

            if (raycast)
            {
                var hit = _hitListPool.FirstOrDefault().pose;
                hitPosition = hit.position;
            }
            
            Debug.Log($"{raycast}::{position}");
            
            onPointerDownCallback?.Invoke(raycast, hitPosition);
        }
    }
}
