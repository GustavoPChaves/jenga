using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    StackApiUseCase _stackApi;
    JengaGameGenerator _jengaGameGenerator;
    StackModel _stack;
    GameObject _blockPrefab, _canvasPrefab;
    CameraController _cameraController;
    [SerializeField]
    TMPro.TMP_Text _textUI;

    int currentAnchorIndex = 0;

    override protected void Awake()
    {
        base.Awake();
        _blockPrefab = Resources.Load("Prefabs/JengaBlock") as GameObject;
        _canvasPrefab = Resources.Load("Prefabs/CanvasWorldSpace") as GameObject;
        _stackApi = new StackApiUseCase();
        _jengaGameGenerator = new JengaGameGenerator(_blockPrefab, _canvasPrefab);
        _cameraController = FindObjectOfType<CameraController>();
        GetStackDataFromApi();
    }

    /// <summary>
    /// Get Stack Data From API
    /// </summary>
    void GetStackDataFromApi()
    {
        StartCoroutine(_stackApi.FetchData(
            (stack) => {
                _stack = stack;
                _stack.OrderStack();
                _jengaGameGenerator.GenerateJengaGame(_stack);
                ChangeCameraFocus(0);
            })
        );
    }

    /// <summary>
    /// Change Camera Position to target next or previous stack
    /// If option is less than 0 it goes to the last stack
    /// If option is moer than the numbers of stacks it goes to the first stack
    /// </summary>
    /// <param name="option"></param>
    public void ChangeCameraFocus(int option)
    {
        currentAnchorIndex += option;
        if (currentAnchorIndex > _jengaGameGenerator.stackAnchors.Count - 1) currentAnchorIndex = 0;
        if (currentAnchorIndex < 0) currentAnchorIndex = _jengaGameGenerator.stackAnchors.Count - 1;
        _cameraController.SetTarget(_jengaGameGenerator.stackAnchors[currentAnchorIndex]);
    }

    /// <summary>
    /// Show the block model data on UI and change camera target to the block´s stack
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="blockModel"></param>
    public void ShowInfoOnUI(Transform parent, BlockModel blockModel)
    {
        _textUI.text = blockModel.Description;
        _cameraController.SetTarget(parent);

    }
}
