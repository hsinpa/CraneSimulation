using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class extension
{
    
    /// <summary>
    /// 數值範圍指定.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="val">The val.</param>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <returns>T.</returns>
    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }




}
