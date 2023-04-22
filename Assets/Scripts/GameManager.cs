using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    StackApiUseCase _stackApi = new StackApiUseCase();
    JengaGameGenerator jengaGameGenerator;
    StackModel _stack;
    GameObject blockPrefab;
    [SerializeField]
    TMPro.TMP_Text textUI;
    [SerializeField]
    CameraController cameraController;
    GameObject canvasPrefab;
    int currentAnchorIndex = 0;

    void Awake()
    {
        GetStackDataFromApi();
        blockPrefab = Resources.Load("Prefabs/JengaBlock") as GameObject;
        canvasPrefab = Resources.Load("Prefabs/CanvasWorldSpace") as GameObject;
        jengaGameGenerator = new JengaGameGenerator(blockPrefab, canvasPrefab);
    }
    void GetStackDataFromApi()
    {
        StartCoroutine(_stackApi.FetchData(
            (stack) => {
               _stack = stack;
                _stack.OrderStack();
                jengaGameGenerator.GenerateJengaGame(_stack);
                ChangeCameraFocus(0);
            })
        );
    }
    public void ChangeCameraFocus(int option)
    {
        currentAnchorIndex += option;
        if (currentAnchorIndex > jengaGameGenerator.stackAnchors.Count - 1) currentAnchorIndex = 0;
        if (currentAnchorIndex < 0) currentAnchorIndex = jengaGameGenerator.stackAnchors.Count - 1;
        cameraController.SetTarget(jengaGameGenerator.stackAnchors[currentAnchorIndex]);
    }

    public void ShowInfoOnUI(Transform parent, BlockModel blockModel)
    {
        textUI.text = blockModel.Description;
        cameraController.SetTarget(parent);

    }
}
