using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

[System.Serializable] //同期のためのクラス
public class CanvasData
{
    public string[] texts;
    public ParamsScript paramsScript;
}

public class KeywordGridController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    GameObject keyPrefab;
    [SerializeField]
    Sprite keyGreen;
    [SerializeField]
    Sprite keyOrange;
    [SerializeField]
    Sprite keyBlack;
    [SerializeField]
    Sprite keyWhite;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject panelCanvas;
    PanelScript panelScript;

    GameObject param;
    ParamsScript paramsScript;

    GameObject parent;

    GameObject canvas;


    string json = "";

    //string answer = "あいうえお";
    string[] answers = new string[] { "めいじょう", "あんどろい", "うまむすめ", "かいじゅう", "はっかそん", "", ""};

    GameObject[] keys = new GameObject[30];
    int keyRow = 0;
    int keyColumn = 0;

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            keys[i] = Instantiate(keyPrefab, transform);
            keys[i].GetComponent<Image>().sprite = keyWhite;
            keys[i].transform.GetChild(0).GetComponent<Text>().text = "";
        }
        param = GameObject.Find("Params");
        if(param != null)
        {
            paramsScript = param.GetComponent<ParamsScript>();
        }
        parent = transform.parent.gameObject;
    }

    private void Update()
    {
        param = GameObject.FindWithTag("prm");
        paramsScript = param.GetComponent<ParamsScript>();
        scoreText.text = "Score : " + ParamsScript.scores[int.Parse(parent.gameObject.tag) - 1];

        panelCanvas = GameObject.Find("PanelCanvas(Clone)");
        panelScript = panelCanvas.transform.GetChild(0).GetComponentInParent<PanelScript>();
    }

    public void OnClickKeyButton(string key)
    {
        if (keyColumn < 5)
        {
            keys[5 * keyRow + keyColumn].transform.GetChild(0).GetComponent<Text>().text = key;
            keyColumn++;
        }
    }

    public void OnClickEnter()
    {
        if(keyColumn < 5)
        {
            NotEnough();
        }
        else
        {
            string input = "";
            for(int i = 0; i < 5; i++)
            {
                input += keys[5 * keyRow + i].transform.GetChild(0).GetComponent<Text>().text;
            }
            if (input == answers[paramsScript.turn])
            {
                for (int i = 0; i < 5; i++)
                {
                    char key_ = input[i];
                    for (int j = 0; j < 5; j++)
                    {
                        if (key_ == answers[paramsScript.turn][j])
                        {
                            if (i == j)
                            {
                                TurnGreen(keys[5 * keyRow + i]);
                                break;
                            }
                            else
                            {
                                TurnOrange(keys[5 * keyRow + i]);
                                break;
                            }

                        }
                        else
                        {
                            TurnBlack(keys[5 * keyRow + i]);
                        }
                    }
                }
                Clear(int.Parse(parent.gameObject.tag));
                StartCoroutine(NextGame());
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    char key_ = input[i];
                    for (int j = 0; j < 5; j++)
                    {
                        if (key_ == answers[paramsScript.turn][j])
                        {
                            if (i == j)
                            {
                                TurnGreen(keys[5 * keyRow + i]);
                                break;
                            }
                            else
                            {
                                TurnOrange(keys[5 * keyRow + i]);
                                break;
                            }

                        }
                        else
                        {
                            TurnBlack(keys[5 * keyRow + i]);
                        }
                    }
                }
                keyColumn = 0;
                keyRow++;
            }
        }
    }

    public void OnClickBack()
    {
        if(keyColumn > 0)
        {
            keyColumn--;
            keys[5 * keyRow + keyColumn].transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    void TurnOrange(GameObject key)
    {
        key.GetComponent<Image>().sprite = keyOrange;
    }
    void TurnGreen(GameObject key)
    {
        key.GetComponent<Image>().sprite = keyGreen;
    }
    void TurnBlack(GameObject key)
    {
        key.GetComponent<Image>().sprite = keyBlack;
    }


    void Clear(int tag)
    {
        Debug.Log("やったあ！");
        paramsScript.turn++;
        ParamsScript.scores[tag-1]++;
        //panelScript.top = 0;
        panelScript.bottom = -2000;
    }

    void NotEnough()
    {
        Debug.Log("足りないぜ");
    }

    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2);

        if(paramsScript.turn > 4)
        {
            SceneManager.LoadScene("result");
        }

        //panelScript.top = 2000;
        panelScript.bottom = 2000;

        for (int i = 0; i < 30; i++)
        {
            keys[i].transform.GetChild(0).GetComponent<Text>().text = "";
            keys[i].GetComponent<Image>().sprite = keyWhite;
        }
        keyRow = 0;
        keyColumn = 0;

        scoreText.text = "Score : " + ParamsScript.scores[int.Parse(parent.gameObject.tag) - 1];
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            CanvasData canvasData = new CanvasData();
            string[] txts = new string[30];
            for(int i = 0; i < txts.Length; i++)
            {
                txts[i] = keys[i].transform.GetChild(0).GetComponent<Text>().text;
            }

            canvasData.texts = txts;
            canvasData.paramsScript = paramsScript;

            json = JsonUtility.ToJson(canvasData);
            bool isNull = false;
            for(int i = 0; i < 30; i++)
            {
                if (keys[i] == null)
                    isNull = true;
            }
            if(isNull == false)
                stream.SendNext(json);
        }
        else
        {
            this.json = (string)stream.ReceiveNext();
            CanvasData canvasData = JsonUtility.FromJson<CanvasData>(json);
            for (int i = 0; i < 30; i++)
            {
                if(canvasData.texts[i] != null && keys[i] != null)
                    keys[i].transform.GetChild(0).GetComponent<Text>().text = canvasData.texts[i];
            }
        }
    }
}
