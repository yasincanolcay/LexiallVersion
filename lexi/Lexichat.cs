using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Media;
using System.Threading;
using System.Xml.Linq;
using System.Globalization;
using System.Diagnostics;

namespace lexi
{
    class Lexichat
    {
        //YAPAY ZEKA LEXİ

        //lexi chat icin degisken


        //----FORM BORDER RADIUS DESIGN---//
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int NLeftRect
          , int NRightRect, int NTopRect, int NBottomRect, int NWidthEllipse, int NHeightEllipse);

        //------------------------------------//

        //------------------------------------//
        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer lexi = new SpeechSynthesizer();
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        Random rnd = new Random();
        int RecTimeOut = 0;
        DateTime TimeNow = DateTime.Now;
        //----------------------------------------//
        //FORM EKRANDA SURUKLEMEK ICIN INT KONUM DEGISKENLERI VE BOOL
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        //----------------------------------------//

        //USERS MOUSE CONTROLS

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);



        private const int MOUSEEVENTF_LEFTDOWN = 0x02;

        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;

        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        int PositionX = Cursor.Position.X;
        int PositionY = Cursor.Position.Y;
        bool MouseControllMode = false;
        //--------------------------------------------//
        bool radioShow = false;
        //joke
        //fıkra icin
        int Joke = 1;
        bool jokeTold = false;
        //
        //relax muzik icin
        int relaxmusic = 1;
        bool openrelax;
        SoundPlayer relax1 = new SoundPlayer(@"sound\relax1.wav");
        SoundPlayer relax2 = new SoundPlayer(@"sound\relax2.wav");
        SoundPlayer relax3 = new SoundPlayer(@"sound\relax3.wav");
        SoundPlayer relax4 = new SoundPlayer(@"sound\femalerelax2.wav");
        //
        //
        //Kitap tavsiyesi icin gereken degiskenler
        int BookLine = 0;
        List<string> BookListPictures = new List<string>();

        int BookLinePic = 0;
        List<string> Booklist = new List<string>();
        PictureBox BookPic = new PictureBox();
        //lexi klavye
        Keyboard kv = new Keyboard();
        //capslock acmak icin
        bool capslock = false;
        //
        bool Shutdown = false;

        void load()
        {
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommends.txt")))));
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommends.txt")))));
            BookPic.Dock = DockStyle.Bottom;
            BookPic.Width = 183;
            BookPic.Height = 181;
            BookPic.SizeMode = PictureBoxSizeMode.Zoom;


        }
        public void listen()
        {
            lexi.SpeakAsync("hello hello hello");
        }

    }
}
