using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {
  private GameObject vesselGO;
  private int vessel_internal;
  public int vessel {
    get { return vessel_internal; }
    set {
      vessel_internal = value;
      Destroy(vesselGO);
      vesselGO = Instantiate(vessels[vessel_internal]); } }
  private GameObject occupantGO;
  private int occupant_internal;
  public int occupant {
    get { return occupant_internal; }
    set {
      occupant_internal = value;
      Destroy(occupantGO);
      occupantGO = Instantiate(occupants[occupant_internal]);}}
  public List<GameObject> vessels = new List<GameObject>();
  public List<GameObject> occupants = new List<GameObject>();
  
	// Use this for initialization
	void Start () {
    vessel = (int)Random.Range(0, 4);
    occupant = (int)Random.Range(0, 4);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
