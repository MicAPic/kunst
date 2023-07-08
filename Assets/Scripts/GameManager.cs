using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Camera mainCamera;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        
        mainCamera = Camera.main;
    }
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
