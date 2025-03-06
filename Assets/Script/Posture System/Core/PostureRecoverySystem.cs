using System.Collections;
using UnityEngine;

public class PostureRecoverySystem : MonoBehaviour
{
    [SerializeField] private PostureGauge posture;
    [SerializeField] private PostureDataSO postureData;

    private bool canRecover = true;
    private Coroutine recoveryCoroutine;

    private void Start()
    {
        posture.OnPostureBreak += DisableRecovery;
        posture.OnPostureRecover += EnableRecovery;
        posture.OnPostureChanged += ResetRecoveryDelay;
    }

    private void DisableRecovery() => canRecover = false;
    private void EnableRecovery() => canRecover = true;

    private void ResetRecoveryDelay()
    {
        if (recoveryCoroutine != null)
        {
            StopCoroutine(recoveryCoroutine); 
        }
        Debug.Log($"{posture.gameObject.name} Posture Damaged! Restarting Recovery Timer...");
        recoveryCoroutine = StartCoroutine(RecoverPosture());
    }

    private IEnumerator RecoverPosture()
    {
        yield return new WaitForSeconds(postureData.recoveryDelay); 

        while (canRecover && posture.CurrentPosture > 0 && !posture.IsPostureBroken())
        {
            posture.ReducePosture(postureData.recoveryRate * Time.deltaTime);
            //Debug.Log($"{posture.gameObject.name} Recovering... Posture: {posture.CurrentPosture}/{posture.MaxPosture}");
            yield return null;
        }
    }
}
