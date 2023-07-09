using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public Camera mainCamera;
    public Rigidbody2D[] boxes;
    public bool isPaused;
    public bool canPause;

    public DraggableObject draggableObject;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        
        mainCamera = Camera.main;
        draggableObject = FindObjectOfType<DraggableObject>();
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
