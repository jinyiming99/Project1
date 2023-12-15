
using System.Collections.Generic;

public class LinkList_LowGC<T>
{
    LinkedList<T> _data = new LinkedList<T>();
    Stack<LinkedListNode<T>> _cache = new Stack<LinkedListNode<T>>();
    public int Count => _data.Count;
    public T First => _data.First == null ? default(T):_data.First.Value;
    public T Last => _data.Last == null ? default(T):_data.Last.Value;
    
    private int _maxCount = 0;
    public int MaxCount
    {
        get => _maxCount;
        set
        {
            _maxCount = value;
            while (_cache.Count > _maxCount)
            {
                _cache.Pop();
            }
        }
    }
    public LinkList_LowGC(int maxCount)
    {
        _maxCount = maxCount;
    }
    
    public void AddLast(T value)
    {
        if (Contains(value))
            return;
        LinkedListNode<T> node;
        if (_cache.Count > 0)
        {
            node = _cache.Pop();
            node.Value = value;
        }
        else
        {
            node = new LinkedListNode<T>(value);
        }
        _data.AddLast(node);
    }
    
    public bool Contains(T value)
    {
        return _data.Contains(value);
    }
    
    public void Remove(T value)
    {
        var node = _data.Find(value);
        _data.Remove(node);
        if (_cache.Count < _maxCount)
            _cache.Push(node);
    }
    
    public void Clear()
    {
        _data.Clear();
        _cache.Clear();
    }
}
