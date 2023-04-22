using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlock : MonoBehaviour
{
    BlockModel _blockModel;
    //If the number of metarials was larger, a list would be better, but 3 properties are fine.
    Material _glassMaterial, _woodMaterial, _stoneMaterial;

    void Awake()
    {
        _glassMaterial = Resources.Load<Material>("Materials/Glass");
        _woodMaterial = Resources.Load<Material>("Materials/Wood");
        _stoneMaterial = Resources.Load<Material>("Materials/Stone");
    }

    /// <summary>
    /// Set the block model and setup the MeshRenderer Material
    /// </summary>
    /// <param name="blockModel"></param>
    public void SetBlockModel(BlockModel blockModel)
    {
        _blockModel = blockModel;
        GetComponent<MeshRenderer>().material = GetMaterialFromBlockType(blockModel.GetBlockType);
    }

    /// <summary>
    /// Return the material based on block type
    /// </summary>
    /// <param name="blockType"></param>
    /// <returns></returns>
    Material GetMaterialFromBlockType(BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Glass:
                 return _glassMaterial;
            case BlockType.Wood:
                return _woodMaterial;
            case BlockType.Stone:
                return _stoneMaterial;
        }
        return _glassMaterial;
    }

    /// <summary>
    /// If user clicks with mouse left button inside object, The UI shows the block description
    /// </summary>
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)){
            GameManager.Instance.ShowInfoOnUI(transform.parent, _blockModel);
        }
    }
}
