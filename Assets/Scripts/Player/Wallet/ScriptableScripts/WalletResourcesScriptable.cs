/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System.Collections.Generic;
using TinyBytes.Idle.Player.Wallet;
using UnityEngine;

namespace TinyBytes.Idle.Scriptable.Wallet
{
	[CreateAssetMenu(fileName = "WalletResources", menuName = "ScriptableObjects/Wallet/WalletResourcesObject")]
	public class WalletResourcesScriptable : ScriptableObject
	{
		#region Events



		#endregion

		#region Inspector properties

		[SerializeField] private List<ResourceScriptableData> _resources;
		[SerializeField] private Sprite _defaultSprite;
		
		#endregion
		
		#region Private properties
		
		
		
		#endregion
		
		#region Public properties
	

		
		#endregion
		
		#region Private methods
		
		
		
		#endregion
		
		#region Public methods
		
		public Sprite GetIcon(ResourceType resource)
        {
			var resourceData = _resources.Find((element) => element.type == resource);

			return (resourceData.type != ResourceType.Undefined ? resourceData.icon : _defaultSprite);
        }
		
		#endregion
	}
}