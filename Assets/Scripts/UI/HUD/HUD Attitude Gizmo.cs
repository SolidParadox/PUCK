using UnityEngine;

public class HUDAttitudeGizmo : MonoBehaviour {
  public Transform target;
  public Transform display;

  private void LateUpdate () {
    Vector3 targetEA = target.rotation.eulerAngles;

    //targetEA.z = 0;
    float change = targetEA.y;
    targetEA.y = targetEA.z;
    targetEA.z = change;

    display.rotation = Quaternion.Euler ( targetEA );
  }
}
