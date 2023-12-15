
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 简单的容器，用于存放小物件
/// </summary>
public class ObjectSmallContainer 
{

    List<GameObject> mObjects = new();

    int mCount = 0;

    /// <summary>
    /// 设置容器数量
    /// </summary>
    /// <param name="number"></param>
    public virtual void SetContainerNumber(GameObject target ,int number)
    {
        if (target == null)
            return;
        mCount = number;
        var needCreateHeader = number - mObjects.Count;
        for (int i = 0; i < needCreateHeader; i++)
        {
            var item = GameObject.Instantiate(target,target.transform.parent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            mObjects.Add(item);
        }
        for (int i = 0; i < mObjects.Count; i++)
        {
            mObjects[i].SetActive(i < number);
        }
    }
    /// <summary>
    /// 获取指定位置的物件
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetItem<T>(int index) where T : Component
    {
        if (index < 0 || index >= mObjects.Count)
            return null;
        return mObjects[index].GetComponent<T>();
    }
    
    public int Count => mCount;
}
