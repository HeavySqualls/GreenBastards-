using UnityEngine;

public class Toolbox : MonoBehaviour
{
    private static Toolbox _instance;

    public static Toolbox GetInstance()
    {
        if (Toolbox._instance == null)
        {
            var go = new GameObject("Toolbox");
            DontDestroyOnLoad(go);
            Toolbox._instance = go.AddComponent<Toolbox>();
        }

        return Toolbox._instance;
    }

    private GameManager gameManager;
    private TimeManager timeManager;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        this.gameManager = gameObject.AddComponent<GameManager>();
        this.timeManager = gameObject.AddComponent<TimeManager>();
    }

    public GameManager GetGameManager()
    {
        return this.gameManager;
    }

    public TimeManager GetTimeManager()
    {
        return this.timeManager;
    }
}
