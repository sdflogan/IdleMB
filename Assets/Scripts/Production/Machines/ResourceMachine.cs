/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System;
using System.Threading.Tasks;
using TinyBytes.Idle.Player;
using TinyBytes.Idle.Production.Resources;
using TinyBytes.Idle.Production.Storage;
using UnityEngine;

namespace TinyBytes.Idle.Production.Machines
{
	public class ResourceMachine : MonoBehaviour
	{
        #region Events

        public event Action<float> OnOperatingStarted;
        public event Action<float> OnOperating;
        public event Action OnOperatingFinished;

		#endregion

		#region Inspector properties

		[SerializeField] private float _resourceValueMultiplierBase = 1.5f;
		[SerializeField] private float _operatingSeconds = 10f;

		[SerializeField] private ResourceStorage _inStorage;
		[SerializeField] private ResourceStorage _outStorage;

        #endregion

        #region Private properties

        private TransformableResource _operatingResource = null;

        #endregion

        #region Public properties

        public ResourceMachineState State { get; private set; }
        public MachineData Data { get; private set; }

        #endregion

        #region Unity Events

        private void Awake()
        {
            MainEvents.OnGameLoaded += OnGameLoaded;
        }

        private void OnDestroy()
        {
            MainEvents.OnGameLoaded -= OnGameLoaded;
        }

        #endregion

        #region Private methods

        private void OnGameLoaded()
        {
            // Load Saved Data
            Data = new MachineData();
            Think();
        }

        private void ResourceProcessed()
        {
            PlayerServices.Instance.Wallet.Earn(Player.Wallet.ResourceType.SC, CalculateEarnedValue(_operatingResource));
            _operatingResource.Increase();
            _operatingResource.gameObject.SetActive(true);
            _outStorage.Add(_operatingResource);
            _operatingResource = null;

            OnOperatingFinished?.Invoke();
        }

        private long CalculateEarnedValue(TransformableResource resource)
        {
            // TODO: WORK ON A BETTER FORMULA HERE
            var levelMultiplier = Data.EarningLevel * 0.1f;

            return (long) (resource.GetCurrentValue() * _resourceValueMultiplierBase * levelMultiplier);
        }

        private float CalculateOperatingSeconds()
        {
            // TODO: WORK ON A BETTER FORMULA HERE
            var speedMultiplier = Data.SpeedLevel * 0.01f;

            return Math.Min(1, _operatingSeconds - _operatingSeconds * speedMultiplier);
        }

        #region AI

        private void Think()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            switch(State)
            {
                case ResourceMachineState.Idle:
                    Idle();
                    break;

                case ResourceMachineState.Operating:
                    Operating();
                    break;

                case ResourceMachineState.Finish:
                    Finish();
                    break;

                default:
                    Idle();
                    break;
            }
        }

        private async void Idle()
        {
            await Task.Delay(1000);
            State = ResourceMachineState.Operating;

            Think();
        }

        private async void Operating()
        {
            if (_inStorage.HasUsedSpace() && _outStorage.HasEmptySpace())
            {
                float remainingSeconds = CalculateOperatingSeconds();

                OnOperatingStarted?.Invoke(remainingSeconds);

                _operatingResource = _inStorage.Pop();
                _operatingResource.gameObject.SetActive(false);

                while (remainingSeconds > 0)
                {
                    await Task.Delay(500);
                    remainingSeconds -= 0.5f;

                    OnOperating?.Invoke(remainingSeconds);
                }

                State = ResourceMachineState.Finish;
            }
            else
            {
                State = ResourceMachineState.Idle;
            }

            Think();
        }

        private void Finish()
        {
            ResourceProcessed();

            State = ResourceMachineState.Operating;

            Think();
        }

        #endregion

        #endregion

        #region Public methods



        #endregion
    }
}