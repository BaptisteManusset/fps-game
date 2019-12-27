using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCare : Item
{
  private PlayerState ps;
  public int care = 0;

  public override void doAction()
  {
    ps = player.GetComponent<PlayerState>();
    ps.updateHeath(care);
  }
}
