using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchKeyBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick1()
    {
        GameObject player1 = gameObject.transform.Find("Player1").gameObject;

        Vector3 tmp = player1.transform.Find("KeyBoard").gameObject.transform.position;
        player1.transform.Find("KeyBoard").gameObject.transform.position = player1.transform.Find("KeyBoardEx").gameObject.transform.position;
        player1.transform.Find("KeyBoardEx").gameObject.transform.position = tmp;
    }

    public void onClick2()
    {
        GameObject player2 = gameObject.transform.Find("Player2").gameObject;

        Vector3 tmp = player2.transform.Find("KeyBoard").gameObject.transform.position;
        player2.transform.Find("KeyBoard").gameObject.transform.position = player2.transform.Find("KeyBoardEx").gameObject.transform.position;
        player2.transform.Find("KeyBoardEx").gameObject.transform.position = tmp;
    }
}
