using System.Security.Cryptography;
using UnityEngine;

// Attitude Control System -> MIKA 2
public class ACS : MonoBehaviour {
  public  Rigidbody       rgb;

  public  float           acc;
  public  float           mxv;

  public  float           strengthDOS;      // DRAG for OVERSPEED
  public  float           strengthD;

  protected Vector2       delta;
  protected Vector2       backupDelta;
  protected int               ticks;

  protected bool              vLock;
  
  public EnergyUser       energyUser;
  public float energyAccMul;
  
  public Vector3 eccentricPosition; // Eccentric Position

  public static Vector2 Cutter ( Vector2 a, Vector2 b, float m ) {
    if ( a.magnitude < 0.0001f ) return Vector2.zero;
    if ( ( a + b ).magnitude >= m ) {
      if ( Vector2.Dot ( a, b ) < 0 ) { return a.normalized * Mathf.Min ( a.magnitude, m ); }
      if ( b.magnitude > m ) { b = b.normalized * m; }
      return ( b + a ).normalized * m - b;
    }
    return a;
  }

  virtual public void AddThrusterOutput ( Vector2 a ) {
    delta += a;
  }

  virtual public void SetThrusterOutput ( Vector2 a ) {
    backupDelta = a;
    vLock = true;
  }

  virtual public void Start () {
    if ( rgb == null ) {
      rgb = GetComponent<Rigidbody> ();
    }
  }

  virtual public void Update () {
    ticks++;
  }

  virtual public void FixedUpdate () {
    if ( ticks == 0 || vLock ) { delta = backupDelta; } else { delta /= ticks; backupDelta = delta; }
    vLock = false;

    if ( delta.sqrMagnitude > 1 ) {
      delta.Normalize ();
    }
    energyUser.draw = Mathf.Abs ( delta.magnitude * energyAccMul );

    float umv = Mathf.Max ( 0, Mathf.Abs ( delta.magnitude ) ) * mxv;

    rgb.velocity -= rgb.velocity * strengthD * Time.fixedDeltaTime;
    if ( rgb.velocity.magnitude > umv ) {
      rgb.velocity -= ( rgb.velocity - rgb.velocity.normalized * umv ) * strengthDOS * Time.fixedDeltaTime;
    }

    Vector2 deltaProcessed = Cutter( delta * energyUser.drawEfficiency * acc * Time.fixedDeltaTime, rgb.velocity, umv * delta.magnitude );

    Vector3 appliedForce = deltaProcessed * rgb.mass / Time.fixedDeltaTime;

    rgb.AddForceAtPosition ( appliedForce, rgb.position + eccentricPosition );
   
    ResetDeltas ();
  }

  protected virtual void ResetDeltas () {
    delta = Vector2.zero;
    ticks = 0;
  }

  protected virtual void OnEnable () {
    ResetDeltas ();
  }
}
