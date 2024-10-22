using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
           if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if(instance == null)
                {
                    string tName = typeof(T).ToString();
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                }
            }
            return instance;
        }

    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}