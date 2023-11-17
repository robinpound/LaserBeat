using UnityEngine;

public class TargetType : MonoBehaviour
{
    [SerializeField] private Material primaryMaterial, secondaryMaterial;
    public enum TargetMaterial { Primary, Secondary };
    private TargetMaterial targetMaterial;
    
    private void Start()
    {
        bool randColour = Random.Range(0, 2) == 1;
        if (randColour) targetMaterial = TargetMaterial.Primary;
        else targetMaterial = TargetMaterial.Secondary;

        switch (targetMaterial)
        {
            case TargetMaterial.Primary:
                gameObject.GetComponent<Renderer>().material = primaryMaterial;
                break;

            case TargetMaterial.Secondary:
                gameObject.GetComponent<Renderer>().material = secondaryMaterial;
                break;
        }
    }

    public TargetMaterial GetTargetType()
    {
        return targetMaterial;
    }
}
