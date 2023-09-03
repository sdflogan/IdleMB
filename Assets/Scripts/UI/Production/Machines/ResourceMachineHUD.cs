/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.Production.Machines;
using UnityEngine;
using UnityEngine.UI;

namespace TinyBytes.Idle.UI.Production.Machines
{
	public class ResourceMachineHUD : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private Image _fill;
        [SerializeField] private ResourceMachine _resourceMachine;

		#endregion

		#region Private properties

        private float _operatingTotalSeconds = 0;

        #endregion

        #region Public properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            _resourceMachine.OnOperatingStarted += OnOperatingStarted;
            _resourceMachine.OnOperating += OnOperating;
            _resourceMachine.OnOperatingFinished += OnOperatingFinished;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _resourceMachine.OnOperatingStarted -= OnOperatingStarted;
            _resourceMachine.OnOperating -= OnOperating;
            _resourceMachine.OnOperatingFinished -= OnOperatingFinished;
        }

        #endregion

        #region Private methods

        #region Callbacks

        private void OnOperatingStarted(float remainingSeconds)
        {
            _fill.fillAmount = 0;
            _operatingTotalSeconds = remainingSeconds;

            gameObject.SetActive(true);
        }

        private void OnOperating(float remainingSeconds)
        {
            _fill.fillAmount = (1 - (remainingSeconds / _operatingTotalSeconds));
        }

        private void OnOperatingFinished()
        {
            _fill.fillAmount = 1;
            gameObject.SetActive(false);
        }

        #endregion

        #endregion

        #region Public methods



        #endregion
    }
}