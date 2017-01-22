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
      vesselGO = Instantiate(vessels[vessel_internal]);
      vesselGO.transform.parent = transform;
    } }
  private GameObject occupantGO;
  private int occupant_internal;
  public int occupant {
    get { return occupant_internal; }
    set {
      occupant_internal = value;
      Destroy(occupantGO);
      occupantGO = Instantiate(occupants[occupant_internal]);
      occupantGO.transform.parent = transform;
    } }
  public List<GameObject> vessels = new List<GameObject>();
  public List<GameObject> occupants = new List<GameObject>();
  
	// Use this for initialization
	void Start () {
    vessel = (int)Random.Range(0, vessels.Count - 0.00001f);
    occupant = (int)Random.Range(0, occupants.Count - 0.00001f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
