/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.GameCamera.PlatformInput
{
    public class CameraInputPC : ICameraInput
    {
        #region Private properties

        private float _moveSpeedMultiplier = 50;
        private float _zoomSpeedMultiplier = 5;

        #endregion

        #region ICameraInput

        public CameraInputPC(float moveSpeedMultiplier, float zoomSpeedMultiplier)
        {
            _moveSpeedMultiplier = moveSpeedMultiplier;
            _zoomSpeedMultiplier = zoomSpeedMultiplier;
        }

        public bool IsMoving()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsZooming()
        {
            return true;
        }

        public Vector2 GetAxisXY()
        {
            return new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        }

        public float CalculateZoomMagnitude()
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }

        public float CalculateMoveSpeed(float speedBase, float zoomMultiplier)
        {
            return speedBase * zoomMultiplier * _moveSpeedMultiplier;
        }

        public float CalculateZoom(float zoomMagnitude, float speedBase, float currentSize, Vector2 zoomRangeLimit)
        {
            return Mathf.Clamp(currentSize - zoomMagnitude * speedBase * _zoomSpeedMultiplier, zoomRangeLimit.x, zoomRangeLimit.y);
        }

        #endregion
    }
}