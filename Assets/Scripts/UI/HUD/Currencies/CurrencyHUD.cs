/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using TinyBytes.Idle;
using TinyBytes.Idle.Player.Wallet;
using TinyBytes.Utils.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinyBytes.Idle.UI.HUD
{
	public class CurrencyHUD : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private ResourceType _resource;
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _value;

        #endregion

        #region Private properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            WalletEvents.OnResourceUpdated += OnResourceUpdated;
        }

        private void OnDestroy()
        {
            WalletEvents.OnResourceUpdated -= OnResourceUpdated;
        }

        private void Start()
        {
            Init();
        }

        #endregion

        #region Public properties



        #endregion

        #region Private methods

        private void OnResourceUpdated(ResourceType resource, long value)
        {
            if (_resource == resource)
            {
                _value.text = value.ToAlphabet();
            }
        }

        private void Init()
        {
            _icon.sprite = AssetDataService.Instance.WalletResourcesData.GetIcon(_resource);
        }

        #endregion

        #region Public methods



        #endregion
    }
}