using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StackModel
{
    public BlockModel[] blocks;

    public SortedDictionary<string, List<BlockModel>> sortedStack = new SortedDictionary<string, List<BlockModel>>();
   
    /// <summary>
    /// Deconding JSON into StackModel
    /// JsonUtility has a limitation with JSON lists, so adding a named key fix the error.
    /// </summary>
    /// <param name="json">JSON to decode</param>
    /// <returns></returns>
    public static StackModel FromJson(string json)
    {
        return JsonUtility.FromJson<StackModel>("{\"blocks\":" + json + "}");
    }

    /// <summary>
    /// Order the stack by grade, agrouping in a dictionary using grade as key, this structure facilitates the procedural stack generation
    /// And sorting the list of blocks using the following priority: domain, cluster and id
    /// </summary>
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

