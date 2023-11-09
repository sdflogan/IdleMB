/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.GameCamera.PlatformInput
{
	public interface ICameraInput
	{
		bool IsMoving();

		bool IsZooming();

		Vector2 GetAxisXY();

		float CalculateZoomMagnitude();

		float CalculateMoveSpeed(float speedBase, float zoomMultiplier);

		float CalculateZoom(float zoomMagnitude, float speedBase, float currentSize, Vector2 zoomRangeLimit);
	}
}