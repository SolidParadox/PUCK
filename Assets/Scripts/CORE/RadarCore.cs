using UnityEngine;
using System.Collections.Generic;

public abstract class RadarCore : MonoBehaviour {
  public List<GameObject> contacts = new List<GameObject>();
  public bool breached { get; protected set; }

  public string[] targetTags;
  public enum TagMode { Free, Exclude, Include };
  public TagMode tagMode;

  public bool changeLastFrame = false;

  protected virtual void LateUpdate () {
    CheckDeadContacts ();
  }
  private void CheckDeadContacts () {
    for ( int i = 0; i < contacts.Count; i++ ) {
      if ( contacts[i] == null ) {
        contacts.RemoveAt ( i );
        breached = contacts.Count != 0;
        changeLastFrame = true;
        i--;
      }
    }
  }

  public bool CheckTag ( string alpha ) {
    if ( tagMode == TagMode.Free ) { return true; }
    for ( int i = 0; i < targetTags.Length; i++ ) {
      if ( tagMode == TagMode.Include && alpha == targetTags[i] ) {
        return true;
      }
      if ( tagMode == TagMode.Exclude && alpha == targetTags[i] ) {
        return false;
      }
    }
    return tagMode == TagMode.Exclude;
  }

  public bool CheckContact ( GameObject contact ) {
    return CheckTag ( contact.tag );
  }

  public void AddContact ( GameObject contact ) {
    if ( CheckTag ( contact.tag ) ) {
      if ( !contacts.Contains ( contact ) ) {
        breached = true;
        contacts.Add ( contact );
        changeLastFrame = true;
      }
    }
  }

  public void RemoveContact ( GameObject contact ) {
    if ( contacts.Contains ( contact ) ) {
      contacts.Remove ( contact );
      breached = contacts.Count != 0;
      changeLastFrame = true;
    }
  }
}
