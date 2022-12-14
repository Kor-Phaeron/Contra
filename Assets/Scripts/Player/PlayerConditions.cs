using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions
{
    public bool isCollidingBelow { get; set; }
    public bool isIdle { get; set; }
    public bool isFalling { get; set; }
    public bool isJumping { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isShootingAndRunningForward { get; set; }
    public bool isShootingAndRunningUpward { get; set; }
    public bool isShootingAndRunningDownward { get; set; }
    public bool isShootingUp { get; set; }
    public bool isShootingLying { get; set; }
    public void ResetConditions()
    {
        isCollidingBelow = false;
        isFalling = false;
    }
}
