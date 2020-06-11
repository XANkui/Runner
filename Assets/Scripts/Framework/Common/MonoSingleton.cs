
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T m_instance;

    public static T Instance { get => m_instance;  }

    protected virtual void Awake() {
        m_instance = this as T;
    }
}
