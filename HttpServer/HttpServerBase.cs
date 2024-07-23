using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Cysharp.Threading.Tasks;

public class HttpServerBase : MonoBehaviour
{
    public enum SendType { GET, POST, PUT, DELETE }

    // 레거시
    [Obsolete("Use Overloaded method instead")]
    protected virtual IEnumerator SendRequestCor(string url, SendType sendType, JObject jobj, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed)
    {
        yield return StartCoroutine(CheckNetwork());

        using (var req = new UnityWebRequest(url, sendType.ToString()))
        {
            Debug.LogFormat("url: {0} \n" +
                "보낸데이터: {1}",
                url,
                JsonConvert.SerializeObject(jobj, Formatting.Indented));

            byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jobj));
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            var result = ResultCheck(req);
            if (result.IsNetworkError)
            {
                onNetworkFailed?.Invoke(result);

                // TODO: 네트워크 재시도 팝업 호출.

                yield return new WaitForSeconds(1f);
                Debug.LogError("재시도");
                yield return StartCoroutine(SendRequestCor(url, sendType, jobj, onSucceed, onFailed, onNetworkFailed));
            }
            else
            {
                if (result.IsSuccess)
                {
                    onSucceed?.Invoke(result);
                }
                else
                {
                    onFailed?.Invoke(result);
                }
            }
        }
    }

    // 빌더패턴 + 코루틴
    protected virtual IEnumerator SendRequestCor(HttpRequestInfo info)
    {
        yield return StartCoroutine(CheckNetwork());

        using (var req = new UnityWebRequest(info.URL, info.SendType.ToString()))
        {
            Debug.LogFormat("url: {0} \n" +
                "보낸데이터: {1}",
                info.URL,
                JsonConvert.SerializeObject(info.Jobj, Formatting.Indented));

            byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(info.Jobj));
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            var result = ResultCheck(req);
            if (result.IsNetworkError)
            {
                info.OnNetworkFailedAction?.Invoke(result);

                if (info.UseRetry)
                {
                    // TODO: 네트워크 재시도 팝업 호출.
                    yield return new WaitForSeconds(1f);
                    Debug.LogError("재시도");
                    yield return StartCoroutine(SendRequestCor(info));
                }
            }
            else
            {
                if (result.IsSuccess)
                {
                    info.OnSucceedAction?.Invoke(result);
                }
                else
                {
                    info.OnFailedAction?.Invoke(result);
                }
            }
        }
    }

    // 빌더패턴 + 유니태스크
    protected async UniTask SendRequest(HttpRequestInfo info)
    {
        await CheckNetwork();

        using (var req = new UnityWebRequest(info.URL, info.SendType.ToString()))
        {
            Debug.LogFormat("url: {0} \n" +
                "보낸데이터: {1}",
                info.URL,
                JsonConvert.SerializeObject(info.Jobj, Formatting.Indented));

            byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(info.Jobj));
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            await req.SendWebRequest();

            var result = ResultCheck(req);
            if (result.IsNetworkError)
            {
                info.OnNetworkFailedAction?.Invoke(result);

                if (info.UseRetry)
                {
                    // TODO: 네트워크 재시도 팝업 호출.
                    await UniTask.Delay(TimeSpan.FromSeconds(1f));
                    Debug.LogError("재시도");
                    await SendRequest(info);
                }
            }
            else
            {
                if (result.IsSuccess)
                {
                    info.OnSucceedAction?.Invoke(result);
                }
                else
                {
                    info.OnFailedAction?.Invoke(result);
                }
            }
        }
    }

    protected virtual IEnumerator CheckNetwork()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // TODO: 네트워크 오류 팝업 호출
            Debug.LogError("네트워크 연결 안됨");

            yield return new WaitUntil(() => Application.internetReachability != NetworkReachability.NotReachable);

            Debug.Log("네트워크 재연결됨");
        }
    }

    protected virtual Result ResultCheck(UnityWebRequest req)
    {
        Result res;
        int code = -1;
        switch (req.result)
        {
            case UnityWebRequest.Result.InProgress:
                res = new Result(req.downloadHandler.text, false, true, "InProgress", code);
                return res;
            case UnityWebRequest.Result.Success:
                JObject jobj = JObject.Parse(req.downloadHandler.text);
                bool isSuccess = int.Parse(jobj["code"].ToString()) == 0 ? true : false;

                Debug.Log(req.downloadHandler.text);
                // 성공
                if (isSuccess)
                {
                    res = new Result(req.downloadHandler.text, true, false, string.Empty, 0);
                    return res;
                }
                // 실패
                else
                {
                    Debug.LogErrorFormat("Error-{0}: {1}", jobj["code"].ToString(), jobj["message"].ToString());
                    int.TryParse(jobj["code"].ToString(), out code);
                    res = new Result(req.downloadHandler.text, false, false,
                        jobj["message"].ToString(), code);
                    return res;
                }
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
            case UnityWebRequest.Result.DataProcessingError:
                // 통신에러
                Debug.LogError(req.error);
                Debug.Log(req.downloadHandler.text);
                res = new Result(req.downloadHandler.text, false, true, req.error, -1);
                return res;
            default:
                Debug.LogError("디폴트 케이스에 걸림");
                Debug.LogError(req.error);
                Debug.Log(req.downloadHandler.text);
                res = new Result(req.downloadHandler.text, false, true, "Unknown", -1);
                return res;
        }
    }

    public class Result
    {
        private string json;
        private bool isSuccess;
        private bool isNetworkError;
        private string errorMsg;
        private int errorCode;

        public string Json => json;
        public bool IsSuccess => isSuccess;
        public bool IsNetworkError => isNetworkError;
        public string Error => string.Format("{0}: {1}", errorCode.ToString(), errorMsg);
        public int ErrorCode { get => errorCode; }

        public JToken ResultData
        {
            get
            {
                return JObject.Parse(json)["data"];
            }
        }

        public Result(string json, bool isSuccess, bool isNetworkError, string errorMsg, int errorCode)
        {
            this.json = json;
            this.isSuccess = isSuccess;
            this.isNetworkError = isNetworkError;
            this.errorMsg = errorMsg;
            this.errorCode = errorCode;
        }
    }
}
