using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oni.Server
{
    public class GameURL
    {
        // API Docs
        // https://documenter.getpostman.com/view/20602451/2s9YeLYVLL

        /// <summary>
        /// 유저관련 API와.. 카테고리를 나누기엔 너무 작은 API들...
        /// </summary>
        public static class UserServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42530";

            public static readonly string SignUpPath = "/user/signUp";

            public static readonly string LoginPath = "/user/login";

            public static readonly string GetLeaderBoardPath = "/user/getLeaderboard";

            public static readonly string GetMyRankingPath = "/user/getMyRanking";

            public static readonly string SessionAliveCheckPath = "/user/whoami";

            public static readonly string GetConstantsPath = "/constants";

            // 재화의 타입id 받아오기
            public static readonly string GetCurrenciesPath = "/currency/getCurrencies";

            public static readonly string GetUserCurrenciesPath = "/currency/getUserCurrencies";

            // 개발용 재화 소매넣기
            public static readonly string Dev_AddUserCurrency = "/currency/dev-addUserCurrency";

            public static readonly string GetSeasonInfoPath = "/season/getSeasonInfo";

            public static readonly string GetPVEsPath = "/pve/getPVEs";

            public static readonly string ChangeUserNamePath = "/user/changeUsername";
        }

        public static class AuctionServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42530/auction";

            // 경매 목록 조회
            public static readonly string GetOffersPath = "/getOffers";

            // 입찰하기
            public static readonly string BidPath = "/bid";

            // 출품하기
            public static readonly string OfferPath = "/offer";

            // 내 입찰목록 조회
            public static readonly string GetMyBidsPath = "/getMyBids";

            // 내 출품목록 조회 
            public static readonly string GetMyOffersPath = "/getMyOffers";

            // 한 매물 ID로 조회
            public static readonly string GetOfferByIdPath = "/getOfferByID";

            // 결과 확인
            public static readonly string ClaimPath = "/claim";

            // 낙찰 실패카드 돌려받기
            public static readonly string RestoreBidFailedCard = "/restoreBidFailedCard";

            // 낙찰하기
            public static readonly string FinalizeAuction = "/finalizeAuction";
        }

        public static class CardManageServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42531";

            #region 설명
            /**
             * desc     |qr코드를 이용한 카드 생성
             * url      |{host}:42531/card/gen
             * input    |qr_code        |string     |qr코드 원본 값
             * output   |code           |string     |ok, error: message
             *          |data           |json       |
             *          ||uid           |string     |카드 고유값
             *          ||image_url     |string     |카드 이미지 url
             *          ||name          |string     |카드 이름
             *          ||rating        |string     |
             *          ||cost          |int        |
             *          ||attribute     |int        |
             *          ||hp            |int        |
             *          ||atk           |int        |
             *          ||def           |int        |
             *          ||hp_coin       |int        |
             *          ||atk_coin      |int        |
             *          ||def_coin      |int        |
             *          ||ability_type
             *          ||ability_level
             */
            #endregion
            public static readonly string GenerateCardPath = "/card/gen";

            // 에셋번들에서 뽑아오기 전용(이미지 url대신 파일 이름을 줌)
            public static readonly string GenerateCard2Path = "/card/gen2";

            #region 설명
            /**
             * desc     |카드 확정 시 이미지 저장
             * url      |{host}:42531/set_card
             * input    |form-data
             *          ||uid       |string         |카드 고유값
             *          ||file      |image          |카드 이미지
             * output   |code       |string         |ok, message
             */
            #endregion
            public static readonly string SaveCardPath = "/card/save";

            #region 설명
            /**
             * desc     |내가 가진 카드 불러오기
             * url      |{host}:42531/card/load
             * input    |user_uid       |string         |사용자 고유값
             * output   |code           |string         |ok, message
             *          |data           |array object
             *          ||uid
             *          ||name
             *          ||rating
             *          ||cost
             *          ||attribute
             *          ||hp
             *          ||atk
             *          ||def
             *          ||hp_coin
             *          ||atk_coin
             *          ||def_coin
             *          ||ability_type
             *          ||ability_level
             *          ||image_url
             */
            #endregion
            public static readonly string GetMyCardsPath = "/card/load";

            #region 설명
            /**
             * desc     |내가 가진 카드 삭제
             * url      |{host}:42531/card/delete
             * input    |user_uid       |string         |사용자 고유값
             *          |card_uid       |string         |카드 고유값
             * output   |code           |string         |ok, message
             * 응답 예시
             * {
                "code": "ok",
                "data": {
                    "sell_price": 800,
                    "user_currencies": [
                        {
                            "currency_id": 1,
                            "amount": 9450
                        },
                        {
                            "currency_id": 2,
                            "amount": 28489
                        },
                        {
                            "currency_id": 3,
                            "amount": 10
                        }
                    ]
                }
            }
             */
            #endregion
            public static readonly string DeleteCardPath = "/card/delete";

            #region 설명
            /**
             * desc     |카드 획득
             * url      |{host}:42531/card/save
             * input    |user_uid       |string         |사용자 고유값
             *          |card_uid       |string         |카드 고유값
             * output   |code           |string         |ok, message
             */
            #endregion
            public static readonly string SaveDeckPath = "/deck/save";

            #region 설명
            /**
             * desc     |카드 덱 불러오기
             * url      |{host}:42531/load_deck
             * input    |user_uid           |string         |사용자 고유값
             * output   |code               |string         |ok, message
             *          |data               |array object
             *          |uid                |string         |카드덱 고유값
             *          |cost
             *          |name
             *          |cards              |array object
             *          ||card_uid
             *          ||name
             *          ||rating
             *          ||cost
             *          ||attribute
             *          ||hp
             *          ||atk
             *          ||def
             *          ||hp_coin
             *          ||atk_coin
             *          ||def_coin
             *          ||ability_type
             *          ||ability_level
             *          ||image_url
             *          ||card_order        |덱에 저장할 때 카드 순서
             */
            #endregion
            public static readonly string LoadDeckPath = "/deck/load";

            #region 설명
            // example
            // {
            //    "code": "ok",
            //    "data":
            //    {
            //        "available_in": null
            //    }
            // }
            // "available_in"키의 값 기준
            // - null || 0 이라면 스캔 가능
            // - 1이상이라면 쿨타임 밀리세컨드(ms)
            #endregion
            public static readonly string GetScanCoolPath = "/card/gen_cooltime";
        }

        public static class ParcelServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42530/parcel";

            public static readonly string GetMyParcelsPath = "/getMyParcels";

            public static readonly string ClaimPath = "/claim";
        }

        public static class RaidServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42535";

            public static readonly string OpenRaidPath = "/openWorldRaid";

            public static readonly string GetRaidsPath = "/getWorldRaids";

            public static readonly string GetRaidDetailPath = "/getRaidDetail";

            // 유저가 참가중인 모든 레이드 불러오기
            public static readonly string GetParticipatedAllRaidsPath = "/getParticipatedRaids";

            public static readonly string RefreshWorldRaidsPath = "/refreshWorldRaids";

            // 레이드 결과를 확인하고 보상을 수령
            public static readonly string ClaimRaidRewardsPath = "/claimRaidRewards";
        }

        public static class ShopServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42530";

            public static readonly string GetItemsPath = "/shop/getItems";

            public static readonly string PurchaseBoosterPath = "/shop/purchase/booster";
        }

        public static class GuildServer
        {
            public static readonly string ServerUrl = "http://43.203.118.62:42530";

            public static readonly string GetGuildListPath = "/guild/list";

            public static readonly string SearchGuildPath = "/guild/search";

            public static readonly string EstablishGuildPath = "/guild/found";

            public static readonly string JoinToGuildPath = "/guild/join";

            public static readonly string GetMyGuildPath = "/guild/my";

            public static readonly string LeaveGuildPath = "/guild/leave";

            public static readonly string KickFromGuildPath = "/guild/kick";

            public static readonly string AcceptUserPath = "/guild/accept";

            public static readonly string GetSkillsInfoTablePath = "/guild/skills";

            public static readonly string SkillContributePath = "/guild/contribute";

            public static readonly string GetMemberWorldRaidsPath = "/guild/getMemberWorldRaids";

            public static readonly string AttendPath = "/guild/attend";

            public static readonly string IsAttendedPath = "/guild/isAttended";

            public static readonly string AppointMaster = "/guild/appointMaster";
        }

        public static class TCPServer
        {
            public static readonly string ServerIp = "43.203.118.62";
            //public static readonly string ServerIp = "10.0.0.20";
            public const int PvpPort = 42532;
            public const int SingleRaidPort = 42533;
            public const int WorldRaidPort = 42534;
            public const int PvePort = 42536;
        }
    }
}
