using UnityEngine;

public class EnemyBrain : MonoBehaviour {
  public SensorCore interestSensor;
  public SensorCore lightSensor;

  public ACS acs;

  private void FixedUpdate () {
    if ( interestSensor.Breached ) {
      acs.AddThrusterOutput ( ( interestSensor.contacts[ 0 ].transform.position - acs.rgb.position ).normalized );
    }
  }
}
