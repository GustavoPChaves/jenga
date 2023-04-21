[System.Serializable]
public class BlockModel
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
    public BlockType GetBlockType => (BlockType)mastery;
    public string Description => "Grade Level: " + grade + "\n\nCluster: " + cluster + "\n\nStandard Description: " + standarddescription;

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