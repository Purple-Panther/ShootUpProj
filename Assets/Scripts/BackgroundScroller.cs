using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private float startPosition;

    void Start()
    {
        startPosition = transform.position.y; // Ajuste para a posição inicial em Y
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * -scrollSpeed, 10); // Invertendo a velocidade e aplicando o movimento em Y
        transform.position = new Vector2(transform.position.x, startPosition + newPosition);
    }
}
