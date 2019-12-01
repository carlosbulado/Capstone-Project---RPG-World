using UnityEngine;

public class Dice
{
    public static int Roll(int faces)
    {
        // TODO: Write on the screen the result: "<sprite> rolled D<faces> - <result>"
        return Random.Range(1, faces + 1);
    }

    public static int Roll(int min, int max)
    {
        // TODO: Write on the screen the result: "<sprite> rolled <min>/<max> - <result>"
        return Random.Range(min, max + 1);
    }
}