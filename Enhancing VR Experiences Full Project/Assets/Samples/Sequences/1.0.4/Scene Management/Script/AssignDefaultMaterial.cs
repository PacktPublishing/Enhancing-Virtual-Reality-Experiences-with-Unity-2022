using UnityEngine.Rendering;

namespace UnityEngine.Sequences.Sample
{
    /// <summary>
    /// Custom script to assign the default material from the active RenderPipeline asset
    /// to the MeshRenderer so the sample stays compatible with URP and HDRP.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshRenderer))]
    class AssignDefaultMaterial : MonoBehaviour
    {
        private void Start()
        {
            var renderer = gameObject.GetComponent<MeshRenderer>();

            var renderPipelineAsset = GraphicsSettings.currentRenderPipeline;
            if (renderPipelineAsset != null && renderer.sharedMaterial != renderPipelineAsset.defaultMaterial)
                renderer.sharedMaterial = renderPipelineAsset.defaultMaterial;
        }
    }
}
