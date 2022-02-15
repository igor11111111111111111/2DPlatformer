using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class BreakableBlockData : FloorData
    {
        [SerializeField] private ContactFilter2D _filter;
        [SerializeField] private List<BreakableBlockData> _nearBlocks = new List<BreakableBlockData>();
        [SerializeField] private GameObject _breakableBricks;
        [SerializeField] private CircleCollider2D _finderCollider;
        private SpriteRenderer _renderer;
        private BoxCollider2D _boxCollider;
        private float _delay = 0.2f;

        protected void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public override void InitFloorOnScene(FloorOnScene floorOnScene)
        {
            base.InitFloorOnScene(floorOnScene);
            _floorOnScene.OnComplete += FindNearBlock;
        }

        protected void Start()
        {
            if (_floorOnScene == null)
                FindNearBlock();
        }

        private void FindNearBlock()
        {
            List<Collider2D> results = new List<Collider2D>();
            _finderCollider.OverlapCollider(_filter, results);
            foreach (var result in results)
            {
                result.TryGetComponent(out BreakableBlockData block);
                if (block)
                {
                    _nearBlocks.Add(block);
                }
            }
            if(_floorOnScene != null)
                _floorOnScene.OnComplete -= FindNearBlock;
        }

        public void DestroyBlock()
        {
            //Destroy(_climbCollider);
            if (!_renderer.enabled) return;
            _renderer.enabled = false;
            _boxCollider.enabled = false;
            var go = Instantiate(_breakableBricks, transform.position, Quaternion.identity);
            go.transform.SetParent(transform);
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            for (int i = 0; i < _nearBlocks.Count - 1; i++)
            {
                try
                {
                    _nearBlocks[i].Remove(this);
                    _nearBlocks[i].DestroyBlock();
                }
                catch (System.ArgumentOutOfRangeException)
                {
                }
                yield return new WaitForSeconds(_delay);
            }
        }

        public void Remove(BreakableBlockData block)
        {
            _nearBlocks.Remove(block);
        }
    }
}
