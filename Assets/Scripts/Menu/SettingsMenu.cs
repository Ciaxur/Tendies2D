using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public bool windowedState = false;

    public void WindowedMode()
    {
        if (!windowedState)
        {
            Debug.Log("Windowed Mode Activated");
            windowedState = !windowedState;
        }
        else
        {
            Debug.Log("Windowed Mode Deactivated");
            windowedState = !windowedState;
        }

    }
}
