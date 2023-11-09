/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.GameCamera.PlatformInput
{
	public class CameraInputMobile : ICameraInput
	{
        #region Private properties

        private float _moveSpeedMultiplier = 0.5f;
        private float _zoomSpeedMultiplier = 0.005f;

        #endregion

        #region ICameraInput

        public CameraInputMobile(float moveSpeedMultiplier, float zoomSpeedMultiplier)
        {
            _moveSpeedMultiplier = moveSpeedMultiplier;
            _zoomSpeedMultiplier = zoomSpeedMultiplier;
        }

        public bool IsMoving()
        {
            return (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved);
        }

        public bool IsZooming()
        {
            return (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved);
        }

        public Vector2 GetAxisXY()
        {
            var touch = Input.GetTouch(0);

            return new Vector2(-touch.deltaPosition.x, -touch.deltaPosition.y);
        }

        public float CalculateZoomMagnitude()
        {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            Vector2 prevTouch0Pos = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1Pos = touch1.position - touch1.deltaPosition;

            float prevTouchDeltaMag = (prevTouch0Pos - prevTouch1Pos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            return prevTouchDeltaMag - touchDeltaMag;
        }

        public float CalculateMoveSpeed(float speedBase, float zoomMultiplier)
        {
            return speedBase * zoomMultiplier * _moveSpeedMultiplier;
        }

        public float CalculateZoom(float zoomMagnitude, float speedBase, float currentSize, Vector2 zoomRangeLimit)
        {
            return Mathf.Clamp(currentSize + zoomMagnitude * speedBase * _zoomSpeedMultiplier, zoomRangeLimit.x, zoomRangeLimit.y);
        }

        #endregion
    }
}