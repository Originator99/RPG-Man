using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour {
    [ContextMenu("Convert To Regular Mesh")]
    private void Convert() {
        SkinnedMeshRenderer skinnedMeshRender = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRender = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilder = gameObject.AddComponent<MeshFilter>();

        meshFilder.sharedMesh = skinnedMeshRender.sharedMesh;
        meshRender.sharedMaterials = skinnedMeshRender.sharedMaterials;

        DestroyImmediate(skinnedMeshRender);
        DestroyImmediate(this);
    }
}
