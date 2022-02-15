using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer2D
{
    public interface IUseDirectionChanged
    {
        UnityAction<float> OnDirectionXchanged { get; set; }
    }
}
