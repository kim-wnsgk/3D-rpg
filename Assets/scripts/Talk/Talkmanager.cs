using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Talkmanager : MonoBehaviour
{
    public Dictionary<int, string[]> talkdata;

    private void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        TalkData();
    }

    void TalkData() //대화 내용
    {
        talkdata.Add(100, new string[] { "입단 테스트좀 해볼까?" });
        talkdata.Add(200, new string[] { "평소에 어디서 뛰고 왔는지 모르겠지만 일단 A봇 10마리만 잡아볼래?" });
        talkdata.Add(300, new string[] { "테스트에 거부하더니 이제는 할 마음이 다시 생겼나?" });
        talkdata.Add(400, new string[] { "이 정도 성적이면 출석 점수는 인정해주지" });
        talkdata.Add(500, new string[] { "다음에는 더욱 열심히 하도록" });
        talkdata.Add(600, new string[] { "" });
        talkdata.Add(700, new string[] { "" });
        talkdata.Add(800, new string[] { "" });
        talkdata.Add(900, new string[] { "" });
        talkdata.Add(1000, new string[] { "" });
    }

    public string GetTalkData(int id, int talkindex) // 대화 진행도에 따라 대사, null을 리턴함
    {
        if (talkindex == talkdata[id].Length) // 모든 대사를 보여주면 null을 리턴함 
            return null;
        else
            return talkdata[id][talkindex]; // 대화가 안끝났으면 다음 대사를 리턴함
    }
}
