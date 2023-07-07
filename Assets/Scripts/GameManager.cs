using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LayerMask obstacleLayers;
    public CircleCollider2D playerCollider;
    public Transform level;
    public float dragDamping = 0.25f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
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
