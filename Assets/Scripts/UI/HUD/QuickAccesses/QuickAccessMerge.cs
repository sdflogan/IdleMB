/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Mauricio Perez (Peche)
    Date: Friday 8th, December 2023
*/

using TinyBytes.Idle.GameCamera;
using UnityEngine;
using UnityEngine.UI;

namespace TinyBytes.Idle.UI
{
    public class QuickAccessMerge : MonoBehaviour
    {
		#region Inspector

		[SerializeField] private Button _btnAccess = default;
		[SerializeField] private ButtonAnimation _buttonAnimator = default;
		[SerializeField] private Vector3 _cameraGoal = default;

		#endregion

		#region Unity events

		private void OnEnable()
		{
			_btnAccess.onClick.AddListener(Access);
		}

		private void OnDisable()
		{
			_btnAccess.onClick.RemoveAllListeners();
		}

		#endregion

		#region Private methods

		private void Access()
		{
			// TODO: SFX

			_buttonAnimator?.Play();

			Debug.LogError("<color=orange>MergeAccess</color> -> move camera to merge area");

			GameplayCameraService.OnInstantMovement?.Invoke(_cameraGoal);
		}

		#endregion
	}
}