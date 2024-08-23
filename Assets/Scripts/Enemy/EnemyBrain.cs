using UnityEngine;

public class EnemyBrain : MonoBehaviour {
  public SensorCore interestSensor;
  public SensorCore lightSensor;

  public ACS acs;

  private void FixedUpdate () {
    if ( interestSensor.Breached ) {
      acs.AddThrusterOutput ( (Vector2) ( ( interestSensor.contacts[0].transform.position - acs.rgb.position ).normalized ) );
      Debug.DrawLine ( acs.rgb.position, interestSensor.contacts[0].transform.position, Color.red );
    }
  }
}
