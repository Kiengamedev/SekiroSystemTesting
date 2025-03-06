using UnityEngine;
using UnityEngine.UI;

public class UICombatTest : MonoBehaviour
{
    [Header("UI Elements")]
    public Button guardButton;
    public Button deflectButton;
    public Button finishButton; // New Finish button
    public Image playerPostureBar;
    public Image enemyPostureBar;

    [Header("Posture System References")]
    public GuardSystem playerGuardSystem;
    public DeflectSystem playerDeflectSystem;
    public PostureSystem playerPosture;
    public PostureSystem enemyPosture;

    private void Start()
    {
        if(guardButton != null) guardButton.onClick.AddListener(() => Debug.Log("Block Button Clicked!"));

        guardButton.onClick.AddListener(TestGuard);
        deflectButton.onClick.AddListener(TestDeflect);
        finishButton.onClick.AddListener(ExecuteFinisher);

        // Hide the Finish button initially
        finishButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePostureBars();
        CheckFinisherCondition();
        CheckPlayerPostureBreak();
    }

    void TestGuard()
    {
        Debug.Log("Testing Guard...");
        playerGuardSystem.StartGuarding();
        playerGuardSystem.ReceiveAttack(10f); // Simulate enemy attacking player
    }

    void TestDeflect()
    {
        bool isPerfectTiming = true; 

        playerDeflectSystem.AttemptDeflect(isPerfectTiming, enemyPosture);
    }

    void ExecuteFinisher()
    {
        Debug.Log("Enemy takes Finish Damage!");
        enemyPosture.ResetPosture();
        finishButton.gameObject.SetActive(false); // Hide after execution
    }

    void CheckFinisherCondition()
    {
        // Show Finish button only when Enemy's Posture is full
        if (enemyPosture.CurrentPosture >= enemyPosture.MaxPosture)
        {
            finishButton.gameObject.SetActive(true);
        }
        else
        {
            finishButton.gameObject.SetActive(false);
        }
    }

    void CheckPlayerPostureBreak()
    {
        // If player's posture is full (from guarding), reset it
        if (playerPosture.CurrentPosture >= playerPosture.MaxPosture && playerGuardSystem.IsGuarding())
        {
            Debug.Log("Player Posture Broken from Guarding! Resetting...");
            playerPosture.ResetPosture();
        }
    }

    void UpdatePostureBars()
    {
        if (playerPostureBar) playerPostureBar.fillAmount = playerPosture.CurrentPosture / playerPosture.MaxPosture;
        if (enemyPostureBar) enemyPostureBar.fillAmount = enemyPosture.CurrentPosture / enemyPosture.MaxPosture;
    }
}
