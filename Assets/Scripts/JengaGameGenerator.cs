using System.Collections.Generic;
using UnityEngine;

public class JengaGameGenerator
{
    GameObject _blockPrefab;
    GameObject _canvasPrefab;
    public List<Transform> stackAnchors { get; private set; }

    public JengaGameGenerator(GameObject blockPrefab, GameObject canvasPrefab)
    {
        stackAnchors = new List<Transform>();
        _blockPrefab = blockPrefab;
        _canvasPrefab = canvasPrefab;
    }

    /// <summary>
    /// Generate the stacks based on <StackModel cref="StackModel.cs"/> data
    /// For each grade, it create a empty GameObject to be the parent, places a Canvas with text
    /// and calls GenerateJengaStack to procedurally generate the stack
    /// </summary>
    /// <param name="stack"></param>
    public void GenerateJengaGame(StackModel stack)
    {
        int loopCount = 0;
        foreach (var key in stack.sortedStack.Keys)
        {
            var anchor = new GameObject(key);
            stackAnchors.Add(anchor.transform);
            anchor.transform.position = Vector3.right * 20 * loopCount;
            SetupCanvas(anchor.transform.position, key);
            GenerateJengaStack(anchor.transform, stack.sortedStack[key]);
            loopCount++;
        }
    }

    /// <summary>
    /// Place a Canvas with text in front of the stack
    /// </summary>
    /// <param name="position">Position of the stack</param>
    /// <param name="title">Text to be presented, e.g. grade</param>
    void SetupCanvas(Vector3 position, string title)
    {
        var canvas = MonoBehaviour.Instantiate(_canvasPrefab);
        canvas.transform.position = position + Vector3.back * 5;
        canvas.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = title;
    }

    /// <summary>
    /// Procedurally generate the stack
    /// StackHeight: the number of rows of the stack, 
    /// Each row of the stack is formed by 3 blocks, but the last row os blocks can be less than 3
    /// The rotation of the block is based on row count parity
    /// If the generation were longer, this function would be a IEnumarator with a callback completion
    /// </summary>
    /// <param name="anchor">The parent of the stack</param>
    /// <param name="blockModels">The Model to setup the Jenga Block Prefab</param>
    void GenerateJengaStack(Transform anchor, List<BlockModel> blockModels)
    {
        int stackHeight = Mathf.CeilToInt(blockModels.Count / 3f);
        for (int i = 0; i < stackHeight; i++)
        {
            var rowCount = Mathf.Min(blockModels.Count - i * 3, 3);
            for (int j = 0; j < rowCount; j++)
            {
                var block = MonoBehaviour.Instantiate(_blockPrefab, anchor);
                block.GetComponent<JengaBlock>().SetBlockType(blockModels[i + j]);
                if (i % 2 == 0)
                {
                    block.transform.localPosition = new Vector3(j, i, 0);
                }
                else
                {
                    block.transform.Rotate(Vector3.up * i, 90);
                    block.transform.localPosition = new Vector3(1f, i, j - 1f);
                }
            }
        }
    }
}
