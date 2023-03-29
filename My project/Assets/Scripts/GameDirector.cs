using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject character;
    public GameObject flag;
    public GameObject distance;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("characterPivot");
        flag = GameObject.Find("flagPivot");
        distance = GameObject.Find("UIDistance");
        
    }

    // Update is called once per frame
    void Update()
    {
        //float length = flag.transform.position.z - character.transform.position.z;

        float VecterLength = Vector3.Distance(flag.transform.position, character.transform.position);

        // distance.GetComponent<Text>().text = "목표 지점까지 " + length.ToString("F2") + "m";
        distance.GetComponent<Text>().text = "목표 지점까지 " + VecterLength.ToString("F2") + "m";

    }
}
