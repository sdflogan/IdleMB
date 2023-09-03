/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using UnityEngine;

namespace TinyBytes.Idle.Production.Storage
{
	public class ResourceStorage : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private int _rows;
		[SerializeField] private int _cols;

		[SerializeField] private float _size;
		[SerializeField] private float _offsetX;
		[SerializeField] private float _offsetY;

		[SerializeField] private StorageSlot _storageSlotPrefab;

		#endregion

		#region Private properties

		private StorageSlot[][] _slots;

        #endregion

        #region Public properties



        #endregion

        #region Unity Events

        private void Awake()
        {
            LoadSlots();
        }

        private void OnDrawGizmos()
        {
            var UpLeftVertex = transform.TransformPoint(Vector3.zero);
            var DownLeftVertex = transform.TransformPoint(new Vector3(0, 0, (_rows) * _offsetX));

            var UpRightVertex = transform.TransformPoint(new Vector3(0, (_cols) * _offsetY, 0));
            var DownRightVertex = transform.TransformPoint(new Vector3(0, (_cols) * _offsetY, (_rows) * _offsetX));

            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(UpLeftVertex, DownLeftVertex);
            Gizmos.DrawLine(UpLeftVertex, UpRightVertex);
            Gizmos.DrawLine(DownRightVertex, UpRightVertex);
            Gizmos.DrawLine(DownRightVertex, DownLeftVertex);

            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }

        #endregion

        #region Private methods

        private void LoadSlots()
        {
			_slots = new StorageSlot[_rows][];

			for (int i=0; i<_rows; i++)
            {
				_slots[i] = new StorageSlot[_cols];

				for (int j=0; j<_cols; j++)
                {
					var storageSlot = Instantiate(_storageSlotPrefab, transform);
                    storageSlot.Init(i, j, _size, _offsetX, _offsetY);
                    _slots[i][j] = storageSlot;
                }
            }
        }

        private StorageSlot GetAvailableSlot()
        {
            for (int i=0; i<_rows; i++)
            {
                for (int j=0; j<_cols; j++)
                {
                    var slot = _slots[i][j];

                    if (slot.IsAvailable)
                    {
                        return slot;
                    }
                }
            }

            return null;
        }

        private StorageSlot GetLastUsedSlot()
        {
            //for (int i=_rows-1)
        }

        #endregion

        #region Public methods

        public bool HasEmptySpace()
        {
            for (int i=0; i<_rows; i++)
            {
                for (int j=0; j<_cols; j++)
                {
                    if (_slots[i][j].IsAvailable)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Add(GameObject collectedResource)
        {
            var availableSlot = GetAvailableSlot();

            if (availableSlot != null)
            {
                availableSlot.Fill(collectedResource);
            }
        }

        public GameObject Pop()
        {

        }

        #endregion
    }
}