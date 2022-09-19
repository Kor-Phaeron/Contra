using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions
{
    public bool isCollidingBelow { get; set; }
    public bool isFalling { get; set; }
    public bool isJumping { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isShooting { get; set; }
    public void ResetConditions()
    {
        isCollidingBelow = false;
        isFalling = false;
    }
}
