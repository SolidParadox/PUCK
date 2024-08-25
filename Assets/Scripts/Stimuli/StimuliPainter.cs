using System.Collections.Generic;
using UnityEngine;

public class StimuliPainter : MonoBehaviour {
  public float range;
  public float angle;

  public int shunt;

  public int raycount;

  public QueryTriggerInteraction triggerInteraction;

  public List<string> hits;

  private void FixedUpdate () {
    float gr = 1.61803398875f;
    hits = new List<string>();
    for ( int i = shunt; i < raycount; i++ ) {
      float t = i / ( raycount - 1f );
      float inclination = Mathf.Acos ( 1 - 2 * t );
      float azimuth = 2 * Mathf.PI * gr * i;

      float x = Mathf.Sin ( inclination ) * Mathf.Cos ( azimuth );
      float y = Mathf.Sin ( inclination ) * Mathf.Sin ( azimuth );
      float z = Mathf.Cos ( inclination );

      Color col = Color.clear;
      Vector3 ray = new Vector3 ( x, y, z ) * range;

      if ( Vector3.Angle ( transform.up, ray ) > angle ) { col = Color.clear; }

      ray = transform.rotation * ray;
      Debug.DrawLine ( transform.position, transform.position + ray, col );

      RaycastHit hit;
      Physics.Raycast ( transform.position, ray, out hit, range, 999, triggerInteraction );
      if ( hit.collider != null ) {
        hits.Add ( hit.transform.name );
      }

      Debug.DrawLine ( hit.point + ( transform.position - hit.point ).normalized * 0.1f, hit.point, Color.blue );
    }
  }
}
