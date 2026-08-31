public class SectionInfo
{
    public SectionInfo(LevelSection section)
    {
        PathDirectionType[] directions = section.name.GetPathDirections();

        sectionPrefab = section;
        type = section.SectionType;
        startDirection = directions[0];

        if (directions.Length > 1)
        {
            endDirection = directions[1];
        }

        if (directions.Length > 2)
        {
            alternativeDirection = directions[2];
        }
    }

    public SectionType type;
    public PathDirectionType startDirection;
    public PathDirectionType endDirection;
    public PathDirectionType alternativeDirection;
    public LevelSection sectionPrefab;
    public SectionInfo alternativeSectionInfo;
}
