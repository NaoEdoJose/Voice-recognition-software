using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;


namespace VoiceBot
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer s = new SpeechSynthesizer();
        Random random = new Random();

        private bool hayans = false;

        public Form1()
        {
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
            Choices list = new Choices();

            list.Add(new String[] { "hello", "how are you" , "i'm fine" });

            Grammar gr = new Grammar(new GrammarBuilder(list));


            try
            {

                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);


            }
            catch { return; }

            s.SelectVoiceByHints(VoiceGender.Female);
            s.Speak("Hello , My name is VoiceBot");

            InitializeComponent();
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;
          

            //O que você fala 
            if (r == "hello")
            {
               //A resposta
                say("Hello");
            }

            if (r == "how are you")
            {
               say("Fine , and you ?");
               hayans = true;
               if (hayans) { 
                   if (r == "i'm fine") { 
                       say("That's good to hear. Now , what can I do for you?"); 
                       hayans = false;
                   } 
               }
            }


        }

        public void say(String h) {

            s.Speak(h);
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
