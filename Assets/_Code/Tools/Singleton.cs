using System.Diagnostics;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        [DebuggerStepThrough]
        get
        {
            return _instance ? _instance : NotFound();
        }
    }

    private static T NotFound()
    {
        UnityEngine.Debug.LogError(typeof(T) + " not found!");
        return null;
    }

    protected void Awake()
    {
        if (!_instance) _instance = (T)this;
        else Destroy(gameObject);
    }
}