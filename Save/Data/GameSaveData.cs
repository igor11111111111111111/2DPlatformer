using System;
using System.Collections.Generic;

namespace Platformer2D
{
    [Serializable]   
    public class GameSaveData : ISaveData
    { 
        public PlayerSaveData PlayerSaveData;
        public BullSaveData BullSaveData;
        public InventorySaveData InventoryData;
        public EnvironmentSaveData EnvironmentData;
        public List<UnitAISaveData> UnitsAIData;
        public List<FloorSaveData> FloorSaveData;
    }
}
