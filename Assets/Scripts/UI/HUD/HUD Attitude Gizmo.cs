using UnityEngine;

public class HUDAttitudeGizmo : MonoBehaviour {
  public Transform target;
  public Transform display;

  public Quaternion qTrim;

  private void LateUpdate () {
    Vector3 targetEA = target.rotation.eulerAngles;
    
    //targetEA.z = 0;

    display.rotation = Quaternion.Euler ( targetEA ) * qTrim;
  }
}
