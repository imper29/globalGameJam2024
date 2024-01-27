using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Returns the current difficulty.
    /// </summary>
    /// <remarks>This will be reimplemented later so that difficulty scales more slowly and is reduced when the character loses organs.</remarks>
    /// <returns>The current difficulty.</returns>
    public static float GetDifficulty()
    {
        return Time.time;
    }
}
