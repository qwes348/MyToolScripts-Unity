using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oni
{
    public class PopupInfo
    {
        public enum PopupButtonType { None = -1, Confirm, Yes, No, Close, Purchase }
        public enum CurrencyType { None = -1, Gold, Cash }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public string SmallContent { get; private set; }
        public bool PauseScene { get; private set; }
        public PopupButtonType[] ButtonTypes { get; private set; }
        public Action<PopupButtonType> Listener { get; private set; }
        public Dictionary<CurrencyType, int> CurrencyDict { get; private set; }

        private PopupInfo(Builder builder)
        {
            Title = builder.Title;
            Content = builder.Content;
            PauseScene = builder.PauseScene;
            ButtonTypes = builder.ButtonTypes;
            Listener = builder.Listener;
            CurrencyDict = builder.CurrencyDict;
            SmallContent = builder.SmallContent;
        }

        public class Builder
        {
            public string Title { get; private set; }
            public string Content { get; private set; }
            public string SmallContent { get; private set; }
            public bool PauseScene { get; private set; }
            public PopupButtonType[] ButtonTypes { get; private set; }
            public Action<PopupButtonType> Listener { get; private set; }
            public Dictionary<CurrencyType, int> CurrencyDict { get; private set; }

            public Builder()
            {
                Title = string.Empty;
                SmallContent = string.Empty;
                Content = string.Empty;
                ButtonTypes = null;
                Listener = null;
                PauseScene = false;
                CurrencyDict = new Dictionary<CurrencyType, int>();
            }

            public Builder SetTitle(string title)
            {
                this.Title = title;
                return this;
            }

            public Builder SetContent(string content)
            {
                this.Content = content;
                return this;
            }

            public Builder SetSmallContent(string content)
            {
                this.SmallContent = content;
                return this;
            }

            public Builder SetButtons(params PopupButtonType[] buttonTypes)
            {
                this.ButtonTypes = buttonTypes;
                return this;
            }

            public Builder SetListener(Action<PopupButtonType> listener)
            {
                this.Listener = listener;
                return this;
            }

            public Builder SetPauseScene(bool isPause)
            {
                this.PauseScene = isPause;
                return this;
            }

            public Builder AddCurrencyDict(CurrencyType key, int amount)
            {
                if(CurrencyDict.ContainsKey(key))
                {
                    CurrencyDict[key] = amount;
                    return this;
                }

                CurrencyDict.Add(key, amount);
                return this;
            }

            public PopupInfo Build()
            {
                return new PopupInfo(this);
            }
        }
    }
}
