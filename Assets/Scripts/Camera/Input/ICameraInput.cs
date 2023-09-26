/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.GameCamera.Input
{
	public interface ICameraInput
	{
		Vector2 GetTouchPosition();

		float GetZoomVariation();
	}
}