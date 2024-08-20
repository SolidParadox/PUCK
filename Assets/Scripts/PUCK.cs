using UnityEngine;

public class PUCK : MonoBehaviour {
  public ACS acs;
  public PIDCore pid;
  public SensorCore impactSensor;

  public float impactMinSpeed;

  public float avMaxDelta;

  public float stunDuration;
  private float stunDelta;

  public bool IsStunned { get { return stunDelta > 0; } }

  private float AngleNormalizer ( float past, float current ) {
    int maxC = 10;
    while ( Mathf.Abs ( current - past ) > 180 && maxC > 0 ) {
      current += 360 * Mathf.Sign ( past );
      maxC--;
    }
    return current;
  }

  private Vector2 direction;
  private float deltaAT, deltaAC;

  public void StartStun () {
    stunDelta = stunDuration;
    deltaAT %= 360;
    deltaAC %= 360;
    pid.ResetError ( deltaAT, deltaAC );
  }

  void Update () {
    Vector2 phi = new Vector2 ( Input.GetAxis ( "Horizontal" ), Input.GetAxis ( "Vertical" ) );

    if ( phi.magnitude > 0.1f && !IsStunned ) {
      direction = phi;
      acs.AddThrusterOutput ( direction );
    }

    if ( IsStunned ) {
      stunDelta -= Time.deltaTime;
    } else {
      if ( impactSensor.Breached && acs.rgb.velocity.magnitude > impactMinSpeed ) {
        StartStun ();
      }
    }
  }

  private void FixedUpdate () {
    float aT = Vector2.SignedAngle ( Vector2.up, direction );
    float aC = Vector2.SignedAngle ( Vector2.up, acs.rgb.transform.up );

    aT = AngleNormalizer ( deltaAT, aT );
    aC = AngleNormalizer ( deltaAC, aC );

    Debug.DrawLine ( Vector2.zero, Quaternion.Euler ( 0, 0, aT ) * Vector2.up, Color.red );
    Debug.DrawLine ( Vector2.zero, Quaternion.Euler ( 0, 0, aC ) * Vector2.up, Color.green );
    //sDebug.Log ( aT + " " + aC + " FROM : " + Vector2.SignedAngle( Vector2.up, direction ) + " " + powerTrain.rgb.rotation );

    float deltaA = pid.WorkFunction ( aT, aC, Time.fixedDeltaTime );
    deltaA = Mathf.Clamp ( deltaA, -avMaxDelta, avMaxDelta );
    acs.rgb.AddTorque ( 0, 0, deltaA );

    deltaAT = aT;
    deltaAC = aC;
  }
}
