using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ControllerSwitcher : MonoBehaviour
{
    public Dropdown p1Controller;
    public Dropdown p2Controller;

    // Ensure that both p1 and p2 in options menu are valid and not the same
    public void checkControls()
    {
        if (p1Controller.value == p2Controller.value)
        {
            if (p1Controller.value != p2Controller.options.Count -1)
            {
                p2Controller.value++;
            }
            if (p2Controller.value >= p2Controller.options.Count - 1)
            {
                p2Controller.value = 0;
            }
        }
    }
}
