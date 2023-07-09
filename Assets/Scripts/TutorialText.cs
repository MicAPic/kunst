using DG.Tweening;
using ntw.CurvedTextMeshPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public float speed = 1.0f;
    private RectTransform _rectTransform;
     
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
