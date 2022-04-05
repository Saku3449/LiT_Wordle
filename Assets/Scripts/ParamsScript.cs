using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable] //同期のためのクラス
public class ParamsData
{
    public int turn;
    public int[] scores;
}

public class ParamsScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public int turn = 0;
    public static int[] scores = { 0, 0 };
    string json = "";

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            ParamsData paramsData = new ParamsData();

            paramsData.turn = turn;
            paramsData.scores = scores;

            json = JsonUtility.ToJson(paramsData);
            //bool isNull = false;
            //for (int i = 0; i < 30; i++)
            //{
            //    if (keys[i] == null)
            //        isNull = true;
            //}
            //if (isNull == false)
            stream.SendNext(json);
        }
        else
        {
            this.json = (string)stream.ReceiveNext();
            ParamsData paramsData = JsonUtility.FromJson<ParamsData>(json);

            turn = paramsData.turn;
            scores = paramsData.scores;
        }
    }

    public static int[] getScores()
    {
        return scores;
    }
    public static void addScore(int idx, int val)
    {
        scores[idx] = val;
    }

}
