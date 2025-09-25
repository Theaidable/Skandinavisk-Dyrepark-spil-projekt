using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[DisallowMultipleComponent]
public class ApplyBackgroundFromBank : MonoBehaviour
{
    [SerializeField] private GraphicsBank bank;
    [SerializeField] private string textureProperty = "_MainTex";
    [SerializeField] private bool instantiateMaterial = true;

    private Renderer targetRenderer;
    private RawImage targetRawImage;
    private Material _instance;

    private void Awake()
    {
        if(bank == null)
        {
            Debug.LogWarning($"{name}: GraphicsBank not assigned", this);
            return;
        }

        if(targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if(targetRawImage == null)
        {
            targetRawImage = GetComponent<RawImage>();
        }
    }

    private void OnEnable()
    {
        Apply();
        
        #if UNITY_EDITOR
        
        GraphicsBank.Changed += OnBankChanged;
        
        #endif
    }

    private void OnDisable()
    {
        #if UNITY_EDITOR
        
        GraphicsBank.Changed -= OnBankChanged;
        
        #endif

        // ryd instans i både Play og Edit
        if (_instance != null)
        {
            if (targetRenderer && targetRenderer.sharedMaterial == _instance)
            {
                targetRenderer.sharedMaterial = null;

                #if UNITY_EDITOR

                if (!Application.isPlaying)
                {
                    DestroyImmediate(_instance);
                }

                DestroyImmediate(_instance);

                #endif
            }

            _instance = null;
        }
    }

    #if UNITY_EDITOR

    void OnValidate()
    {
         Apply();
    }

    void OnBankChanged(GraphicsBank b)
    {
        if (b == bank)
        {
            Apply();
        }
    }

#endif

    public void Apply()
    {
        if (bank == null || bank.backgroundTexture == null)
        {
            return;
        }

        if (targetRawImage != null)
        {
            targetRawImage.texture = bank.backgroundTexture;
        
            #if UNITY_EDITOR
            
            UnityEditor.EditorUtility.SetDirty(targetRawImage);
            
            #endif

            return;
        }

        if (targetRenderer == null)
        {
            return;
        }

        var srcMat = targetRenderer.sharedMaterial;

        if (srcMat == null)
        {
            return;
        }

        var makeInstance = instantiateMaterial || Application.isPlaying;

        if (_instance == null)
        {
            _instance = makeInstance ? Instantiate(srcMat) : srcMat;
        }

        _instance.SetTexture(textureProperty, bank.backgroundTexture);
        targetRenderer.sharedMaterial = _instance;

        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(targetRenderer);

        if (_instance != null)
        {
            UnityEditor.EditorUtility.SetDirty(_instance);
        }

        #endif
    }
}
