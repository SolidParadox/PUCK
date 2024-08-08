using UnityEngine;

// Simply checks for collisions
public class RCol : RadarCore {
  private void OnCollisionEnter2D ( Collision2D collision ) {
    AddContact ( collision.gameObject );
  }
  private void OnCollisionExit2D ( Collision2D collision ) {
    RemoveContact ( collision.gameObject );
  }
}
