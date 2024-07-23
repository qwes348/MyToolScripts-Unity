using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oni.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

public class AuctionServerManager : HttpServerBase
{
    public static AuctionServerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Coroutine GetOffers(string uuid, int[] costMinMax, int[] elementAttribues, int[] atkMinMax, int[] defMinMax, int[] hpMinMax,
        Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.GetOffersPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["costMinAndMax"] = JArray.FromObject(costMinMax);
        jobj["allowedAttributes"] = JArray.FromObject(elementAttribues);
        jobj["atkMinAndMax"] = JArray.FromObject(atkMinMax);
        jobj["defMinAndMax"] = JArray.FromObject(defMinMax);
        jobj["hpMinAndMax"] = JArray.FromObject(hpMinMax);

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine GetOffersRecentAll(string uuid, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.GetOffersPath;

        int[] emptyArray = new int[0];

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["costMinAndMax"] = JArray.FromObject(emptyArray);
        jobj["allowedAttributes"] = JArray.FromObject(emptyArray);
        jobj["atkMinAndMax"] = JArray.FromObject(emptyArray);
        jobj["defMinAndMax"] = JArray.FromObject(emptyArray);
        jobj["hpMinAndMax"] = JArray.FromObject(emptyArray);

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine GetOfferById(string uuid, int auctionID, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.GetOfferByIdPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["auctionID"] = auctionID;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine SendOffer(string uuid, string cardUid, int expireMin,
       Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.OfferPath;

        var expireTime = DateTime.UtcNow;
        expireTime = expireTime.AddMinutes(expireMin);

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["userCardUID"] = cardUid;
        jobj["expireAt"] = expireTime.ToString("O");

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine GetMyBids(string uuid, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.GetMyBidsPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine GetMyOffers(string uuid, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.GetMyOffersPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine SendBid(string uuid, int bid, int auctionID, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.BidPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["bid"] = bid;
        jobj["auctionID"] = auctionID;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }    

    public Coroutine Claim(string uuid, int auctionID, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.ClaimPath;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["auctionID"] = auctionID;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine FinalizeAuction(string uuid, int auctionID, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.FinalizeAuction;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["auctionID"] = auctionID;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }

    public Coroutine RestoreBidFailedCard(string uuid, int auctionID, Action<Result> onSucceed, Action<Result> onFailed, Action<Result> onNetworkFailed = null)
    {
        string url = GameURL.AuctionServer.ServerUrl + GameURL.AuctionServer.RestoreBidFailedCard;

        JObject jobj = new JObject();
        jobj["userUID"] = uuid;
        jobj["auctionID"] = auctionID;

        HttpRequestInfo info = new HttpRequestInfo.Builder()
            .SetUrl(url)
            .SetJobj(jobj)
            .SetAllAction(onSucceed, onFailed, onNetworkFailed)
            .SetSendType(SendType.POST)
            .SetUseRetry(true)
            .Build();

        return StartCoroutine(SendRequestCor(info));
    }
}
