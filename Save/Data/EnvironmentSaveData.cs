using System;
using System.Collections.Generic;

namespace Platformer2D
{
    [Serializable]
    public class EnvironmentSaveData : ISaveData
    {
        public List<PrefabSaveData> PrefabData;

        public EnvironmentSaveData(List<PrefabSaveData> prefabData)
        {
            PrefabData = prefabData;
        }
    }
}
