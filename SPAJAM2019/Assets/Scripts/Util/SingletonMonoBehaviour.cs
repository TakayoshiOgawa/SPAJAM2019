using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton"), SerializeField]
    private bool dontDestoryOnLoad = false;

    /// <summary>
    /// 静的なインスタンス
    /// </summary>
    private static T instance;
    public static T Instance
    {
        get
        {
            if (!HasInstance())
            {
                FindInstance();
            }
            return instance;
        }
    }

    /// <summary>
    /// インスタンスが存在するか
    /// </summary>
    public static bool HasInstance()
    {
        return instance != null;
    }

    /// <summary>
    /// シーン内のインスタンスを検索
    /// </summary>
    static protected void FindInstance()
    {
        instance = FindObjectOfType<T>();
        if (instance == null)
        {
            Debug.LogError("Not Find a " + typeof(T) + " instance");
        }
    }

    /// <summary>
    /// 生成時に実行
    /// </summary>
    protected virtual void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        if (Instance != this)
        {
            Destroy(this);
            return;
        }

        // シングルトンとして破棄されないオブジェクトに登録
        if (dontDestoryOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}