using UnityEngine;

public class OutlineCollision : MonoBehaviour
{
    private Collider2D _collider;

    private Material _outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponentInChildren<Player>().GetComponent<Collider2D>();
        _outlineMaterial = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };

        CreateOutline();
    }

    private void CreateOutline()
    {
        // Calcula o tamanho do contorno baseado no tamanho do colisor
        Vector2 size = _collider.bounds.size;
        Vector2 center = _collider.bounds.center;

        // Desenha linhas para formar o contorno do colisor
        DrawLine(center + new Vector2(-size.x / 2, -size.y / 2),
            center + new Vector2(size.x / 2, -size.y / 2));
        DrawLine(center + new Vector2(size.x / 2, -size.y / 2),
            center + new Vector2(size.x / 2, -size.y / 2));
        DrawLine(center + new Vector2(size.x / 2, -size.y / 2),
            center + new Vector2(-size.x / 2, -size.y / 2));
        DrawLine(center + new Vector2(-size.x / 2, -size.y / 2),
            center + new Vector2(-size.x / 2, -size.y / 2));
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        // Cria um objeto de linha
        GameObject line = new GameObject("Outline");
        line.transform.position = start;
        line.AddComponent<LineRenderer>();

        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.sortingOrder = 2;

        // Define a cor do contorno
        lineRenderer.material = _outlineMaterial;

        // Define os pontos de início e fim da linha
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}