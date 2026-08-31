using UnityEngine;
using TMPro;

public class TextParticleEmitter : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public ParticleSystem textParticleSystem;
    private ParticleSystemRenderer particleRenderer;

    void Start()
    {
        if (textMeshPro == null || textParticleSystem == null) return;

        // Force TMPro to generate its mesh immediately
        textMeshPro.ForceMeshUpdate();

        // Get the renderer component and assign the text mesh
        particleRenderer = textParticleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.mesh = textMeshPro.mesh;

        // Match the text material to ensure characters/font textures align correctly
       // particleRenderer.material = textMeshPro.materialForRendering;
    }

    [ContextMenu("Emit Damage")]
    public void EmitDamageText(float value)
    {
        textMeshPro.text = "-"+value.ToString();
        textParticleSystem.Play();
    }

}
