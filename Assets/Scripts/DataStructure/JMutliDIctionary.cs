using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this class not checking about value is duplicated
/// </summary>
/// <typeparam name="KEY_T"></typeparam>
/// <typeparam name="VALUE_T"></typeparam>
public class JMutliDIctionary <KEY_T, VALUE_T> 
{
    Dictionary<KEY_T, List<VALUE_T>> data = null;
    int count = 0;

    public int Count
    {
        get
        {
            return count;
        }
    }

    JMutliDIctionary()
    {
        data = new Dictionary<KEY_T, List<VALUE_T>>();
    }

    public void Add(KEY_T key, VALUE_T value)
    {
        //find list
        List<VALUE_T> foundedList = null;

        if(!data.TryGetValue(key, out foundedList))
        {
            foundedList = new List<VALUE_T>();
            data.Add(key, foundedList);
        }

        foundedList.Add(value);
        count++;
    }

    public void RemoveAt(KEY_T key, int index)
    {
        List<VALUE_T> foundedList = null;

        if(!data.TryGetValue(key, out foundedList))
        {
            Debug.LogError($"{key} is not founded");
            return;
        }

        if(foundedList.Count <= index)
        {
            Debug.LogError($"Out of range : count : {foundedList.Count} index : {index}");
            return;
        }

        foundedList.RemoveAt(index);
        count--;
    }

    public void Remove(KEY_T key, VALUE_T value)
    {
        List<VALUE_T> foundedList = null;

        if(!data.TryGetValue(key, out foundedList))
        {
            Debug.LogError($"{key} is not founded");
            return;
        }

        foundedList.Remove(value);
        count--;
    }

    public List<VALUE_T> GetList(KEY_T key)
    {
        List<VALUE_T> foundedList = null;
        if(!data.TryGetValue(key, out foundedList))
        {
            Debug.LogError($"{key} is not founded");
            return null;
        }

        return foundedList;
    }
}
