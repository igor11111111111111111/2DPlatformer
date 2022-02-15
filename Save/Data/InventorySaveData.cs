using System;
using System.Collections.Generic;

namespace Platformer2D
{
    [Serializable]
    public class InventorySaveData : ISaveData 
    {
        public List<int> ItemsId;
         
        public InventorySaveData(List<int> itemsId)
        {
            ItemsId = itemsId;
        }
    }
}
