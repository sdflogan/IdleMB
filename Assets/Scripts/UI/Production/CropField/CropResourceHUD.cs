/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle.Production.Crops;
using UnityEngine;
using UnityEngine.UI;

namespace TinyBytes.Idle.UI.Production.Crops
{
	public class CropResourceHUD : MonoBehaviour
	{
        #region Events



        #endregion

        #region Inspector properties

        [SerializeField] private Image _fill;

		#endregion

		#region Private properties

		private CropResource _resource;
        private float _collectTotalSeconds = 0;

        #endregion

        #region Public properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            _resource = GetComponentInParent<CropResource>();
            _resource.OnCollectStarted += OnCollectStarted;
            _resource.OnCollecting += OnCollecting;
            _resource.OnCollectEnded += OnCollectEnded;

            gameObject.SetActive(false);
        }

        #endregion

        #region Private methods

        #region Callbacks

        private void OnCollectStarted(float remainingSeconds)
        {
            _fill.fillAmount = 0;
            _collectTotalSeconds = remainingSeconds;

            gameObject.SetActive(true);
        }

        private void OnCollecting(float remainingSeconds)
        {
            _fill.fillAmount = (1 - (remainingSeconds / _collectTotalSeconds));
        }

        private void OnCollectEnded()
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