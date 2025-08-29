using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    private static BulletCounter _instance;
    public static BulletCounter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletCounter>();
                if (_instance == null)
                {
                    Debug.LogError("BulletCounter instance not found in scene!");
                }
            }
            return _instance;
        }
    }

    [Header("UI References")]
    [SerializeField] private Text _bulletCountText;
    [SerializeField] private Canvas _uiCanvas;

    private void Awake()
    {
        // Setup Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        // Crear UI si no existe
        if (_bulletCountText == null)
        {
            CreateBulletCounterUI();
        }
    }

    private void Update()
    {
        // Contar balas activas en tiempo real
        UpdateActiveBulletCount();
    }

    private void UpdateActiveBulletCount()
    {
        int activeBullets = 0;
        
        // Buscar todas las balas en el BulletPool
        if (BulletPool.Instance != null)
        {
            // Contar todos los GameObjects con componente Bullet que estén activos
            Bullet[] allBullets = FindObjectsOfType<Bullet>();
            foreach (Bullet bullet in allBullets)
            {
                if (bullet.gameObject.activeInHierarchy)
                {
                    activeBullets++;
                }
            }
        }
        
        UpdateUI(activeBullets);
    }

    private void UpdateUI(int bulletCount)
    {
        if (_bulletCountText != null)
        {
            _bulletCountText.text = $"Active Bullets: {bulletCount}";
        }
    }

    public int GetActiveBulletCount()
    {
        int activeBullets = 0;
        Bullet[] allBullets = FindObjectsOfType<Bullet>();
        foreach (Bullet bullet in allBullets)
        {
            if (bullet.gameObject.activeInHierarchy)
            {
                activeBullets++;
            }
        }
        return activeBullets;
    }

    public void ClearAllActiveBullets()
    {
        // Desactivar todas las balas activas
        Bullet[] allBullets = FindObjectsOfType<Bullet>();
        foreach (Bullet bullet in allBullets)
        {
            if (bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(false);
            }
        }
        
        // Actualizar UI inmediatamente
        UpdateUI(0);
    }

    private void CreateBulletCounterUI()
    {
        // Crear Canvas si no existe
        if (_uiCanvas == null)
        {
            GameObject canvasGO = new GameObject("BulletCounterCanvas");
            _uiCanvas = canvasGO.AddComponent<Canvas>();
            _uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Crear el texto
        GameObject textGO = new GameObject("BulletCountText");
        textGO.transform.SetParent(_uiCanvas.transform, false);
        
        _bulletCountText = textGO.AddComponent<Text>();
        _bulletCountText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        _bulletCountText.fontSize = 24;
        _bulletCountText.color = Color.white;
        _bulletCountText.text = "Active Bullets: 0";

        // Posicionar en la esquina superior izquierda
        RectTransform rectTransform = _bulletCountText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchoredPosition = new Vector2(20, -20);
        rectTransform.sizeDelta = new Vector2(200, 30);
    }
}
