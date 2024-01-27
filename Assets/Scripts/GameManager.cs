using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// Returns the current difficulty between zero and one inclusive.
    /// </summary>
    /// <remarks>This will be reimplemented later so that difficulty scales more slowly and is reduced when the character loses organs.</remarks>
    /// <returns>The current difficulty.</returns>
    public static float GetDifficulty()
    {
        //Reaches maximum difficulty in 3 minutes.
        return Mathf.Clamp01((Time.time - startTime) / 300.0f);
    }
}
