/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBytes.Idle.Production.Resources;
using TinyBytes.Idle.Production.Storage;
using UnityEngine;
using UnityEngine.AI;

namespace TinyBytes.Idle.Production.Workers.Transport
{
	public class TransportWorker : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private ResourceStorage _fromStorage;
		[SerializeField] private ResourceStorage _toStorage;

		[SerializeField] private NavMeshAgent _agent;

        #endregion

        #region Private properties

        private List<TransformableResource> _collectedResources = new List<TransformableResource>();

        #endregion

        #region Public properties

        public TransportWorkerStates State { get; private set; }

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
            Think();
        }

        #region AI

        private void Think()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            switch (State)
            {
                case TransportWorkerStates.Idle:
                    Idle();
                    break;

                case TransportWorkerStates.Go_Pick:
                    GoPick();
                    break;

                case TransportWorkerStates.Pick:
                    Pick();
                    break;

                case TransportWorkerStates.Carry:
                    Carry();
                    break;

                case TransportWorkerStates.Drop:
                    Drop();
                    break;

                default:
                    Idle();
                    break;
            }
        }

        private async void Idle()
        {
            await Task.Delay(1000);
            State = TransportWorkerStates.Go_Pick;

            Think();
        }

        private async void GoPick()
        {
            if (_fromStorage.HasUsedSpace())
            {
                _agent.SetDestination(_fromStorage.transform.position);

                await Task.Delay(500);

                while (_agent.remainingDistance > _agent.stoppingDistance)
                {
                    if (!Application.isPlaying)
                    {
                        return;
                    }

                    await Task.Delay(500);
                }

                State = TransportWorkerStates.Pick;
            }
            else
            {
                State = TransportWorkerStates.Idle;
            }

            Think();
        }

        private async void Pick()
        {
            while (_fromStorage.HasUsedSpace())
            {
                var resource = _fromStorage.Pop();
                resource.gameObject.SetActive(false);
                _collectedResources.Add(resource);
                await Task.Delay(250);
            }

            State = TransportWorkerStates.Carry;

            Think();
        }

        private async void Carry()
        {
            _agent.SetDestination(_toStorage.transform.position);

            await Task.Delay(500);

            while (_agent.remainingDistance > _agent.stoppingDistance)
            {
                await Task.Delay(500);
            }

            State = TransportWorkerStates.Drop;

            Think();
        }

        private async void Drop()
        {
            foreach(var resource in _collectedResources)
            {
                while (!_toStorage.HasEmptySpace())
                {
                    await Task.Delay(500);
                }

                resource.gameObject.SetActive(true);
                _toStorage.Add(resource);

                await Task.Delay(250);
            }

            _collectedResources.Clear();

            State = TransportWorkerStates.Go_Pick;

            Think();
        }

        #endregion

        #endregion

        #region Public methods



        #endregion
    }
}