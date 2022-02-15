using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public abstract class RaycastFind : Find
    {
        protected ContactFilter2D _filter;
        protected List<RaycastHit2D> _allResults;

        protected RaycastFind(StateData stateData) : base(stateData)
        {
        }
    }
}
