using System;
using System.Reflection;
using UnityEngine;

public static class PMHelper
{
    public static float ymax;
    public static String curLevel="Level 1";
    public static String nextLevel="Level 2";

    public static GameObject Exist(String s)
    {
        return GameObject.Find(s);
    }
    public static T Exist<T>(GameObject g)
    {
        return g.GetComponent<T>();
    }
    public static bool Child(GameObject g, GameObject g2)
    {
        return g.transform.IsChildOf(g2.transform);
    }
}
