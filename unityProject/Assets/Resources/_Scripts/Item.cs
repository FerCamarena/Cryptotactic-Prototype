using UnityEngine;

public class Item : MonoBehaviour
{
    public bool selected;
    [SerializeField] private Material outlineMaterial;

    private void Start()
    {
        // Assuming you have assigned the outline material in the inspector
        outlineMaterial = new Material(GetComponent<MeshRenderer>().sharedMaterial);
        outlineMaterial.enableInstancing = true;
    }

    private void Update()
    {
        if (selected)
        {
            AddOutlineMaterial();
        }
        else
        {
            RemoveOutlineMaterial();
        }
    }

    public void SetSelected(bool selectedd)
    {
        selected = selectedd;
    }

    private void AddOutlineMaterial()
    {
        if (!HasOutlineMaterial())
        {
            ApplyOutlineMaterial();
        }
    }

    private void RemoveOutlineMaterial()
    {
        if (HasOutlineMaterial())
        {
            RemoveOutlineMaterialInternal();
        }
    }

    private void ApplyOutlineMaterial()
    {
        Material[] originalMaterials = GetComponent<MeshRenderer>().sharedMaterials;
        Material[] newMaterials = new Material[originalMaterials.Length + 1];

        for (int i = 0; i < originalMaterials.Length; i++)
        {
            newMaterials[i] = originalMaterials[i];
        }

        newMaterials[originalMaterials.Length] = outlineMaterial;

        GetComponent<MeshRenderer>().sharedMaterials = newMaterials;
    }

    private void RemoveOutlineMaterialInternal()
    {
        Material[] originalMaterials = GetComponent<MeshRenderer>().sharedMaterials;
        Material[] newMaterials = new Material[originalMaterials.Length - 1];

        for (int i = 0, j = 0; i < originalMaterials.Length; i++)
        {
            if (originalMaterials[i] != outlineMaterial)
            {
                newMaterials[j++] = originalMaterials[i];
            }
        }

        GetComponent<MeshRenderer>().sharedMaterials = newMaterials;
    }

    private bool HasOutlineMaterial()
    {
        Material[] materials = GetComponent<MeshRenderer>().sharedMaterials;
        foreach (Material material in materials)
        {
            if (material == outlineMaterial)
            {
                return true;
            }
        }
        return false;
    }
}