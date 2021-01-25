using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerToken 
{
    public enum controller { left, right, none };
    public enum button
    { primary, secondary, trigger, grip, menu, joystickClick };

    public controller c;
    public button b;

}
