using UnityEngine;

public class RRays : RadarCore {
  public float deltaAngle;
  public int rayCount;
  public float distance;

  public ContactFilter2D contactFilter;
  private RaycastHit2D[] rc;

  public Vector2[] points;
  public float[] pointDistance;

  public bool overrideFUW = false;    // Cutoff Fixed Update Work Function

  private void Start () {
    points = new Vector2[rayCount];
    pointDistance = new float[rayCount];
  }

  private void FixedUpdate () {
    if ( !overrideFUW ) {
      WorkFunction ();
    }
  }

  public void WorkFunction () {
    Quaternion had = Quaternion.Euler (0,0,deltaAngle );  // Heading angle delta
    Vector2 heading = Quaternion.Euler (0,0,-deltaAngle * rayCount / 2 ) * transform.up;
    contacts.Clear ();

    for ( int i = 0; i < rayCount; i++ ) {
      rc = new RaycastHit2D[8];
      int hc = Physics2D.Raycast ( transform.position, heading, contactFilter, rc, distance );
      int targetJ = -1;
      for ( int j = 0; j < hc; j++ ) {
        if ( CheckContact ( rc[j].collider.gameObject ) ) {
          // In the past, radar was using check contact, which had a clause for not adding already present elements, WHICH WERE ADDED RIGHT BELOW -> always pass through
          AddContact ( rc[j].collider.gameObject );
          targetJ = j;
          break;
        }
      }
      if ( targetJ == -1 ) {
        points[i] = heading * distance;
        pointDistance[i] = distance;
      } else {
        points[i] = heading * rc[targetJ].distance;
        pointDistance[i] = rc[targetJ].distance;
      }
      //Debug.DrawLine ( Vector3.position, points[i] + (Vector2) transform.position, Color.yellow );
      heading = had * heading;
    }
  }

  public float GetDistance ( int rayID  ) {
    return points[rayID].magnitude / distance;
  }
}
