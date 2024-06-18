using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        instance = this;
    }

    public static void StartRoutine(IEnumerator routine)
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.InternalStartRoutine(routine));
        }
        else
        {
            Debug.LogError("CoroutineManager instance is null, cannot start routine.");
        }
    }

    private IEnumerator InternalStartRoutine(IEnumerator routine)
    {
        currentCoroutine = StartCoroutine(routine);
        yield return currentCoroutine;
    }

    public static void StopRoutine()
    {
        if (instance != null && instance.currentCoroutine != null)
        {
            instance.StopCoroutine(instance.currentCoroutine);
        }
    }
}