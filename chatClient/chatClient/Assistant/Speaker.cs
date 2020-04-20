using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
//using Microsoft.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace chatClient.Assistant
{
    class Speaker
    {
        SpeechSynthesizer synth;

        public Speaker()
        {
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = 0;
            synth.Volume = 100;
        }

        public void Speak(string text)
        {
            try
            {
                synth.SpeakAsync(text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
