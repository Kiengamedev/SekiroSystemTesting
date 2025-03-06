//using UnityEngine;

public interface IPosture
{
    void AddPosture(float amount, bool canBreakPosture);
    void ReducePosture(float amount);
    bool IsPostureBroken();
    void ResetPosture();
    bool IsAtMaxPosture();
    void LostPosture(bool forceBreak);

    float CurrentPosture { get; }
    float MaxPosture { get; }
}

