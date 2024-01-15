using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 單例實例
    private static T _instance;

    // 公開的單例屬性
    public static T Instance
    {
        get
        {
            // 如果_instance為空，則在場景中查找該類型的物件
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                // 如果在場景中沒有該物件，則創建一個新的物件
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    // 在Awake中初始化
    protected virtual void Awake()
    {
        // 確保只有一個實例存在
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        Awakes();
    }
    protected virtual void Awakes(){}
    // 在這裡添加你的其他邏輯和數據
}