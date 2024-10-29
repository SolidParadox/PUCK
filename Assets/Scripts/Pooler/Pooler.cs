using UnityEngine;

public class Pooler : MonoBehaviour {
  public int size;

  private void Start () {
    for ( int i = 1; i < size; i++ ) {
      GameObject delta = Instantiate ( transform.GetChild ( 0 ).gameObject, transform );
      delta.name = transform.GetChild ( 0 ).name;
      delta.SetActive ( false );
    }
  }

  public GameObject Request () {
    GameObject omega = transform.GetChild ( 0 ).gameObject;
    omega.transform.SetSiblingIndex ( size - 1 );
    omega.SetActive ( true );
    return omega;
  }

  private void Update () {
    if ( Input.GetKeyDown ( KeyCode.Space ) ) {
      Request ();
    }
  }
}
