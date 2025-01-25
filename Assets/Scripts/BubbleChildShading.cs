using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChildShading : MonoBehaviour
{
    static readonly int PROPERTY_BUBBLE_ORIGIN = Shader.PropertyToID("_BubbleOrigin");
    static readonly int PROPERTY_BUBBLE_RANGE_SQR = Shader.PropertyToID("_BubbleRangeSqr");

    bool isDirty;
    Vector3 worldPosition;
    float radius = 1f;

	Dictionary<Material, Material> materialVariants = new Dictionary<Material, Material>();
	List<Material> activeMaterials = new List<Material>();

	public void UpdateBubble(Vector3 worldPosition, float radius)
    {
        if(this.worldPosition != worldPosition || this.radius != radius)
        {
            this.isDirty = true;
        }

        this.worldPosition = worldPosition;
        this.radius = radius;
    }

    void Awake()
    {
        FindRendererMaterials();
        this.isDirty = true;
    }

    void LateUpdate()
    {
        if (this.isDirty)
        {
            this.isDirty = false;
            UpdateMaterials();
        }
    }

    static bool IsBubbleMaskedMaterial(Material m)
    {
        return true;
    }

	void FindRendererMaterials()
    {
        var childRenderers = GetComponentsInChildren<Renderer>();

        foreach (var childRenderer in childRenderers)
        {
            RegisterChildRenderer(childRenderer);
        }
    }

    void RegisterChildRenderer(Renderer renderer)
    {
        Material[] newMaterials = new Material[renderer.sharedMaterials.Length];
        for(int i = 0; i < renderer.sharedMaterials.Length; i++)
        {
            var mat = renderer.sharedMaterials[i];
            newMaterials[i] = mat;

            if(mat == null || !IsBubbleMaskedMaterial(mat))
            {
                continue;
            }

            if(!this.materialVariants.ContainsKey(mat))
            {
				RegisterMaterialVariant(mat);
			}
			newMaterials[i] = this.materialVariants[mat];
		}

        renderer.sharedMaterials = newMaterials;
	}

    void RegisterMaterialVariant(Material original)
    {
        var variant = new Material(original);
        this.materialVariants.Add(original, variant);
        this.activeMaterials.Add(variant);
    }

	void UpdateMaterials()
    {
        float radiusSqr = radius * radius;

        foreach(var mat in this.activeMaterials)
        {
            mat.SetVector(PROPERTY_BUBBLE_ORIGIN, this.worldPosition);
            mat.SetFloat(PROPERTY_BUBBLE_RANGE_SQR, radiusSqr);
        }
    }
}
