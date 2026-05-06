using System.Collections;
using UnityEngine;

public class ShadeEnvironment : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [RangeAttribute(0.0f, 1.0f)]
    [SerializeField] float colorStrength;

    private void Awake()
    {
        _renderer = transform.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        TimeManager.DayPeriodChange += Shade;
    }

    private void OnDisable()
    {
        TimeManager.DayPeriodChange -= Shade;
    }
    private void Update()
    {
        
    }

    private void Shade(Gradient lightGradient, float duration)
    {
        StartCoroutine(LerpShading(lightGradient, duration));
    }

    private IEnumerator LerpShading(Gradient lightGradient, float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            Color lightColor = lightGradient.Evaluate(i / duration);
            Vector3 finalColor = new Vector3(Mathf.Clamp(lightColor.r + (1.0f - colorStrength), 0.0f, 1.0f),
                                        Mathf.Clamp(lightColor.g + (1.0f - colorStrength), 0.0f, 1.0f),
                                        Mathf.Clamp(lightColor.b + (1.0f - colorStrength), 0.0f, 1.0f));
            _renderer.color = new Color(finalColor.x, finalColor.y, finalColor.z);
            yield return null;
        }
    }
}
