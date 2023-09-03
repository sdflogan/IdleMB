/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections.Generic;
using TinyBytes.Idle.Production.Crops;
using UnityEngine;

namespace TinyBytes.Idle.Production.Resources
{
	public class TransformableResource : MonoBehaviour
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private List<GameObject> _models;

		#endregion

		#region Private properties


		
		#endregion
		
		#region Public properties
		
		public int Level { get; private set; }
		public int ResourceBaseValue { get; private set; }

        #endregion

        #region Unity Events

        private void Awake()
        {
			Level = 0;
			foreach(var model in _models)
            {
				model.SetActive(false);
            }

			_models[0].SetActive(true);
        }

        #endregion

        #region Private methods

		private void UpdateCurrentModel()
        {
			_models[Level - 1].SetActive(false);
			_models[Level].SetActive(true);
        }

        #endregion

        #region Public methods

		public void Increase()
        {
			if (Level < _models.Count)
            {
				Level++;
				UpdateCurrentModel();
            }
        }

		public void Init(CropResource cropResource)
        {
			ResourceBaseValue = cropResource.ResourceValue;
        }

		public long GetCurrentValue()
        {
			return ResourceBaseValue * (Level+1);
        }

        #endregion
    }
}