﻿using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ItemsObjectPool : MonoBehaviour
    {
        //[SerializeField] private ItemScenePresenter _itemPresenterTemplate;
        //[SerializeField] private Transform _parent;
        //private List<ItemScenePresenter> _available = new List<ItemScenePresenter>();
        //private List<ItemScenePresenter> _inUse = new List<ItemScenePresenter>();

        //private void Awake()
        //{
        //    Debug.Log(transform.name);
        //}
        //public ItemScenePresenter Get(IItem item)
        //{
        //    ItemScenePresenter presenter = null;

        //    if (_available.Count == 0)
        //    {
        //        presenter = Instantiate(_itemPresenterTemplate, _parent);
        //        presenter.Present(item);
        //        presenter.PickedUp += () => Release(presenter);
        //    }
        //    else
        //    {
        //        presenter = _available[0];
        //        _available.Remove(presenter);
        //    }

        //    _inUse.Add(presenter);
        //    return presenter;
        //}

        //public void Release(ItemScenePresenter presenter)
        //{
        //    if (_inUse.Remove(presenter) == false)
        //        return;

        //    presenter.gameObject.SetActive(false);
        //    _available.Add(presenter);
        //}
    }
}