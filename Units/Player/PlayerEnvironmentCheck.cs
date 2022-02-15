using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer2D
{
    public class PlayerEnvironmentCheck : MonoBehaviour
    {
        public static PlayerEnvironmentCheck Instance;
        [SerializeField] private ContactFilter2D _environmentFilter;
        private BoxCollider2D _collider;

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            Instance = null;
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        public Class TryGet<Class>() where Class : class
        {
            List<Collider2D> results = new List<Collider2D>();
            _collider.OverlapCollider(_environmentFilter, results);
            foreach (var result in results)
            {
                result.TryGetComponent(out Class c);
                if (c != null)
                {
                    return c;
                }
            }
            return null;
        }
    }
}

