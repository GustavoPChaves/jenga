using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StackModel
{
    public BlockModel[] blocks;

    public SortedDictionary<string, List<BlockModel>> sortedStack = new SortedDictionary<string, List<BlockModel>>();
   
    public static StackModel FromJson(string json)
    {
        return JsonUtility.FromJson<StackModel>("{\"blocks\":" + json + "}");
    }

    public void OrderStack()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if(sortedStack.ContainsKey(blocks[i].grade)){
                sortedStack[blocks[i].grade].Add(blocks[i]);
            }
            else
            {
                var newList = new List<BlockModel>();
                newList.Add(blocks[i]);
                sortedStack.Add(blocks[i].grade, newList);
            }
        }
        foreach (var key in sortedStack.Keys)
        {
            sortedStack[key].Sort( (a, b) => a.CompareWith(b));
        }
    }
}

