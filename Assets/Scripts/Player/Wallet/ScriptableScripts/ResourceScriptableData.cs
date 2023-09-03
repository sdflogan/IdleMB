/*
    All rights are reserved. Unauthorized copying of this file, via any medium is strictly
    prohibited. Proprietary and confidential.
	
	Author: Dani S.
*/

using System;
using TinyBytes.Idle.Player.Wallet;
using UnityEngine;

namespace TinyBytes.Idle.Scriptable.Wallet
{
	[Serializable]
	public struct ResourceScriptableData
	{
		public ResourceType type;
		public Sprite icon;
	}
}