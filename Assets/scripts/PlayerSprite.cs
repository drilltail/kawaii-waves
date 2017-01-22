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
      vesselGO.transform.position += transform.position;
      vesselGO.transform.parent = transform;
      vesselGO.transform.localScale = new Vector3(vesselGO.transform.localScale.x * transform.localScale.x,
                                                  vesselGO.transform.localScale.y * transform.localScale.y,
                                                  vesselGO.transform.localScale.z * transform.localScale.z);
    } }
  private GameObject occupantGO;
  private int occupant_internal;
  public int occupant {
    get { return occupant_internal; }
    set {
      occupant_internal = value;
      Destroy(occupantGO);
      occupantGO = Instantiate(occupants[occupant_internal]);
      occupantGO.transform.position += transform.position;
      occupantGO.transform.parent = transform;
      occupantGO.transform.localScale = new Vector3(occupantGO.transform.localScale.x * transform.localScale.x,
                                                    occupantGO.transform.localScale.y * transform.localScale.y,
                                                    occupantGO.transform.localScale.z * transform.localScale.z);
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
		print(vesselGO.GetComponent<SpriteRenderer>().color);
	}
}
