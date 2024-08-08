using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPV : DataPlotter
{
  public Rigidbody2D rgb;
  public override void LateUpdate () {
    sample = rgb.velocity.magnitude;
    base.LateUpdate ();
  }
}
