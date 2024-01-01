
using System;

namespace TinyBytes.Idle.Production.Machines
{
    [Serializable]
    public class MachineData
    {
        public int EarningLevel { get; private set; }
        public int SpeedLevel { get; private set; }

        public MachineData()
        {
            EarningLevel = 0;
            SpeedLevel = 0;
        }
    }
}