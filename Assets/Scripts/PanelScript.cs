using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PanelData
{
    public float top = 5000;
    public float bottom = 3000;
}

public class PanelScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public float top;
    public float bottom;

    string json = "";

    void Update()
    {
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, top);
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, bottom);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            PanelData panelData = new PanelData();

            panelData.top = top;
            panelData.bottom = bottom;

            json = JsonUtility.ToJson(panelData);
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
            PanelData panelData = JsonUtility.FromJson<PanelData>(json);

            top = panelData.top;
            bottom = panelData.bottom;
        }
    }
}
