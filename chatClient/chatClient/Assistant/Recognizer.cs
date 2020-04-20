using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chatClient.Assistant
{
    class Recognizer
    {
        public bool Status;
        private SpeechRecognitionEngine sre;
        GrammarBuilder gb;

        public Recognizer()
        {
            Status = true;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            sre = new SpeechRecognitionEngine();
            sre.SetInputToDefaultAudioDevice();//microfone

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);

            Choices numbers = new Choices();
            //numbers.Add(new string[] { "привет хрень", "один", "два", "три", "четыре", "пять" });
            numbers.Add(new string[] { "привет ассистент", "ассистент" });

            gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);
            //gb.Append(new Choices("left", "right", "up", "down"));  //добавляем используемые фразы
            //sre.UnloadAllGrammars();
            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);//загружаем "грамматику"

            sre.RecognizeAsync(RecognizeMode.Multiple);

            
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (Status == true)
            {
                Speaker speaker = new Speaker();

                if (e.Result.Confidence > 0.7)
                {
                    if (e.Result.Text == "привет ассистент")
                    {
                        speaker.Speak("Привет, пользователь");
                    }
                    else if (e.Result.Text == "ассистент")
                        speaker.Speak("Слушаю");
                    //MessageBox.Show(e.Result.Text);
                    //MessageBox.Show("Привет, лошара");
                }
                //MessageBox.Show("Recognized phrase: " + e.Result.Text);
            }
        }

        // Handle the SpeechRecognized event.  
        /*static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Recognized text: " + e.Result.Text);
        }*/
    }
}
