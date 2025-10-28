using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [Range(0, 500)]
    public int starCount = 200;
    
    [Range(0f, 10000f)]
    public float scrollSpeed = 3612f; // Reduzido em 40% do valor anterior (6020f)
    
    [Range(0.2f, 1.5f)]
    public float starSize = 0.48f; // Reduzido em 60%
    
    [Range(0f, 0.025f)]
    public float twinkleSpeed = 0.012f;
    
    public bool enableParallax = true;
    public bool enableTwinkle = true;
    
    [Range(0.8f, 1f)]
    public float starBrightness = 0.95f;
    
    [Tooltip("Layer para renderização das estrelas")]
    public string backgroundLayer = "Background";
    
    [Tooltip("Ordem de renderização das estrelas")]
    public int sortingOrder = -100;

    private struct Star
    {
        public Vector2 position;
        public float size;
        public float brightness;
        public float twinklePhase;
        public float layerSpeed;
        public Color tint;
    }

    private Star[] stars;
    private float scrollOffset;
    private Camera cam;
    private float initialCamSize;
    private float initialFieldOfView;
    private float targetStarScale = 1.0f;
    private float currentStarScale = 1.0f;
    private const float WorldWidth = 200f;
    private const float WorldHeight = 400f;
    private const float ScaleTransitionSpeed = 0.5f;
    
    private Canvas backgroundCanvas;
    private RawImage starsImage;
    private RenderTexture renderTexture;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            enabled = false;
            return;
        }
        
        initialCamSize = cam.orthographicSize;
        initialFieldOfView = cam.fieldOfView;
        SetupBackgroundCanvas();
        GenerateStars();
    }
    
    void SetupBackgroundCanvas()
    {
        // Canvas para renderização das estrelas em plano de fundo
        GameObject canvasObj = new GameObject("BackgroundCanvas");
        backgroundCanvas = canvasObj.AddComponent<Canvas>();
        backgroundCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        backgroundCanvas.worldCamera = cam;
        backgroundCanvas.sortingLayerName = backgroundLayer;
        backgroundCanvas.sortingOrder = sortingOrder;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        GameObject imageObj = new GameObject("StarsImage");
        imageObj.transform.SetParent(canvasObj.transform, false);
        starsImage = imageObj.AddComponent<RawImage>();
        
        RectTransform rectTransform = starsImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        starsImage.texture = renderTexture;
    }

    void GenerateStars()
    {
        stars = new Star[starCount];
        
        // Distribuição mais uniforme com menos estrelas
        for (int i = 0; i < starCount; i++)
        {
            stars[i] = new Star
            {
                position = new Vector2(
                    Random.Range(-WorldWidth * 0.5f, WorldWidth * 0.5f),
                    Random.Range(0f, WorldHeight)
                ),
                size = Random.Range(0.5f, 2.5f) * starSize * 0.04f,
                brightness = Random.Range(0.7f, 1f) * starBrightness,
                twinklePhase = Random.Range(0f, Mathf.PI * 2f),
                layerSpeed = enableParallax ? Random.Range(0.15f, 0.7f) : 0.4f,
                tint = Color.Lerp(Color.white, new Color(1f, 0.95f, 0.85f), Random.Range(0f, 0.4f))
            };
        }
    }

    void Update()
    {
        scrollOffset += Time.deltaTime * scrollSpeed;
        
        // Adaptar escala das estrelas ao zoom da câmera
        if (backgroundCanvas != null && cam != null)
        {
            if (renderTexture.width != Screen.width || renderTexture.height != Screen.height)
            {
                renderTexture.Release();
                renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
                starsImage.texture = renderTexture;
            }
            
            // Calcular o fator de escala alvo baseado no zoom da câmera
            // Quando a câmera faz zoom out, as estrelas devem diminuir para 50% do tamanho
            if (cam.fieldOfView > initialFieldOfView)
            {
                float zoomProgress = (cam.fieldOfView - initialFieldOfView) / (120f - initialFieldOfView);
                targetStarScale = Mathf.Lerp(1.0f, 0.5f, zoomProgress);
            }
            else
            {
                targetStarScale = 1.0f;
            }
            
            // Transição suave do tamanho atual para o tamanho alvo
            currentStarScale = Mathf.Lerp(currentStarScale, targetStarScale, Time.deltaTime * ScaleTransitionSpeed);
            
            RenderStars();
        }
    }

    private void RenderStars()
    {
        if (cam == null) return;
        
        RenderTexture previousRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        
        GL.Clear(true, true, Color.clear);
        
        // Escala dinâmica baseada no zoom da câmera
        float zoomFactor = initialCamSize / cam.orthographicSize;
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float aspect = cam.aspect;
        float orthoSize = cam.orthographicSize;
        // Ajuste de escala para melhor visualização com menos estrelas
        // Aplicar a escala de transição suave para o tamanho das estrelas
        float pixelScale = screenWidth / (aspect * orthoSize * 3.8f) * zoomFactor * currentStarScale;
        
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, screenWidth, screenHeight, 0);
        
        Material starMaterial = new Material(Shader.Find("Unlit/Transparent"));
        starMaterial.mainTexture = Texture2D.whiteTexture;
        starMaterial.SetPass(0);
        
        for (int i = 0; i < starCount; i++)
        {
            ref var star = ref stars[i];
            
            float starY = (star.position.y + scrollOffset * star.layerSpeed) % WorldHeight;
            if (starY < 0f) starY += WorldHeight;
            
            float screenX = (star.position.x / WorldWidth + 0.5f) * screenWidth;
            float screenY = starY / WorldHeight * screenHeight;
            
            float pixelSize = star.size * pixelScale;
            
            if (screenX >= -30f && screenX <= screenWidth + 30f && 
                screenY >= -30f && screenY <= screenHeight + 30f)
            {
                DrawStar(screenX, screenY, pixelSize, ref star, starMaterial);
            }
        }
        
        GL.PopMatrix();
        RenderTexture.active = previousRT;
    }
    
    private void DrawStar(float x, float y, float size, ref Star star, Material material)
    {
        float twinkle = enableTwinkle ? 
            0.6f + 0.4f * Mathf.Sin(Time.time * twinkleSpeed * 4f + star.twinklePhase) : 1f;
        float alpha = star.brightness * twinkle;
        
        // Três camadas de glow para efeito visual
        DrawQuad(x - size * 1.5f, y - size * 1.5f, size * 3f, size * 3f, 
                star.tint, alpha * 0.5f, material);
        
        DrawQuad(x - size * 1f, y - size * 1f, size * 2f, size * 2f, 
                star.tint, alpha * 0.8f, material);
        
        DrawQuad(x - size * 0.5f, y - size * 0.5f, size, size, 
                star.tint, alpha, material);
    }
    
    private void DrawQuad(float x, float y, float width, float height, Color color, float alpha, Material material)
    {
        Color prevColor = material.color;
        material.color = color * new Color(1f, 1f, 1f, alpha);
        
        GL.Begin(GL.QUADS);
        GL.Color(material.color);
        GL.TexCoord2(0, 0);
        GL.Vertex3(x, y, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(x + width, y, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(x + width, y + height, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(x, y + height, 0);
        GL.End();
        
        material.color = prevColor;
    }
    
    private void OnDestroy()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
    }
}