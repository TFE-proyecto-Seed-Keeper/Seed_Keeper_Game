using UnityEngine;

public class LightCookieMotion : MonoBehaviour
{
    [SerializeField] private Material lightCookieMateria;

    [Header("Texture 1")]
    [SerializeField] private Vector2 cycleDuration1UV = new (20f, 20f);
    [SerializeField] private AnimationCurve movementPaht1U;
    [SerializeField] private AnimationCurve movementPaht1V;
    [SerializeField] private Vector2 movementMagnitude1UV = new(0.1f, 0.1f);
    [SerializeField] private Vector2 movementTimeOffset1UV = new();
    [SerializeField] private Vector2 texture1TilingUV = new(1f, 1f);
    [SerializeField] private Vector2 Texture1OffsetUV = new();

    [Header("Texture 2")]
    [SerializeField] private Vector2 cycleDuration2UV = new(20f, 20f);
    [SerializeField] private AnimationCurve movementPaht2U;
    [SerializeField] private AnimationCurve movementPaht2V;
    [SerializeField] private Vector2 movementMagnitude2UV = new(0.1f, 0.1f);
    [SerializeField] private Vector2 movementTimeOffset2UV = new();
    [SerializeField] private Vector2 texture2TilingUV = new(1f, 1f);
    [SerializeField] private Vector2 Texture2OffsetUV = new();

    private float time1U;
    private float time1V;
    private float time2U;
    private float time2V;

    void Update()
    {
        time1U = Time.time % cycleDuration1UV.x;
        time1U /= cycleDuration1UV.x;

        time1V = Time.time % cycleDuration1UV.y;
        time1V /= cycleDuration1UV.y;

        time2U = Time.time % cycleDuration2UV.x;
        time2U /= cycleDuration2UV.x;

        time2V = Time.time % cycleDuration2UV.y;
        time2V /= cycleDuration2UV.y;

        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        float newU1 = movementPaht1U.Evaluate(time1U + movementTimeOffset1UV.x) * movementMagnitude1UV.x;
        float newV1 = movementPaht1V.Evaluate(time1V + movementTimeOffset1UV.y) * movementMagnitude1UV.y;

        Vector4 newUV1 = new(texture1TilingUV.x, texture1TilingUV.y, newU1 + Texture1OffsetUV.x, newV1 + Texture1OffsetUV.y);

        lightCookieMateria.SetVector("_Texture1_ST", newUV1);

        float newU2 = movementPaht2U.Evaluate(time2U + movementTimeOffset2UV.x) * movementMagnitude2UV.x;
        float newV2 = movementPaht2V.Evaluate(time2V + movementTimeOffset2UV.y) * movementMagnitude2UV.y;

        Vector4 newUV2 = new(texture2TilingUV.x, texture2TilingUV.y, newU2 + Texture2OffsetUV.x, newV2 + Texture2OffsetUV.y);

        lightCookieMateria.SetVector("_Texture2_ST", newUV2);
    }

    private void OnValidate()
    {
        UpdateMaterial();
    }
}
