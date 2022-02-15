using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PickableObject : UseOnTriggerText, IPickable
    {  
        public int Id { get; set; }
        public PrefabSaveData PrefabData { get; set; }
        protected override string _text => "E";
    }
}
