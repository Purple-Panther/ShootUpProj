using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowDebugPanel : MonoBehaviour
{
    private Toggle panelState;
    [SerializeField]
    private GameObject panel;

    private void Awake()
    {
        panelState = GetComponent<Toggle>();
    }

    private void Start()
    {
        CheckToggleState();

        panelState.onValueChanged.AddListener( _ => CheckToggleState());
    }

    private void CheckToggleState()
    {
        if(panelState.isOn)
            panel.SetActive(true);
        else
            panel.SetActive(false);
    }
}
