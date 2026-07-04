using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetController : MonoBehaviour
{
    // 구글 앱스 스크립트 배포 후 받은 웹 앱 URL을 입력하세요
    private string webAppUrl = "https://docs.google.com/spreadsheets/d/1GFRfLX8hyC6LKDw8_72oMbrrMgDHsy41783jJkuxXjQ/edit?gid=0#gid=0";

    void Start()
    {
        // 테스트용 호출 (필요할 때 원하는 함수를 실행하세요)
          StartCoroutine(CallOverwrite());
         StartCoroutine(CallUpdate());
    }

    // 1. 전체 덮어쓰기 요청 코루틴
    public IEnumerator CallOverwrite()
    {
        DataPacket packet = new DataPacket();
        packet.mode = "overwrite";
        // 덮어씌울 2차원 배열 데이터 정의
        packet.values = new string[][] {
            new string[] { "ID", "Score" },
            new string[] { "UserA", "1500" },
            new string[] { "UserB", "2300" }
        };

        string jsonPayload = JsonUtility.ToJson(packet);
        yield return StartCoroutine(PostRequest(jsonPayload));
    }

    // 2. 특정 데이터 업데이트 요청 코루틴 (UserA의 점수를 9999로 변경)
    public IEnumerator CallUpdate()
    {
        DataPacket packet = new DataPacket();
        packet.mode = "update";
        packet.id = "UserA";        // 찾을 ID
        packet.newValue = "9999";   // 바꿀 값

        string jsonPayload = JsonUtility.ToJson(packet);
        yield return StartCoroutine(PostRequest(jsonPayload));
    }

    // 실제로 구글 서버에 통신을 보내는 공통 함수
    private IEnumerator PostRequest(string jsonPayload)
    {
        using (UnityWebRequest request = new UnityWebRequest(webAppUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"서버 응답: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"에러 발생: {request.error}");
            }
        }
    }
}

// JSON 변환을 위한 데이터 구조 클래스
[Serializable]
public class DataPacket
{
    public string mode;
    public string id;
    public string newValue;
    public string[][] values; // 2차원 배열 데이터 데이터 수신용
}