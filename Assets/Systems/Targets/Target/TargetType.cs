using UnityEngine;

public class TargetType : MonoBehaviour
{
    [SerializeField] private Material primaryMaterial, secondaryMaterial, finalMaterial;
    [SerializeField] private bool isFinal = false;
    public enum TargetMaterial { Primary, Secondary, Final};
    private TargetMaterial targetMaterial;
    
    private void Start()
    {
        bool randColour = Random.Range(0, 2) == 1;
        if (true) targetMaterial = TargetMaterial.Primary; // Change to rand colour after testing
        else targetMaterial = TargetMaterial.Secondary;
        if (isFinal) targetMaterial = TargetMaterial.Final;

        switch (targetMaterial)
        {
            case TargetMaterial.Primary:
                gameObject.GetComponent<Renderer>().material = primaryMaterial;
                break;

            case TargetMaterial.Secondary:
                gameObject.GetComponent<Renderer>().material = secondaryMaterial;
                break;

            case TargetMaterial.Final:
                gameObject.GetComponent<Renderer>().material = finalMaterial;
                break;
        }
    }

    public TargetMaterial GetTargetType()
    {
        return targetMaterial;
    }
}
