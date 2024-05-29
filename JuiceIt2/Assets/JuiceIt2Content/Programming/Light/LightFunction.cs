using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFunction : MonoBehaviour
{
    private UniversalAdditionalLightData _lightExtantion;

    private void Awake()
    {
        _lightExtantion = GetComponent<UniversalAdditionalLightData>();
    }

    private void Update()
    {
        _lightExtantion.lightCookieOffset += Vector2.one * Time.deltaTime;
    }
    
}
