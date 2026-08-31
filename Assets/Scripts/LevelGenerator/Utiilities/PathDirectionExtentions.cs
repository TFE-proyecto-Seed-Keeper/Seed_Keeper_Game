using System;
using System.Linq;

public static class PathDirectionExtentions
{
    public static PathDirectionType[] GetPathDirections(this string sectionName)
    {
        string[] directions = sectionName.Split('_')[0].Select(x => x.ToString()).ToArray();

        PathDirectionType[] result = new PathDirectionType[directions.Length];

        for (int i = 0; i < directions.Length; i++)
        {
            string direction = directions[i];

            if (direction == PathDirections.Left)
            {
                result[i] = PathDirectionType.Left;
            }
            else if (direction == PathDirections.Right)
            {
                result[i] = PathDirectionType.Right;
            }
            else if (direction == PathDirections.Up)
            {
                result[i] = PathDirectionType.Up;
            }
            else if (direction == PathDirections.Down)
            {
                result[i] = PathDirectionType.Down;
            }
            else
            {
                throw new Exception($"Invalid section direction '{direction}'");
            }
        }

        return result;
    }

    public static PathDirectionType GetOpposedDirection(this PathDirectionType direction)
    {
        return direction switch
        {
            PathDirectionType.Left => PathDirectionType.Right,
            PathDirectionType.Right => PathDirectionType.Left,
            PathDirectionType.Up => PathDirectionType.Down,
            PathDirectionType.Down => PathDirectionType.Up,
            _ => throw new Exception($"Invalid '{direction}' PathDirection")
        };
    }

    internal static class PathDirections
    {
        public const string Up = "U";
        public const string Down = "D";
        public const string Left = "L";
        public const string Right = "R";
    }
}
