using System;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using UnityEngine.Serialization;

namespace Scraper
{
    public class ChatReader : MonoBehaviour
    {
        private TcpClient _twitch;
        private StreamReader _reader;
        private StreamWriter _writer;

        private const string User = "justinfan123123";
        private const string URL = "irc.chat.twitch.tv";
        private const int Port = 6667;

        public static ChatReader current;
        public static Action<string, string> OnMessageReceived;

        public string channel = "disguisedtoast";

        private void Awake()
        {
            current = this;
            ConnectToTwitch();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            ReadChat();
        }
        
        private void ReadChat()
        {
            if (!_twitch.Connected) ConnectToTwitch();
            if (_twitch.Available > 0)
            {
                string message = _reader.ReadLine();
                if (message == null) return;
                
                CheckPing(message);
                ProcessMessage(message);
            }
        }
         
        private void ConnectToTwitch()
        {
            _twitch = new TcpClient(URL, Port);
            _reader = new StreamReader(_twitch.GetStream());
            _writer = new StreamWriter(_twitch.GetStream());

            channel = SessionData.twitchChannelName;
            _writer.WriteLine("PASS " + "RandomPassword");
            _writer.WriteLine("NICK " + User);
            _writer.WriteLine("USER " + User + " 8 * :" + User);
            _writer.WriteLine("JOIN #" + channel);
            _writer.Flush();
        }
        
        private void CheckPing(string message)
        {
            if (message.Contains("PING"))
            {
                _writer.WriteLine("PONG :tmi.twitch.tv");
                _writer.Flush();
            }
        }
        
        private void ProcessMessage(string message)
        {
            if (message.Contains("PRIVMSG"))
            {
                int splitPoint = message.IndexOf("!", StringComparison.Ordinal);
                string author = message.Substring(0, splitPoint);
                author = author.Substring(1);

                splitPoint = message.IndexOf(":", 1, StringComparison.Ordinal);
                string chat = message.Substring(splitPoint + 1);
                
                OnMessageReceived?.Invoke(author, chat);
            }
        }

        public void ProcessTestInput(string author, string message)
        {
            OnMessageReceived?.Invoke(author, message);
        }
    }
}