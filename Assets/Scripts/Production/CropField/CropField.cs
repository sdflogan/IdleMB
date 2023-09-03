/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections.Generic;
using TinyBytes.Idle.Production.Resources;
using TinyBytes.Idle.Production.Storage;
using TinyBytes.Utils;
using UnityEngine;

namespace TinyBytes.Idle.Production.Crops
{
	public class CropField : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private int _rows;
		[SerializeField] private int _cols;

        [SerializeField] private float _size;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;

		[SerializeField] private CropResource _cropResourcePrefab;
		[SerializeField] private TransformableResource _transformableResourcePrefab;
        [SerializeField] private ResourceStorage _storage;

        #endregion

        #region Private properties

        private CropResource[][] _crops;

        #endregion

        #region Public properties

        public ResourceStorage Storage => _storage;

        #endregion

        #region Unity Events

        private void Awake()
        {
            GenerateResources();
        }

        private void OnDrawGizmos()
        {
            var UpLeftVertex = transform.TransformPoint(Vector3.zero);
            var DownLeftVertex = transform.TransformPoint(new Vector3((_rows - 1) * _offsetX, 0, 0));

            var UpRightVertex = transform.TransformPoint(new Vector3(0, 0, (_cols - 1) * _offsetY));
            var DownRightVertex = transform.TransformPoint(new Vector3((_rows - 1) * _offsetX, 0, (_cols - 1) * _offsetY));

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(UpLeftVertex, DownLeftVertex);
            Gizmos.DrawLine(UpLeftVertex, UpRightVertex);
            Gizmos.DrawLine(DownRightVertex, UpRightVertex);
            Gizmos.DrawLine(DownRightVertex, DownLeftVertex);

            Gizmos.DrawWireSphere(transform.position, 1f);
        }

        private void OnDestroy()
        {
            for (int i=0; i<_rows; i++)
            {
                for (int j=0; j<_cols; j++)
                {
                    _crops[i][j].OnCollectEnded -= OnResourceCollected;
                }
            }
        }

        #endregion

        #region Private methods

        private void GenerateResources()
        {
            _crops = new CropResource[_rows][];
            
            for (int i = 0; i < _rows; i++)
            {
                _crops[i] = new CropResource[_cols];

                for (int j = 0; j < _cols; j++)
                {
                    var cropResource = Instantiate(_cropResourcePrefab, transform);
                    cropResource.Init(i, j, _size, _offsetX, _offsetY, this);
                    cropResource.OnCollectEnded += OnResourceCollected;
                    _crops[i][j] = cropResource;
                }
            }
        }

        private void OnResourceCollected(CropResource resource)
        {
            var resourceInstance = SimplePool.Spawn(_transformableResourcePrefab);
            resourceInstance.Init(resource);
            Storage.Add(resourceInstance);
        }

        #endregion

        #region Public methods

        public List<CropResource> GetAvailableCrops()
        {
            var availableCrops = new List<CropResource>();

            for (int i=0; i<_rows; i++)
            {
                for (int j=0; j<_cols; j++)
                {
                    if (_crops[i][j].IsAvailableToCollect())
                    {
                        availableCrops.Add(_crops[i][j]);
                    }
                }
            }

            return availableCrops;
        }

        

        #endregion
    }
}