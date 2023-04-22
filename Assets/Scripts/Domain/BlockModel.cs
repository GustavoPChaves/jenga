[System.Serializable]
public class BlockModel
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domain;
    public string cluster;
    //JsonUtility has this naming limitation, for simplicity I left it that way. Using json property could solve this.
    public string standardid;
    public string standarddescription;
    public BlockType GetBlockType => (BlockType)mastery;
    public string Description => "Grade Level: " + grade + "\n\nCluster: " + cluster + "\n\nStandard Description: " + standarddescription;

    /// <summary>
    /// Comparing blocks to sort using the priority: domain, cluster and id.
    /// </summary>
    /// <param name="b">Other block to compare with</param>
    /// <returns></returns>
    public int CompareWith(BlockModel b)
    {
        int compareResult = 0;
        compareResult = domain.CompareTo(b.domain);
        if (compareResult != 0) return compareResult;
        compareResult = cluster.CompareTo(b.cluster);
        if (compareResult != 0) return compareResult;
        return id.CompareTo(b.id);
    }
    
}