using System.Collections;
using UnityEngine;

namespace Manager
{
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
            if (instance is not null)
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
            if (instance is not null && instance.currentCoroutine is not null)
            {
                instance.StopCoroutine(instance.currentCoroutine);
            }
        }
    }
}