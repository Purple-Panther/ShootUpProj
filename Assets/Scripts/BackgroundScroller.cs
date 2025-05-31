using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private float _startPosition;

    void Start()
    {
        _startPosition = transform.position.y; 
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * -scrollSpeed, 10); 
        transform.position = new Vector2(transform.position.x, _startPosition + newPosition);
    }
}
