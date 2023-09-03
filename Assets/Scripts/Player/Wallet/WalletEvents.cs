using System;

namespace TinyBytes.Idle.Player.Wallet
{
    public static class WalletEvents
    {
        public static Action OnServiceLoaded;

        public static Action<ResourceType, long> OnResourceUpdated;
    }
}