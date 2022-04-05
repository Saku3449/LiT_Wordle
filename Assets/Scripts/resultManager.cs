using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultManager : MonoBehaviour
{
    public Text winner;
    public Text loser;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ParamsScript.scores[0] > ParamsScript.scores[1])
        {
            winner.text = "うみ";
            loser.text = "あみだ";
        }
        else
        {
            winner.text = "あみだ";
            loser.text = "うみ";
        }
    }
}
