using UnityEngine;

public class SUPPGraph : DataPlotter {
  public Transform target;
  public Vector3 weights;
  public override void LateUpdate () {
    sample = Vector3.Scale ( target.position, weights ).magnitude;
    base.LateUpdate ();
  }
}
