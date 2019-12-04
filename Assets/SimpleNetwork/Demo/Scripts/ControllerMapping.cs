using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMapping {

#if UNITY_PS4
    // Buttons
    public static UnityEngine.KeyCode PS4_SQ_BUTTON = KeyCode.JoystickButton2;
    public static UnityEngine.KeyCode PS4_X_BUTTON = KeyCode.JoystickButton0;
    public static UnityEngine.KeyCode PS4_CIR_BUTTON = KeyCode.JoystickButton1;
    public static UnityEngine.KeyCode PS4_TRI_BUTTON = KeyCode.JoystickButton3;
    public static UnityEngine.KeyCode PS4_L1_BUTTON = KeyCode.JoystickButton4;
    public static UnityEngine.KeyCode PS4_R1_BUTTON = KeyCode.JoystickButton5;
    public static UnityEngine.KeyCode PS4_SHR_BUTTON = null;
    public static UnityEngine.KeyCode PS4_OPT_BUTTON = KeyCode.JoystickButton7;
    public static UnityEngine.KeyCode PS4_LBUTTON_BUTTON = KeyCode.JoystickButton8;
    public static UnityEngine.KeyCode PS4_RBUTTON_BUTTON = KeyCode.JoystickButton9;
    public static UnityEngine.KeyCode PS4_PS_BUTTON = null;
    public static UnityEngine.KeyCode PS4_PADPRESS_BUTTON = KeyCode.JoystickButton6, Mouse0;
    
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

    // Buttons
    public static UnityEngine.KeyCode PS4_SQ_BUTTON = KeyCode.JoystickButton0;
    public static UnityEngine.KeyCode PS4_X_BUTTON = KeyCode.JoystickButton1;
    public static UnityEngine.KeyCode PS4_CIR_BUTTON = KeyCode.JoystickButton2;
    public static UnityEngine.KeyCode PS4_TRI_BUTTON = KeyCode.JoystickButton3;
    public static UnityEngine.KeyCode PS4_L1_BUTTON = KeyCode.JoystickButton4;
    public static UnityEngine.KeyCode PS4_R1_BUTTON = KeyCode.JoystickButton5;
    public static UnityEngine.KeyCode PS4_L2_BUTTON = KeyCode.JoystickButton7;
    public static UnityEngine.KeyCode PS4_R2_BUTTON = KeyCode.JoystickButton7;
    public static UnityEngine.KeyCode PS4_SHR_BUTTON = KeyCode.JoystickButton8;
    public static UnityEngine.KeyCode PS4_OPT_BUTTON = KeyCode.JoystickButton9;
    public static UnityEngine.KeyCode PS4_LBUTTON_BUTTON = KeyCode.JoystickButton10;
    public static UnityEngine.KeyCode PS4_RBUTTON_BUTTON = KeyCode.JoystickButton11;
    public static UnityEngine.KeyCode PS4_PS_BUTTON = KeyCode.JoystickButton12;
    public static UnityEngine.KeyCode PS4_PADPRESS_BUTTON = KeyCode.JoystickButton13;
    public static string RightStickHorizontal = "WINPS4RightStickHorizontal";
    public static string RightStickVertical = "WINPS4RightStickVertical";


#endif
}
