using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMirror : MonoBehaviour
{
  public Transform character;

  void Update()
  {
    Vector3 pos = new Vector3(transform.position.x, transform.position.y, character.position.z);

    transform.position = pos;
  }

}
