using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    StackApiUseCase _stackApi = new StackApiUseCase();
    StackModel _stack;
    GameObject blockPrefab;
    [SerializeField]
    TMPro.TMP_Text textUI;
    [SerializeField]
    CameraController camera;

    GameObject canvasPrefab;

    List<Transform> stackAnchors = new List<Transform>();
    int currentAnchorIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        GetData();
        blockPrefab = Resources.Load("Prefabs/JengaBlock") as GameObject;
        canvasPrefab = Resources.Load("Prefabs/CanvasWorldSpace") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraFocus(int option)
    {
        currentAnchorIndex += option;
        if (currentAnchorIndex > stackAnchors.Count - 1) currentAnchorIndex = 0;
        if (currentAnchorIndex < 0) currentAnchorIndex = stackAnchors.Count - 1;
        camera.SetTarget(stackAnchors[currentAnchorIndex]);
    }

    public void ShowInfoOnUI(Transform parent, BlockModel blockModel)
    {
        textUI.text = blockModel.Description;
        camera.SetTarget(parent);
        
    }
    void GetData()
    {
        StartCoroutine(_stackApi.FetchData(
            (stack) => {
               _stack = stack;
                _stack.OrderStack();
                GenerateJengaGame(_stack);
            })
        );
    }

    void GenerateJengaGame(StackModel stack)
    {
        int interation = 0;
        foreach (var key in stack.sortedStack.Keys)
        {
            var anchor = new GameObject(key);
            stackAnchors.Add(anchor.transform);
            anchor.transform.position = Vector3.right * 20 * interation;
            var canvas = Instantiate(canvasPrefab);
            canvas.transform.position = anchor.transform.position + Vector3.back * 5;
            canvas.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = key;
            GenerateJengaStack(anchor.transform, stack.sortedStack[key]);
            interation++;
        }
        ChangeCameraFocus(0);
    }

    void GenerateJengaStack(Transform anchor, List<BlockModel> blockModels)
    {
        int stackHeight = Mathf.CeilToInt(blockModels.Count / 3f);
        for (int i = 0; i < stackHeight; i++)
        {
            var rowCount = Mathf.Min(blockModels.Count - i * 3, 3);
            for (int j = 0; j < rowCount; j++)
            {
                var block = Instantiate(blockPrefab, anchor);
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
