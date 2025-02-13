public interface IPostureSystem
{
    float MaxPosture { get; }
    float CurrentPosture { get; set; }
    bool IsPostureBroken { get; }

    void TakePostureDamage(float amount, bool canBreak);
    void RecoverPosture(float amount);
    void BreakPosture();
    void ResetPosture();
}
