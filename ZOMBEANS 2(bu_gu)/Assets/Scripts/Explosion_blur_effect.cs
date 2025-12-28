using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements.Experimental;

public class Explosion_blur_effect : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField dof;
    private ChromaticAberration cma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (volume.profile.TryGetSettings<DepthOfField>(out dof))
        {

        }
        if (volume.profile.TryGetSettings<ChromaticAberration>(out cma))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value, 0, 5);
        cma.intensity.value = Mathf.Clamp(cma.intensity.value, 0, 1);
        if (dof.focusDistance.value < 5)
        {

            dof.focusDistance.value += Time.deltaTime * 1f;
        }
        if (cma.intensity.value > 0)
        {

            cma.intensity.value -= Time.deltaTime * 0.5f;
        }

    }


}
