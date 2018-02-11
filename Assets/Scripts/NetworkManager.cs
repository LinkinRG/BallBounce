using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour {
    [SerializeField] private Text status;
	// Use this for initialization
	private void Start () {
        PhotonNetwork.ConnectUsingSettings("1.0");
	}
	
	// Update is called once per frame
	private void Update () {
		status.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
