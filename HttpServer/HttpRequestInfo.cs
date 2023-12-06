using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class HttpRequestInfo
{
    // url
    public string URL { get; private set; }
    // 전송 메소드
    public HttpServerBase.SendType SendType { get; private set; }
    // json body
    public JObject Jobj { get; private set;  }    
    // 실패시 재시도 팝업 노출 여부
    public bool UseRetry { get; set; }

    // 액션들
    public Action<HttpServerBase.Result> OnSucceedAction { get; private set; }
    public Action<HttpServerBase.Result> OnFailedAction { get; private set; }
    public Action<HttpServerBase.Result> OnNetworkFailedAction { get; private set; }

    private HttpRequestInfo(Builder builder)
    {
        this.URL = builder.URL;
        this.SendType = builder.SendType;
        this.Jobj = builder.Jobj;
        this.UseRetry = builder.UseRetry;
        this.OnSucceedAction = builder.OnSucceedAction;
        this.OnFailedAction = builder.OnFailedAction;
        this.OnNetworkFailedAction = builder.OnNetworkFailedAction;
    }

    public class Builder
    {
        // url
        public string URL { get; private set; }
        // 전송 메소드
        public HttpServerBase.SendType SendType { get; private set; }
        // json body
        public JObject Jobj { get; private set; }
        // 실패시 재시도 팝업 노출 여부
        public bool UseRetry { get; set; }

        // 액션들
        public Action<HttpServerBase.Result> OnSucceedAction { get; private set; }
        public Action<HttpServerBase.Result> OnFailedAction { get; private set; }
        public Action<HttpServerBase.Result> OnNetworkFailedAction { get; private set; }

        public Builder()
        {
            URL = string.Empty;
            SendType = HttpServerBase.SendType.POST;
            Jobj = null;
            UseRetry = true;
        }

        public Builder SetUrl(string url)
        {
            this.URL = url;
            return this;
        }

        public Builder SetJobj(JObject jobj)
        {
            this.Jobj = jobj;
            return this;
        }

        public Builder SetUseRetry(bool use)
        {
            UseRetry = use;
            return this;
        }

        public Builder SetAllAction(Action<HttpServerBase.Result> onSucceed, Action<HttpServerBase.Result> onFailed, Action<HttpServerBase.Result> onNetworkFailed)
        {
            this.OnSucceedAction = onSucceed;
            this.OnFailedAction = onFailed;
            this.OnNetworkFailedAction = onNetworkFailed;

            return this;
        }

        public Builder SetSendType(HttpServerBase.SendType sendType)
        {
            this.SendType = sendType;

            return this;
        }

        public HttpRequestInfo Build()
        {
            return new HttpRequestInfo(this);
        }
    }
}
