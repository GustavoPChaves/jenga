using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlock : MonoBehaviour
{
    BlockModel blockModel;
    Material glassMaterial, woodMaterial, stoneMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        glassMaterial = Resources.Load<Material>("Materials/Glass");
        woodMaterial = Resources.Load<Material>("Materials/Wood");
        stoneMaterial = Resources.Load<Material>("Materials/Stone");
    }

    public void SetBlockType(BlockModel blockModel)
    {
        this.blockModel = blockModel;
        GetComponent<MeshRenderer>().material = GetMaterialFromBlockType(blockModel.GetBlockType);
    }

    Material GetMaterialFromBlockType(BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Glass:
                 return glassMaterial;
            case BlockType.Wood:
                return woodMaterial;
            case BlockType.Stone:
                return stoneMaterial;
        }
        return glassMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            GameManager.Instance.ShowInfoOnUI(transform.parent ,blockModel);
        }
    }
}
