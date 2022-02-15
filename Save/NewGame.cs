using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{ 
    public class NewGame : MonoBehaviour
    {
        public GameSaveData Data { get; private set; }
         
        private void Awake()
        {
            Data = new GameSaveData();

            Data.PlayerSaveData = new PlayerSaveData
            (
                new Vector3(32, 7, 0).ToSerializedVector(),
                10,
                10
            );

            Data.BullSaveData = new BullSaveData(true);

            Data.InventoryData = new InventorySaveData
            (
                new List<int> { 1, 1, 0, 1, 1, 1, 0 }
            );

            Data.EnvironmentData = new EnvironmentSaveData
            (
                new List<PrefabSaveData>()
                {
                    new PrefabSaveData(2, new Vector3(28.5f, 4.5f, 0).ToSerializedVector()),
                    new PrefabSaveData(1, new Vector3(15.88f, -3.46f, 0).ToSerializedVector()),
                    new PrefabSaveData(1, new Vector3(11.88f, -3.46f, 0).ToSerializedVector()),
                    new PrefabSaveData(0, new Vector3(12.88f, -3.46f, 0).ToSerializedVector()),
                    new PrefabSaveData(0, new Vector3(8.72f, -3.46f, 0).ToSerializedVector()),
                }
            );

            Data.UnitsAIData = new List<UnitAISaveData>
            {
                new UnitAISaveData("SkeletMelee", new Vector3(40, 7f, 0).ToSerializedVector(), 10)
            };

            Data.FloorSaveData = new List<FloorSaveData>
            {
                new FloorSaveData("BreakableBlock", new Vector3(-3.52f, 6.72f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-3.52f, 7.36f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-3.52f, 8f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-3.52f, 8.64f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-8, 9.28f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-7.36f, 9.28f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-6.72f, 9.28f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-6.08f, 6.08f, 0).ToSerializedVector()),
                new FloorSaveData("BreakableBlock", new Vector3(-5.44f, 6.08f, 0).ToSerializedVector())
            };
        }
    }
}
