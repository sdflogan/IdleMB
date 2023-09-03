/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using TinyBytes.Idle.Production.Crops;
using UnityEngine;
using UnityEngine.AI;

namespace TinyBytes.Idle.Production.Workers.Crop
{
	public class CropWorker : MonoBehaviour
	{
        #region Events



        #endregion

        #region Inspector properties

        [SerializeField] private NavMeshAgent _agent;

		#endregion

		#region Private properties

		private CropField _cropField;
        private CropResource _targetCrop;


        #endregion

        #region Public properties

        public CropWorkerStates State { get; private set; }

        #endregion

        #region Private methods

        #region AI

        private void Think()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            switch (State)
            {
                case CropWorkerStates.Idle:
                    Idle();
                    break;

                case CropWorkerStates.Search:
                    Search();
                    break;

                case CropWorkerStates.Move:
                    Move();
                    break;

                case CropWorkerStates.Collect:
                    Collect();
                    break;

                default:
                    Idle();
                    break;
            }
        }

        private async void Idle()
        {
            await Task.Delay(1000);
            State = CropWorkerStates.Search;

            Think();
        }

        private void Search()
        {
            var availableCrops = _cropField.GetAvailableCrops();

            var closesCrop = FindClosest(availableCrops);

            if (closesCrop == null)
            {
                State = CropWorkerStates.Idle;
            }
            else
            {
                _targetCrop = closesCrop;
                _targetCrop.AssignWorker();
                _agent.SetDestination(_targetCrop.transform.position);
                State = CropWorkerStates.Move;
            }

            Think();
        }

        private async void Move()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                State = CropWorkerStates.Collect;
            }

            await Task.Delay(1000);

            Think();
        }

        private void Collect()
        {
            _targetCrop.StartCollecting(OnCollectFinish);
        }

        #endregion

        private CropResource FindClosest(List<CropResource> crops)
        {
            float closestDistance = float.MaxValue;
            CropResource closestCrop = null;

            if (crops.Count > 0)
            {
                foreach (var crop in crops)
                {
                    var distance = Vector3.Distance(transform.position, crop.transform.position);

                    if (distance < closestDistance)
                    {
                        closestCrop = crop;
                        closestDistance = distance;
                    }
                }
            }

            return closestCrop;
        }

        private void OnCollectFinish()
        {
            State = CropWorkerStates.Search;
            Think();
        }

        private void OnGameLoaded()
        {
            Think();
        }

        #endregion

        #region Unity Events

        private void Awake()
        {
            _cropField = FindObjectOfType<CropField>();

            MainEvents.OnGameLoaded += OnGameLoaded;
        }

        private void OnDestroy()
        {
            MainEvents.OnGameLoaded -= OnGameLoaded;
        }

        #endregion

        #region Public methods



        #endregion
    }
}