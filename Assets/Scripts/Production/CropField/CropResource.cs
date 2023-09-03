/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System;
using System.Collections;
using TinyBytes.Idle.Player;
using UnityEngine;

namespace TinyBytes.Idle.Production.Crops
{
	public class CropResource : MonoBehaviour
	{
		#region Events

		public Action<float> OnCollectStarted;
		public Action<float> OnCollecting;
		public Action OnCollectEnded;


		#endregion

		#region Inspector properties

		[SerializeField] private GameObject _idleState;
		[SerializeField] private GameObject _emptyState;

		#endregion

		#region Private properties

		private float _collectionSeconds = 10f;
		private int _resourceValue = 25;
		private CropField _cropField;
		
		#endregion
		
		#region Public properties
		
		public int Row { get; private set; }
		public int Col { get; private set; }

		public CropResourceState State { get; private set; }

		public bool Free { get; private set; }
		
		#endregion
		
		#region Private methods
		
		private IEnumerator CollectCoroutine(Action onFinish)
        {
			float remainingSeconds = _collectionSeconds;

			while (remainingSeconds > 0)
            {
				yield return new WaitForSeconds(0.5f);
				remainingSeconds -= .5f;

				OnCollecting?.Invoke(remainingSeconds);
            }

			while (!_cropField.Storage.HasEmptySpace())
			{
				yield return new WaitForSeconds(0.5f);
			}

			UpdateState(CropResourceState.Empty);
			ResourceCollected();
			
			onFinish?.Invoke();
        }

		private void ResourceCollected()
        {
			PlayerServices.Instance.Wallet.Earn(Player.Wallet.ResourceType.SC, _resourceValue);
			OnCollectEnded?.Invoke();
		}

		private void UpdateState(CropResourceState state)
        {
			this.State = state;

			switch(State)
            {
				case CropResourceState.Idle:
					_idleState.SetActive(true);
					_emptyState.SetActive(false);
					break;

				case CropResourceState.Empty:
					_idleState.SetActive(false);
					_emptyState.SetActive(true);
					break;
            }
        }
		
		#endregion
		
		#region Public methods
		
		public void Init(int row, int col, float size, float offsetX, float offsetY, CropField cropField)
        {
			this.Row = row;
			this.Col = col;
			this._cropField = cropField;

			transform.localPosition = new Vector3(row*size*offsetX, 0, col*size*offsetY);
			UpdateState(CropResourceState.Idle);
        }

		public bool IsAvailableToCollect()
        {
			return State == CropResourceState.Idle && !Free;
        }

		public void AssignWorker()
        {
			this.Free = false;
        }

		public void FreeWorker()
        {
			this.Free = true;
        }

		public void StartCollecting(Action onFinish)
        {
			OnCollectStarted?.Invoke(_collectionSeconds);
			StartCoroutine(CollectCoroutine(onFinish));
        }
		
		#endregion
	}
}