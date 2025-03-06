using UnityEngine;
using UnityEngine.UI;

public class PostureTestManager : MonoBehaviour
{
    [Header("Posture References")]
    [SerializeField] private PostureGauge playerPosture;
    [SerializeField] private PostureController postureController;
    [SerializeField] private PostureGauge[] enemyPostures;

    [Header("UI Buttons")]
    [SerializeField] private Button blockButton;
    [SerializeField] private Button deflectButton;
    [SerializeField] private Button deathblowButton;

    private void Start()
    {
        blockButton.onClick.AddListener(PlayerBlock);
        deflectButton.onClick.AddListener(PlayerDeflect);
        deathblowButton.onClick.AddListener(ExecuteDeathblow);

        postureController.RegisterPosture(playerPosture);
        foreach (var enemy in enemyPostures)
        {
            postureController.RegisterPosture(enemy);
            enemy.OnPostureBreak += EnableDeathblowButton;
        }
    }

    private void PlayerBlock()
    {
        Debug.Log("Player blocked, taking posture damage.");
        postureController.ApplyBlockDamage(playerPosture);
    }

    private void PlayerDeflect()
    {
        Debug.Log("Player deflected, reducing enemy posture.");
        foreach (var enemy in enemyPostures)
        {
            postureController.ApplyDeflectDamage(playerPosture, enemy);
        }
    }

    private void ExecuteDeathblow()
    {
        Debug.Log("Deathblow executed on all enemies!");
        foreach (var enemy in enemyPostures)
        {
            postureController.HandleDeathblow(enemy);
        }
        deathblowButton.gameObject.SetActive(false);
    }

    private void EnableDeathblowButton()
    {
        Debug.Log("Deathblow button enabled!");
        deathblowButton.gameObject.SetActive(true);
    }
}
