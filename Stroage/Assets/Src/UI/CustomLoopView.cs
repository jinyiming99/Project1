using System;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class CustomLoopView : DropComponentBase
    {
        private class NodeData
        {
            public int Index;
            public Vector2 Pos;
            public bool IsUsed;
            public GameObject Item;
            //public 
            public Action<GameObject> action;
        }
        
        private ScrollRect _scrollRect;
        private RectTransform _content;
        [Header("宽间隔")]
        [SerializeField]
        private float _widthSpacing = 10;
        
        [Header("高间隔")]
        [SerializeField]
        private float _hightSpacing = 10;
        
        [Header("对象宽度")]
        [SerializeField]
        private float _itemWidth = 100;
        
        [Header("对象高度")]
        [SerializeField]
        private float _itemHeight = 100;
        
        [Header("边缘")]
        [SerializeField]
        private Rect _edge = new Rect(0,0,0,0);
        [Header("单列数量")]
        [SerializeField]
        [Min(1)]
        private int _column = 1;
        
        [Header("物品")]
        [SerializeField]
        private GameObject _item;
        
        private GameObjectPool _pool = new GameObjectPool();

        private Action<int, int> _action;
        
        private List<NodeData> _nodeDatas = new List<NodeData>();
        
        private PositionContainer _container ;
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener((v) =>
            {
                Refresh();
            });
            _container = new PositionContainer((int)_itemWidth,(int)_itemHeight,(int)_widthSpacing,(int)_hightSpacing,_column);
            if (_content != null)
                _content.pivot = new Vector2(0,1);
            _item.gameObject.SetActive(false);
            _content = _scrollRect.content;
            _content.anchorMin = new Vector2(0,1);
            _content.anchorMax = new Vector2(0,1);
            _content.pivot = new Vector2(0,1);
            
        }
        

        private void Refresh()
        {
            int startIndex = 0;
            
            if (_scrollRect.vertical)
                startIndex = Mathf.FloorToInt((_content.anchoredPosition.y - _edge.yMin) / (_itemHeight + _hightSpacing)) * _column;
            else
                startIndex = Mathf.FloorToInt((-_content.anchoredPosition.x - _edge.xMin) / (_itemWidth + _widthSpacing)) * _column;
            int endIndex = 0;
            RectTransform rectTransform = _scrollRect.transform as RectTransform;
            if (_scrollRect.vertical)
                endIndex = Mathf.CeilToInt((_content.anchoredPosition.y + rectTransform.sizeDelta.y)/ (_itemHeight + _hightSpacing)) * _column;
            else
                endIndex = Mathf.CeilToInt((-_content.anchoredPosition.x + rectTransform.sizeDelta.x)/ (_itemWidth + _widthSpacing)) * _column;
            
            for (int i = 0 ; i < _nodeDatas.Count; ++i)
            {
                if (_nodeDatas[i].IsUsed)
                {
                    if (i < startIndex || i >= endIndex)
                    {
                        _nodeDatas[i].IsUsed = false;
                        ReleaseGameObject(_nodeDatas[i].Item);
                        _nodeDatas[i].Item = null;
                    }
                }
                else
                {
                    if (i >= startIndex && i < endIndex)
                    {
                        _nodeDatas[i].IsUsed = true;
                        var item = GetGameObject();
                        var rect = item.transform as RectTransform;
                        _nodeDatas[i].Item = item;
                        _nodeDatas[i].Item.transform.SetParent(_content);
                        _nodeDatas[i].Item.transform.localPosition = _nodeDatas[i].Pos + rect.pivot * new Vector2(_itemWidth,-_itemHeight); 
                        _nodeDatas[i].Item.transform.localScale = Vector3.one;
                        _nodeDatas[i].Item.transform.localRotation = Quaternion.identity;
                        var data = _nodeDatas[i].Item;
                        _nodeDatas[i].action?.Invoke(data);
                    }
                }
            }
        }
        
        private GameObject GetGameObject()
        {
            var go = _pool.Pop();
            if (go == null)
            {
                go = GameObject.Instantiate(_item);
                go.SetActive(true);
            }
            return go;
        }
        
        private void ReleaseGameObject(GameObject go)
        {
            _pool.Push(go);
        }

        public void SetData<T>(List<T> list)
        {
            Profiler.BeginSample("setdata");
            
            _container.Calculate(list.Count,!_scrollRect.horizontal);
            var size = new Vector2(_container.ContainerWidth + _edge.xMin + _edge.xMax,_container.ContainerHight+ _edge.yMin + _edge.yMax);
            var rect = _scrollRect.transform as RectTransform;
            _content.sizeDelta = new Vector2(Mathf.Max(size.x, rect.sizeDelta.x), Mathf.Max(size.y, rect.sizeDelta.y));
            for (int i = 0; i < list.Count; ++i)
            {
                Profiler.BeginSample("new");
                var node = new NodeData()
                {
                    Index = i,
                    Pos = _container.GetPos(i) + new Vector2(_edge.xMin, -_edge.yMin),
                    IsUsed = false,
                };
                Profiler.EndSample();
                Profiler.BeginSample("action");
                node.action = (go) =>
                {
                    var com = go.GetComponent<ICustomLoopNode<T>>();
                    com.SetData(list[node.Index]);
                };
                Profiler.EndSample();
                _nodeDatas.Add(node);
            }
            Profiler.BeginSample("Refresh");
            Refresh();
            Profiler.EndSample();
            Profiler.EndSample();
        }

        // private static void SetData<T>(GameObject go)
        // {
        //     var com = go.GetComponent<ICustomLoopNode<T>>();
        //     com.SetData(list[node.Index]);
        // }
    }
}