using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using chatClient.Models;

namespace chatClient.Assistant
{
    class Recognizer
    {
        public bool Status;
        private bool _hello;
        private string _name;
        private SpeechRecognitionEngine sre;
        GrammarBuilder gb;
        Form1 form1;

        public Recognizer(Form1 obj)
        {
            Status = true;
            _hello = false;
            _name = "еви";
            form1 = obj;

            try
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
                sre = new SpeechRecognitionEngine();
                sre.SetInputToDefaultAudioDevice();//microfone

                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);

                Choices numbers = new Choices();
                numbers = CreateGramar();

                gb = new GrammarBuilder();
                gb.Culture = ci;
                gb.Append(numbers);
                sre.UnloadAllGrammars();
                Grammar g = new Grammar(gb);
                sre.LoadGrammar(g);//загружаем "грамматику"

                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                MessageBox.Show("Voice assinstant components not installed",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Status = false;
            }
        }

        private Choices CreateGramar()
        {
            Choices ch = new Choices();
            ch.Add(new string[] { _name, "привет " + _name,
                _name + " есть новые сообщения",
                _name + " назови случайное число",
                _name + " который час",
                _name + " ты молодец",
                _name + " открой окно поиска",
                _name + " открой окно настроек",
                _name + " открой мой профиль",
                _name + " прочитай новые сообщения"});

            return ch;
        }

        private void GetNewMsg(ref List<string> list)
        {
            foreach(var V in form1._newMsgList)
            {
                list.Add("Зачитываю сообщения от " + V.msgList[0].User);
                foreach(var F in V.msgList)
                {
                    list.Add(F.Text + "   ");
                }
                list.Add("     ");
            }
        }

        private void CheckNewMsg()
        {
            string text = "у вас нету новых сообщений";
            List<string> list = new List<string>();
            Speaker speaker = new Speaker();

            foreach(var V in form1._listOfUsers)
            {
                if (V.NewMsg == true)
                {
                    list.Add(V.Name + " " + V.Surname);
                }
            }

            if (list.Count() == 0)
                speaker.Speak(text);
            else
            {
                speaker.Speak("у вас есть новые сообщения от ");
                foreach (var V in list)
                    speaker.Speak(V);
            }
        }

        private string SayTime()
        {
            DateTime time = DateTime.Now;
            return time.Hour + " " + time.Minute.ToString();
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (Status == true)
            {
                Speaker speaker = new Speaker();

                if (e.Result.Confidence > 0.7)
                {
                    if (e.Result.Text == "привет " + _name)
                    {
                        if (_hello == false)
                        {
                            _hello = true;
                            speaker.Speak("привет");
                        }
                        else
                            speaker.Speak("мы уже здоровались");

                        return;
                    }
                    else if (e.Result.Text == _name)
                    {
                        speaker.Speak("Слушаю");
                        return;
                    }
                    else if (e.Result.Text == _name + " назови случайное число")
                    {
                        Random rand = new Random();
                        speaker.Speak(rand.Next(1000).ToString());
                        return;
                    }
                    else if (e.Result.Text == _name + " есть новые сообщения")
                    {
                        CheckNewMsg();
                        return;
                    }
                    else if (e.Result.Text == _name + " прочитай новые сообщения")
                    {
                        List<string> list = new List<string>();
                        GetNewMsg(ref list);

                        if(list.Count != 0)
                            foreach (var V in list)
                                speaker.Speak(V);

                        return;
                    }
                    else if (e.Result.Text == _name + " который час")
                    {
                        speaker.Speak(SayTime());
                        return;
                    }
                    else if (e.Result.Text == _name + " ты молодец")
                    {
                        speaker.Speak("спасибо");
                        return;
                    }
                    else if (e.Result.Text == _name + " открой окно поиска")
                    {
                        form1.OpenSearchWindow();
                        return;
                    }
                    else if (e.Result.Text == _name + " открой окно настроек")
                    {
                        form1.OpenSettingsWindow();
                        return;
                    }
                    else if (e.Result.Text == _name + " открой мой профиль")
                    {
                        form1.OpenMyProfileWindow();
                        return;
                    }
                }
            }
        }
    }
}
