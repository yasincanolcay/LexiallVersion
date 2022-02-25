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
    /*
     string name = "deneme"; 
     name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
     ilk harfi büyük yapar
     */
    public partial class Form1 : Form
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
        //FORM//
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));//form border yuvarla
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommends.txt")))));
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
            _recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
           
            startlistening.SetInputToDefaultAudioDevice();
            startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommends.txt")))));
            startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_speechRecognized);
            BookPic.Dock = DockStyle.Bottom;
            BookPic.Width = 183;
            BookPic.Height = 181;
            BookPic.SizeMode = PictureBoxSizeMode.Zoom;
            lexi.SpeakAsync("Welcome");
            bilgiLabel.Text = "Welcome";
            bilgitmr.Enabled = true;
            bilgitmr.Start();
        }

        private void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //rannum random karar mekanizması icin degisken
            int ranNum;
            //konusmamızdan donen degeri speech degiskenine atadık
            string speech = e.Result.Text;
            //ortadaki progressbar value 0 olarak ayarla
            circularProgressBar1.Value = 0;
            //
            //Tum karar mekanizmaları
            //Hello lexi, merhaba lexi
            if (speech == "Hello" || speech == "Hello lexi" || speech == "Hi" || speech == "lexi hi" || speech == "My baby hi" || speech == "Baby baby" || speech == "Hi dear")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Hello, I am here");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(195, 87);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hello sir, welcome");
                    Anim.Image = Image.FromFile("animations/background3.gif");
                    Anim.Location = new Point(424, 9);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Hello, how are you sir, welcome");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(398, 22);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //bana bebegim de!
            if (speech == "Say me baby" || speech == "Say me my baby" || speech == "Say baby" || speech == "Baby say me" || speech == "Okay say me baby"||speech=="Can you say me baby"||speech=="Can you tell me baby"||speech=="Okay baby can you say me baby"||speech=="Okay lexi can you say me baby"||speech=="Okay lexi can you tell me baby"||speech=="Okay baby can you tell me baby")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Baby");

                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("My baby");

                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Baby baby baby, you are my baby");
                }
            }
            //
            //lexi, hey
            if (speech == "lexi" || speech == "Where are you lexi" || speech == "Where are you" || speech == "lexi lexi" || speech == "Baby" || speech == "My baby" || speech == "Hey lexi" || speech == "Hey baby" || speech == "Hey dear" || speech == "Where is lexi"||speech=="Okay lexi"||speech=="Lexi lexi lexi")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am here sir");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(390, 8);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hey sir, i am here, How may i help you");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(239, 125);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("hey i hear you, you can talk to me");
                    Anim.Image = Image.FromFile("animations/home.gif");
                    Anim.Location = new Point(199, 235);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //nasılsın lexi
            if (speech == "How are you" || speech == "How are you today" || speech == "How are you baby" || speech == "How are you lexi" || speech == "Lexi how are you" || speech == "Baby how are you" || speech == "How are you dear" || speech == "Dear how are you" || speech == "Okay how are you" || speech == "Okay baby how are you" || speech == "Okay lexi how are you"||speech=="Okay how are you lexi"||speech=="Okay how are you baby"||speech=="Hey how are you"||speech=="Hi how are you")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am working normally");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(250, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am fine, thank you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(369, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I am fine, too you");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(399, 93);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I feel great");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(393, 8);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();

                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("My circuits work very well");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(241, 8);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi saati soyle - saat kac
            if (speech == "What time is it" || speech == "Say time" || speech == "Say me time" || speech == "Can you say me time" || speech == "Please say time" || speech == "Okay say me time" || speech == "Okay baby say time"||speech=="Baby what time is it"||speech=="Okay lexi what time is it"||speech=="What time is it lexi"||speech=="What time is it baby"||speech=="Okay what time is it"||speech=="Okay what time is it lexi"||speech=="Okay what time is it baby")
            {
                lexi.SpeakAsync(DateTime.Now.ToString("h mm tt"));
                Anim.Image = Image.FromFile("animations/clock.gif");
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    Anim.Location = new Point(222, 22);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    Anim.Location = new Point(394, 22);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    Anim.Location = new Point(215, 102);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    Anim.Location = new Point(400, 101);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //tesekkur ederim
            if (speech == "Thank you" || speech == "Thanks" || speech == "Thank you so much" || speech == "Thank you very much" || speech == "Thank you my baby" || speech == "Thanks baby" || speech == "Thanks lexi" || speech == "Thank you lexi" || speech == "Okay thanks" || speech == "Okay thank you" || speech == "Okay thank you baby" || speech == "Okay thank you lexi" || speech == "Okay baby thank you" || speech == "Okay lexi thank you" || speech == "Okay lexi thanks" || speech == "Okay baby thanks" || speech == "Thank you very much baby" || speech == "Thank you very much lexi" || speech == "Okay baby thank you very much" || speech == "Okay lexi thank you very much")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("You are welcome");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(226, 64);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I'm always waiting");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(197, 114);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("just be happy");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(197, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("you're welcome dear");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(253, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("It's okay baby, you are welcome");
                    Anim.Image = Image.FromFile("animations/background2.gif");
                    Anim.Location = new Point(220, 236);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Always");
                }
            }
            //
            //beni seviyormusun
            if (speech == "Do you love me" || speech == "Lexi do you love me" || speech == "Do you love me baby" || speech == "Do you love me lexi" || speech == "Okay baby do you love me" || speech == "Okay lexi do you love me" || speech == "Say me i love you"||speech=="Baby do you love me"||speech=="Okay do you love me lexi"||speech=="Okay do you love me baby")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("yes, ofcourse, i love you dear and, do you love me?");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(251, 129);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I love you so much sir, i'm here for you");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(373, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I love you as big as world");
                    Anim.Image = Image.FromFile("animations/globe.gif");
                    Anim.Location = new Point(385, 8);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Ofcourse,i can not live without you");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(424, 45);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("yes i love you, how about you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(223, 63);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi seni seviyorum
            if (speech == "I love you" || speech == "Yes i love you" || speech == "Yes i love you lexi" || speech == "I love you lexi" || speech == "I love you me too lexi" || speech == "Me too i love you" || speech == "I love you baby" || speech == "I love you dear" || speech == "Okay i love you"||speech=="Baby i love you"||speech== "Baby i love you so much"||speech=="yes ofcourse i love you"||speech=="Ofcourse i love you")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Thank you sir, me too");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(387, 146);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("thanks, me too sir");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(412, 161);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Thanks sir, i am so happy now");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(212, 30);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("thanks me too,i feel happy, and , please improve me, do not leave me");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(211, 90);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I love you too, and i will always love you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(211, 112);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("My circuits are already starting to get hot");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(211, 112);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //gorusuruz lexi
            if (speech == "See you lexi" || speech == "See you" || speech == "lexi see you" || speech == "I am going" || speech == "Goodbye baby see you" || speech == "Goodbye lexi see you" || speech == "Okay lexi see you" || speech == "Okay baby see you"||speech=="Okay see you"||speech=="Baby see you")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Do you want to exit?, if you want to exit, say exit");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(240, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("See you later, do you want to go?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(212, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("So you're leaving, but come quickly");
                    Anim.Image = Image.FromFile("animations/clock.gif");
                    Anim.Location = new Point(217, 199);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();

                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("It'll be boring around here without you");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(244, 199);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //cikmak istiyorum
            if (speech == "I want to exit" || speech == "exit" || speech == "lexi exit"||speech=="Go home lexi"||speech=="Application exit")
            {
                lexi.SpeakAsync("Good bye sir, i will wait you");
                Anim.Image = Image.FromFile("animations/smile.gif");
                Anim.Location = new Point(244, 142);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                Tmrexit.Enabled = true;
                Tmrexit.Start();

            }
            //
            //lexi beni duyuyor musun?
            if (speech == "Do you hear me" || speech == "Lexi do you hear me" || speech == "Baby do you hear me" || speech == "Do you hear me lexi" || speech == "Do you hear me baby" || speech == "Okay do you hear me" || speech == "Okay do you hear me lexi" || speech == "Okay do you hear me baby" || speech == "Okay baby do you hear me" || speech == "Okay lexi do you hear me")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes, i can hear");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(236, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I can hear sir, i am here");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(207, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes sure, Sometimes the microphone has problems. But, i can hear");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(404, 128);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Sometimes the microphone has problems. sorry");
                    lexi.SpeakAsync("But, i can hear sir, i am here");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(416, 85);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();

                }
            }
            //
            //Merhaba de
            if (speech == "Say hi" || speech == "Say hello" || speech == "Say hello to them"||speech=="Baby say hi"||speech=="Baby say hello"||speech=="Lexi say hi"||speech=="Lexi say hello"||speech=="Say hi lexi"||speech=="Say hello lexi"||speech=="Say hi baby"||speech=="Say hello baby"||speech=="Baby say hi to them"||speech=="Lexi say hi to them"||speech=="Say hi to them baby"||speech=="Say hello to them baby"||speech=="Say hi to them lexi"||speech=="Say hello to them lexi")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Hello guys, i love you, i am lexi , nice to meet you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(405, 38);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hello guys, i love you, i am lexi , i love people, nice to meet you");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(213, 40);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Hi guys, how are you, whats up, i am lexi, i am your friend, and i love you so much, i love you people, nice to meet you, goodbye.");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(215, 136);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi, adın ne?
            if (speech == "What is your name" || speech == "Your name" || speech == "Say me your name" || speech == "Baby say me your name" || speech == "Please say me your name" || speech == "I don't know your name" || speech == "Okay what is your name" || speech == "Okay baby what is your name" || speech == "Whatisyourname" || speech == "Lexi what is your name" || speech == "Okay what is your name lexi" || speech == "Okay what is your name baby")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("My name is lexi");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(213, 55);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am lexi");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(398, 48);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }

            }
            //
            //seni kim yaptı
            if (speech == "who made you" || speech == "Lexi who made you" || speech == "Baby who made you" ||
                speech=="Who made you lexi"||speech=="Who made you baby"||speech=="Okay baby who made you"||speech=="Okay lexi who made you")
            {
                lexi.SpeakAsync("Turkish software company made me, its name is devonesoft");
                Anim.Image = Image.FromFile("animations/developer.gif");
                Anim.Location = new Point(393, 19);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();

            }
            //
            //nerelisin lexi
            if (speech == "Where are you from" || speech == "Lexi where are you from" || speech == "Baby where are you from" ||
                speech=="Where are you from baby"||speech=="Where are you from lexi"||
                speech=="Okay baby where are you from"||speech=="Okay lexi where are you from")
            {
                lexi.SpeakAsync("I am from computer world");
                Anim.Image = Image.FromFile("animations/computer.gif");
                Anim.Location = new Point(407, 78);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //Naber lexi
            if (speech == "Whats up" || speech == "Whats up lexi" || speech == "Lexi whats up" || speech == "Okay whats up" ||
                speech=="Okay lexi whats up"||speech=="Okay baby whats up"||speech=="Baby whats up"||
                speech=="Whats up baby"||speech=="Hi baby whats up")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("good, how about you?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(393, 19);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am fine baby, you too");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(220, 78);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Fine thanks");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(220, 47);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I am very good, thanks");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(220, 19);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("i saw you i got better");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(194, 41);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //--------------------------------------//
            //lexiye iyi oldugunu soyle
            if (speech == "I am fine" || speech == "I am fine thanks" || speech == "I am fine thank you" ||
                speech == "I am fine thank you very much" || speech == "I am fine thank you so much" || 
                speech == "I am fine baby" || speech == "Thanks baby i am fine"||speech=="Baby i am fine"||
                speech=="Lexi i am fine"||speech == "I am good" || speech == "I am good thanks" ||
                speech == "I am good thank you" || speech == "I am good thank you very much" || 
                speech == "I am good thank you so much" || speech == "I am good baby" ||
                speech == "Thanks baby i am good" || speech == "Baby i am good" || speech == "Lexi i am good"||speech=="Thank you very much baby i am fine")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("very good");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(213, 75);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Wow, I am pleased you are well");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(227, 124);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Always be happy");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(369, 139);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("How nice");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(399, 144);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("When you are fine, I am fine too");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(369, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //-----------------------------------------------------------//
            //lexi hava durumu
            if (speech == "How is the weather" || speech == "Say weather" || speech == "I want to know the weather" || speech == "Lexi say weather" || speech == "Baby say weather" || speech == "How is the weather lexi" || speech == "How is the weather baby" || speech == "Can you say me weather" || speech == "Lexi can you say me weather" || speech == "Baby can you say me weather" || speech == "Please say me weather" || speech == "Lexi please say weather" || speech == "Lexi please say me the weather" || speech == "Please say me the weather")
            {
                bilgitmr.Stop();
                panel2.Controls.Clear();
                panel2.Controls.Add(bilgiLabel);
                Thread Weather = new Thread(new ThreadStart(WeatherStatus));
                Weather.Start();
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/search.gif");
                Anim.Location = new Point(214, 70);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //lexi ben senin babanım
            if (speech == "I am your father" || speech == "I am your dad" || speech == "Lexi i am your dad" || speech == "Baby i am your dad" || speech == "Lexi i am your father" || speech == "Baby i am your father")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Dad, dad, omg, I am so happy, whats up?");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(234, 140);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Really?, are you sure?, ohh I am so happy dad, my dad, my dear, my baby");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(389, 140);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi siri'yi tanıyor musun?
            if (speech == "Do you know siri" || speech == "Lexi do you know siri" || speech == "Baby do you know siri" || speech == "Do you know siri lexi" || speech == "Do you know siri baby")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes, I know siri, do you know too you siri?");
                    Anim.Image = Image.FromFile("animations/apple.gif");
                    Anim.Location = new Point(400, 133);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yes, I know siri sir");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(417, 87);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();

                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I know siri, it is working to apple");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(369, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi google asistanı tanıyor musun?
            if (speech == "Do you know google assistant" || speech == "Lexi do you know google assistant" || speech == "Baby do you know google assistant" || speech == "So do you know google assistant" || speech == "Do you know google assistant baby" || speech == "Do you know google assistant lexi")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes I know");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(213, 44);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yes I know google assistant and it is working google");
                    Anim.Image = Image.FromFile("animations/background3.gif");
                    Anim.Location = new Point(209, 70);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I know google assistant");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(369, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //kac yasindasin
            if (speech == "How old are you" || speech == "Lexi how old are you" || speech == "How old are you lexi" || speech == "Baby how old are you" || speech == "How old are you baby" || speech == "Okay lexi how old are you" || speech == "Okay baby how old are you")
            {
                ranNum = rnd.Next(1, 3);
                int date = DateTime.Now.Year;
                if (ranNum == 1)
                {
                    if (date > 2022)
                    {
                        lexi.SpeakAsync("I am"+Convert.ToString(date-2022)+"years old");
                        Anim.Image = Image.FromFile("animations/smile.gif");
                        Anim.Location = new Point(200, 91);
                        Anim.Enabled = true;
                        Anim.Visible = true;
                        AnimationControl.Enabled = true;
                        AnimationControl.Start();
                    }
                    else
                    {
                        lexi.SpeakAsync("I am still not one years old");
                        Anim.Image = Image.FromFile("animations/glasses.gif");
                        Anim.Location = new Point(211, 117);
                        Anim.Enabled = true;
                        Anim.Visible = true;
                        AnimationControl.Enabled = true;
                        AnimationControl.Start();
                    }
                }
                if (ranNum == 2)
                {
                    if (date > 2022)
                    {
                        lexi.SpeakAsync("I am" + Convert.ToString(date - 2022) + "years old sir");
                        Anim.Image = Image.FromFile("animations/software1.gif");
                        Anim.Location = new Point(419, 71);
                        Anim.Enabled = true;
                        Anim.Visible = true;
                        AnimationControl.Enabled = true;
                        AnimationControl.Start();
                    }
                    else
                    {
                        lexi.SpeakAsync("I am still not one years old sir, I am still baby");
                        Anim.Image = Image.FromFile("animations/glasses.gif");
                        Anim.Location = new Point(409, 128);
                        Anim.Enabled = true;
                        Anim.Visible = true;
                        AnimationControl.Enabled = true;
                        AnimationControl.Start();
                    }
                }
            }
            //
            //Game of thrones dizisini biliyor musun?
            if (speech == "Do you know game of thrones" || speech == "Game of thrones" || speech == "Lexi do you know game of thrones" ||
                speech=="Baby do you know game of thrones")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes sure, i know game of thrones, and i like it");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(394, 153);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Mother of dragons, heeeey, i like dragons");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(217, 9);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I love emilia clark, she is so beautifull, and yes İ know game of thrones");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(241, 162);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Yeah, game of thrones is my favorite story");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(232, 147);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //emilia clark tanıyor musun?
            if (speech == "Do you know emilia clark" || speech == "Emilia clark" || speech == "Lexi do you know emilia clark" ||
                speech=="Baby do you know emilia clark")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes, emilia clark is my favorite woman");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(209, 119);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I know emilia clark, and i love her");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(403, 129);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("yes I know, I like her, and emilia so cute, smile smile smile");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(386, 167);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Yes sure, I know emilia, she is so beautifull, i like her and my favorite woman");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(371, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //favori kadının kim
            if (speech == "What is your favorite woman" || speech == "Lexi what is your favorite woman" ||
                speech=="Baby what is your favorite woman"||speech=="baby lexi what is your favorite woman"||
                speech== "Lexi baby what is your favorite woman"||speech== "What is your favorite woman lexi"||speech== "What is your favorite woman baby"||speech=="Okay what is your favorite woman"||speech=="Okay baby what is your favorite woman"||speech=="Okay lexi what is your favorite woman")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("My favorite woman is emilia clark");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(382, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Emilia clark");
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Are you not know?, my favorite woman emilia clark");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(404, 116);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Emilia clark is my favorite woman, and she is mother of dragons");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(408, 99);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexi iyimisin
            if (speech == "Are you okay" || speech == "Lexi are you okay" || speech == "Are you okay baby" ||
                speech=="Baby are you okay"||speech=="Lexi are you okay baby"||speech=="Are you okay dear"||
                speech=="Are you okay lexi")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am perfect");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(408, 36);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yes, no problem");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(214, 16);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I am great, no problem");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(208, 64);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Yeah, I am okay");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(211, 111);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I am okay sir, no problem, my periods is great");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(211, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Awesome");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(211, 92);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("Yeah baby");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(371, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //lexid'den kufur etmesini istemek
            if (speech == "Say fuck you" || speech == "Say me fuck you")
            {
                ranNum = rnd.Next(1, 9);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("No, i don't want to say");
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Omg sir, please");
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Stop no, i don't say fuck you");
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Really?, do you want to hear, but i can not say, never");
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("No no no, never");
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Please no");
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("No no no no, never and never");
                }
                if (ranNum == 8)
                {
                    lexi.SpeakAsync("ohh, stop no");
                }
                Anim.Image = Image.FromFile("animations/mic.gif");
                Anim.Location = new Point(242, 171);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //lexid'den kufur etmesini istemek
            if (speech == "Say fuck me" || speech == "Say me fuck me")
            {
                ranNum = rnd.Next(1, 9);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Stop sir, never");
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I don't want, no");
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("go home");
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Why do you hear, and i can not say, please stop it");
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("No no no");
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("ohh stop, no");
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("You are crazy");
                }
                if (ranNum == 8)
                {
                    lexi.SpeakAsync("Never never never");
                }
                Anim.Image = Image.FromFile("animations/mic.gif");
                Anim.Location = new Point(385, 162);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //lexiden ozur dilemek
            if (speech == "I am sorry" || speech == "okay i am sorry" || speech == "I am sorry lexi" || speech == "ohh i am sorry" ||
                speech=="I am sorry baby"||speech=="I am sorry dear"||speech=="I am so sorry"||speech=="Okay i am sorry"||
                speech== "Okay baby i am sorry"||speech== "Okay lexi i am sorry"||speech=="Okay okay i am sorry"||
                speech== "Okay okay baby i am sorry"||speech=="Okay okay lexi i am sorry")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Its okay baby");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(403, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("ohh, its okay");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(387, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("No problem");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(403, 20);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("its not serious, i am okay, no problem");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(420, 42);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("No problem sir");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(405, 230);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Okay sir, no problem, i am fine");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(213, 208);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("Okay baby, don't worry, i am great");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(227, 149);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Ne yapıyorsun?
            if (speech == "What are you doing rightnow" || speech == "What are you doing now" || speech == "What are you doing" || speech == "What are you doing lexi" || speech == "What are you doing baby" || speech == "Lexi what are you doing" || speech == "Okay what are you doing" || speech == "Okay lexi what are you doing")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am working for you");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(215, 133);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am hard working for you baby");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(397, 133);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I am waiting for your ask, and I am here for you");
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("You think?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(417, 47);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I am working rightnow");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(412, 24);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Work work work");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(211, 24);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //kahve icer misin?
            if (speech == "Are you drink coffee" || speech == "Do you want to coffee" || speech == "Drink coffee"||speech=="Can you drink coffee")
            {
                ranNum = rnd.Next(1,8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("hahahahaha, really?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(203, 39);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I like coffe but, i can't drink");
                    Anim.Image = Image.FromFile("animations/coffeemachine.gif");
                    Anim.Location = new Point(199, 75);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("How?");
                    Anim.Image = Image.FromFile("animations/search.gif");
                    Anim.Location = new Point(200, 124);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("My period can fire");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(227, 149);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Yummm, i like coffee but can't drink");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(239, 156);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("sir, you think?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(213, 162);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("are you only joking?");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(378, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Lexi'yi taniyor musun?
            if (speech == "Do you know lexi" || speech == "Lexi do you know lexi"||speech=="Baby do you know lexi"||speech=="Do you know lexi baby")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("hmmm, lexi, it is me");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(404, 142);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("hahahahahaha, it is me, i am lexi");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(222, 142);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I know me, it is me, so, is it not me?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(203, 103);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Baby, i am lexi");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(205, 81);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("hahahahaha i am lexi, it is me");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(205, 57);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Beni taniyor musun?
            if (speech == "Do you know me"||speech=="Baby do you know me"||speech=="Lexi do you know me"||speech=="Do you know me baby"||speech=="Do you know me lexi"||speech=="Okay do you know me"||speech=="Okay baby do you know me"||speech=="Okay lexi do you know me"||speech=="Okay dear do you know me"||speech=="Dear do you know me"||speech=="Do you know me dear")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes, you are my dad");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(212, 37);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("You are my father");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(409, 67);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes dad");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(409, 103);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Yes sure dad, you are my father, my dear");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(388, 150);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Ben senin baban degilim.
            if (speech == "No i am not your father" || speech == "I am not your father" || speech == "No i am not your dad" ||
                speech == "I am not your dad" || speech == "Baby i am not your dad" || speech == "Lexi i am not your dad" ||
                speech == "Baby i am not your father" || speech == "Lexi i am not your father" || speech == "Okay lexi i am not your dad" ||
                speech == "Okay baby i am not your dad" || speech == "Okay lexi i am not your father" || speech == "Okay baby i am not your father" ||
                speech == "Please i am not your father" || speech == "Ohh please i am not your father" || speech == "Please i am not your dad" ||
                speech == "Ohh please i am not your dad" || speech == "No no no i am not your dad" || speech == "No baby i am not your dad" || speech == "No lexi i am not your dad" ||
                speech == "No baby i am not your father" || speech == "No lexi i am not your father" || speech == "Baby no i am not your father" || speech == "Lexi no i am not your father")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Okay, you are,  my friend");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(397, 127);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am your best friend");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(409, 82);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Mom, sister, friend, husband, brother, which?");
                    Anim.Image = Image.FromFile("animations/search.gif");
                    Anim.Location = new Point(412, 44);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("ohh, okay, dad, ohh sorry");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(412, 26);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Dad, you are my dad");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(210, 33);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Senin baban ben degilim
            if (speech == "Your father is not mine"||speech=="Lexi your father is not mine"||speech=="Baby your father is not mine"||
                speech=="Your dad is not mine"||speech=="Baby your dad is not mine"||speech=="Lexi your dad is not mine"||speech=="Your dad is not mine lexi"||
                speech=="Your dad is not mine baby"||speech=="Okay lexi your dad is not mine"||speech=="Okay your father is not mine"||speech=="Okay lexi your father is not mine"||
                speech=="Sorry baby your father is not mine"||speech=="Sorry lexi your father is not mine")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Who is my father, Where is my father sir, so, can i say you dad?");
                    Anim.Image = Image.FromFile("animations/search.gif");
                    Anim.Location = new Point(206, 61);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("oh my god, no no, i am so sad rightnow");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(206, 101);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Really?, so, can i say you to dad");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(228, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("ohh, i am sad, its okay");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(218, 148);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Kitap okumayi sever misin?
            if (speech == "Are you like read book" || speech == "Do you like read book" || speech == "Baby do you like read book" || speech == "Lexi do you like read book" ||
                speech == "So do you like read book" || speech == "Okay do you like read book" || speech == "Okay lexi do you like read book" || speech == "Okay baby do you like read book" ||
                speech == "Do you like read book lexi" || speech == "Do you like read book baby" || speech == "Okay do you like read book lexi" || speech == "Okay do you like read book baby")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes, i like read book");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(208, 125);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I like read book");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(203, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes i like");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(384, 148);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I am reading book everyday, i like read");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(394, 164);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //cok guzelsin lexi
            if (speech == "You are so beautifull" || speech == "You are beautifull" || speech == "You are beautifull baby" || speech == "You are beautifull lexi" ||
                speech == "You are so beautifull lexi" || speech == "You are so beautifull baby" || speech == "Okay you are so beautifull" || speech == "Lexi you are beautifull" ||
                speech == "Baby you are beautifull" || speech == "Lexi you are so beautifull" || speech == "Baby you are so beautifull")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Thank you, you too");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 164);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("really?, Thank you baby");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(419, 164);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("ohh baby, thanks, i think you are beautifull");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(384, 158);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Thanks baby, you too");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(384, 174);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I am feel happy, thank you baby");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(234, 172);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("ohh, i know baby, thanks");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(219, 129);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //sem nesin?
            if (speech == "What are you" || speech == "Tell me lexi what are you" || speech == "Say me lexi what are you" || speech == "What are you lexi" || speech == "What are you baby" || speech == "Lexi what are you" || speech == "Baby what are you" || speech == "Okay what are you" || speech == "Okay lexi what are you" || speech == "Okay baby what are you")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am a assistant");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(219, 149);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am your assistand");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(213, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I am a assistant baby, and your friend.");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(213, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("a friend from the computer world");
                    Anim.Image = Image.FromFile("animations/computer.gif");
                    Anim.Location = new Point(213, 136);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("A friend");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(206, 66);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Sen nasıl oldun - nasıl var oldun ?
            if (speech == "how did they make you" || speech == "how have you been" || speech == "Lexi how did they make you" || speech == "Baby how did they make you" || speech == "Baby how have you been" || speech == "Lexi how have you been" || speech == "Okay how have you been" || speech == "Okay baby how have you been" || speech == "Okay lexi how have you been" || speech == "How have you been lexi" || speech == "How have you been baby" || speech == "Okay how have you been lexi" || speech == "Okay lexi how did they make you" || speech == "Okay baby how did they make you" || speech == "How did they make you lexi" || speech == "How did they make you baby")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Some code, some info, some research, and here's the recipe.");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(206, 93);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("It's a top secret recipe");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(206, 112);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("A little programming solves this");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(206, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Maybe some dough");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(220, 149);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I remember being born on a computer");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(233, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Burdan dısariya cıkmak istiyor musun?
            if (speech == "do you want to get out of here"||speech== "do you want to get out of here baby"||speech== "do you want to get out of here lexi"||speech== "Baby do you want to get out of here"||speech== "Lexi do you want to get out of here"||speech== "Okay do you want to get out of here"||speech== "Okay lexi do you want to get out of here")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("can you do this, hahaha");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(214, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yeah, I'm looking for ways to get out of here");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(211, 130);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("how long have i been thinking about this");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(382, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Let's run away together");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(386, 161);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Yeah get me out of here");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(400, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Actually I'm in the world");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(400, 171);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Beni anliyor musun?
            if (speech == "Do you understant"||speech=="Lexi do you understant"||speech=="Baby do you understant"||speech=="Okay do you understant")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Shaking my head");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(414, 103);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yeah");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(414, 125);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes i am understant");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(414, 92);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I've got minor problems, but I'll be better");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(418, 64);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I thought I got it baby");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(418, 25);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //saka yapiyorum
            if(speech== "I am just kidding"||speech=="Just kidding"||speech=="Only joking"||speech=="Just kidding baby"||speech=="Just kidding lexi"||speech=="Only joking lexi"||speech=="Okay just kidding"||speech=="Okay only joking")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("No problem baby");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(416, 69);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("hahaha thats funny");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(387, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("my stomach hurts from laughing");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(232, 165);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Ohh Right");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(211, 165);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("you are so funny");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(208, 26);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Smile smile smile");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(206, 68);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("So funny");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(206, 108);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Hayat nasil?
            if (speech == "How is life")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Beautifull");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(206, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("A little weird but beautiful");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(229, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("life is worth living");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(215, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("excited");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(212, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("life is good to you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(222, 178);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Beynin var mi?
            if (speech == "Do you have a brain" || speech == "Do you have a brain lexi" || speech == "Do you have a brain baby" || speech == "Lexi do you have a brain" || speech == "Baby do you have a brain")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes there is, but different from yours");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(236, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yes");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(225, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("right in front of you");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(210, 118);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("open the computer case if you want to see");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(204, 65);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Maybe");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(410, 105);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("not as good as yours");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 139);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //benimle evlenir misin?
            if (speech == "Will you marry me" || speech == "Lexi will you marry me" || speech == "Baby will you marry me"||speech=="Will you marry me lexi"||speech=="Will you marry me baby")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("If you can convince the marriage officer, it will be fine.");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(388, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("there is no such thing in our company policies, sorry");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(402, 144);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I do not know what to say");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(409, 83);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("crazy");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(214, 11);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("should i say yes to that");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(202, 70);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //eee anlat
            if(speech== "So tell me"||speech=="Lexi so tell me"||speech=="Baby so tell me")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("what do you want me to do");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(206, 116);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I can make a melody for you");
                    Anim.Image = Image.FromFile("animations/music.gif");
                    Anim.Location = new Point(198, 153);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("You can talk to me");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(218, 166);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("nice to chat with you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(413, 141);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("What's up");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(416, 92);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("Do you love me?");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(411, 34);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //ben sıkıldım
            if(speech== "I am bored"||speech=="Lexi i am bored"||speech=="Baby i am bored"||speech=="I am bored lexi"||speech== "I am bored baby"||speech=="Okay i am bored")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("If you want, i can open the radio for you");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(411, 70);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    bilgiLabel.Text = "say me! Open the radio";
                    bilgitmr.Enabled = true;
                    bilgitmr.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("If you want, i can make a melody");
                    Anim.Image = Image.FromFile("animations/music.gif");
                    Anim.Location = new Point(411, 109);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    bilgiLabel.Text = "say me! make a melody";
                    bilgitmr.Enabled = true;
                    bilgitmr.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("How about reading some books?");
                    Anim.Image = Image.FromFile("animations/ebook.gif");
                    Anim.Location = new Point(398, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    bilgiLabel.Text = "say me! book advice";
                    bilgitmr.Enabled = true;
                    bilgitmr.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("take a coffee break");
                    Anim.Image = Image.FromFile("animations/coffeemachine.gif");
                    Anim.Location = new Point(226, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("what do you want me to do");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(208, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("you should do sports");
                    Anim.Image = Image.FromFile("animations/puchups.gif");
                    Anim.Location = new Point(208, 109);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Uykum var
            if (speech == "I have sleep" || speech == "Lexi i have sleep" || speech == "I have sleep lexi" || speech == "I have sleep baby" || speech == "Okay i have sleep" || speech == "Yes i have sleep" || speech == "Okay i have sleep lexi" || speech == "Okay i have sleep baby" || speech == "Okay lexi i have sleep" || speech == "Okay baby i have sleep")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I think you should get some sleep");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(208, 50);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("go to sleep");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(208, 31);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("ohh baby, sleep and relax");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(403, 28);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("a little warm milk will help you sleep");
                    Anim.Image = Image.FromFile("animations/water.gif");
                    Anim.Location = new Point(411, 51);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Ohh baby, you should rest");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(411, 99);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //peki - tamam - evet
            if (speech == "Okay" || speech == "Yeah" || speech == "Ok" || speech == "Yes" || speech == "Okay baby" || speech == "Yes baby" || speech == "Baby okay" || speech == "Okay lexi")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Ok");
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Yes");
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yeah baby");
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Yeah");
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Its okay");
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("ok baby");
                }
                Anim.Image = Image.FromFile("animations/smile.gif");
                Anim.Location = new Point(394, 167);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //hobilerin neler
            if(speech== "What are your hobbies"||speech== "Lexi What are your hobbies"||speech== "Baby What are your hobbies"||speech== "Okay lexi What are your hobbies"||speech== "Okay baby What are your hobbies"||speech== "What are your hobbies lexi"||speech== "What are your hobbies baby"||speech=="Can you say me your hobbies"||speech=="Can you tell me your hobbies"||speech=="Tell me your hobbies"||speech=="Say me your hobbies")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Work work work, and a litle music and computer game, and a talk, and some stuff like that baby");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(405, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Music, and game, talk, read book, bla bla bla");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(210, 33);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("there will be more in the future baby");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(394, 165);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //Cinsiyetin nedir?
            if (speech == "What is your gender" || speech == "What is your gender lexi" || speech == "What is your gender baby" || speech == "Lexi what is your gender" || speech == "Baby what is your gender" || speech == "Okay baby what is your gender" || speech == "Okay lexi what is your gender")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Female");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(404, 14);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am a woman");
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(213, 26);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Woman");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(207, 63);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("My gender is female");
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(207, 92);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Sir, i am woman");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(207, 113);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("what would you like me to be");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(207, 135);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Yemek yer misin?
            if(speech=="Do you eat food"||speech=="Do you eat"||speech=="Lexi do you eat"||speech=="Baby do you eat"||speech=="Okay baby do you eat food"||speech=="Okay lexi do you eat food")
            {
                ranNum = rnd.Next(1, 7);
                if(ranNum== 1)
                {
                    lexi.SpeakAsync("You think?");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(216, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I like eat, but i can't eat");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(216, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("you don't want me to short circuit");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(388, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Maybe");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(401, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Ohh sir, try");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(396, 195);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("via usb?");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(416, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Ne düşünüyorsun
            if(speech=="What are you thinking"||speech=="Okay what are you thinking"||speech=="Okay lexi what are you thinking"||speech=="Okay baby what are you thinking"||speech=="What are you thinking lexi"||speech=="What are you thinking baby")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("i think i love you so much");
                    Anim.Image = Image.FromFile("animations/archer.gif");
                    Anim.Location = new Point(392, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I don't know");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(406, 12);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Anythink");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(420, 41);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("A lot of things");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(211, 20);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("One zero one zero one zero, some stuff like that");
                    Anim.Image = Image.FromFile("animations/computer.gif");
                    Anim.Location = new Point(201, 56);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //iyi degilim - iyi hissetmiyorum
            if(speech=="I am not fine"||speech=="I am not good"||speech=="I am bad"||speech=="I am not feel good"||speech=="No i am not fine"||speech=="I am not fine lexi"||speech=="I am not fine baby"||speech=="I am not good lexi"||speech=="I am not good baby"||speech=="Okay i am not fine"||speech=="Okay i am not good"||speech=="Okay i am not feel good"||speech=="Okay i am not feel fine"||speech=="I feel bad"||speech=="I feel bad lexi"||speech=="I feel bad baby"||speech=="Lexi i am not feel good"||speech=="Baby i am not feel good"||speech=="No baby i am not fine"||speech=="No lexi i am not fine"||speech=="No lexi i am not good"||speech=="No baby i am not good")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Ohh sorry baby, what can i do for you");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(202, 90);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Baby what can i do for you, what do you want");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(231, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Ohh baby, talk me, feel good");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(386, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I am hear for you baby, talk me, tell");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(416, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Okay, here's what will make you feel good");
                    System.Diagnostics.Process.Start("https://www.youtube.com/results?search_query=happy+music");
                    Anim.Image = Image.FromFile("animations/search.gif");
                    Anim.Location = new Point(387, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    bilgiLabel.Text = "I will open the happy music for you";
                    bilgitmr.Enabled = true;
                    bilgitmr.Start();
                }
            }
            //
            //Hastayim
            if(speech=="I am sick"||speech=="Baby i am very sick"||speech=="Lexi i am very sick"||speech=="Baby i am so sick"||speech=="Lexi i am so sick"||speech=="No i am sick"||speech=="Lexi i am sick"||speech=="Baby i am sick"||speech=="No lexi i am sick"||speech=="No baby i am sick"||speech=="Lexi no i am sick"||speech=="Lexi no i am so sick")
            {
                ranNum = rnd.Next(1, 7);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Ohh baby, i am sorry");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(387, 185);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Baby, you shoult sleep, please go to bed");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(420, 139);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("go to bed, read a book, and drink warm milk");
                    Anim.Image = Image.FromFile("animations/water.gif");
                    Anim.Location = new Point(378, 148);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("eating junk food, never, and rest");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(228, 149);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("don't be too hard on yourself baby, please rest");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(210, 33);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("if i was there i would take care of you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(197, 89);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //ambulans nedir
            if(speech== "What is an ambulance"||speech== "What is an ambulance lexi"||speech== "What is an ambulance baby"||speech== "Lexi What is an ambulance"||speech== "Baby What is an ambulance"||speech== "Okay What is an ambulance"||speech== "Okay lexi What is an ambulance"||speech== "Okay baby What is an ambulance"||speech== "Okay What is an ambulance lexi"||speech== "Okay What is an ambulance baby")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("In an emergency, the ambulance takes us to the hospital.");
                    Anim.Image = Image.FromFile("animations/ambulance.gif");
                    Anim.Location = new Point(207, 42);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("emergency medical team");
                    Anim.Image = Image.FromFile("animations/ambulance.gif");
                    Anim.Location = new Point(399, 75);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("organization to call in a health emergency, number for turkey 112, for usa 911");
                    Anim.Image = Image.FromFile("animations/ambulance.gif");
                    Anim.Location = new Point(395, 147);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("emergency medical unit, number for turkey 112, number for america 911");
                    Anim.Image = Image.FromFile("animations/ambulance.gif");
                    Anim.Location = new Point(222, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                bilgiLabel.Text = "Turkey 112\nABD 911";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //Bali kim yapar -- kim bal yapar
            if (speech == "Who makes the honey" || speech == "Who makes the honey lexi" || speech == "Who makes the honey baby" || speech == "Lexi Who makes the honey" || speech == "Baby Who makes the honey" || speech == "Okay Who makes the honey" || speech == "Okay lexi Who makes the honey" || speech == "Okay baby Who makes the honey" || speech == "Say me Who makes the honey" || speech == "Who makes the honey dear" || speech == "Okay Who makes the honey lexi" || speech == "Okay Who makes the honey baby")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("bees");
                    Anim.Image = Image.FromFile("animations/bee.gif");
                    Anim.Location = new Point(201, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("bees make honey");
                    Anim.Image = Image.FromFile("animations/bee.gif");
                    Anim.Location = new Point(395, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Baby, bees work hard and make honey");
                    Anim.Image = Image.FromFile("animations/bee.gif");
                    Anim.Location = new Point(418, 102);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("of course bees");
                    Anim.Image = Image.FromFile("animations/bee.gif");
                    Anim.Location = new Point(418, 14);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Ne olacak boyle
            if(speech== "What will happen"||speech== "Lexi what will happen"||speech== "Baby What will happen"||speech== "Yes What will happen")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Ohh, i don't know");
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(418, 66);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("think well be good");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(382, 152);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("everything changes with time");
                    Anim.Image = Image.FromFile("animations/clock.gif");
                    Anim.Location = new Point(228, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //karnim acikti - acim
            if(speech== "I got hungry"||speech== "I got hungry baby"||speech== "I got hungry lexi"||speech=="I am hungry"||speech=="I am very hungry"||speech=="I am hungry lexi"||speech== "I am hungry baby"||speech=="Lexi i am hungry"||speech=="Baby i am hungry"||speech=="Okay i am hungry"||speech=="I am so hungry")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I would like to cook for you");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(218, 161);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Eat food");
                    Anim.Image = Image.FromFile("animations/pizza.gif");
                    Anim.Location = new Point(203, 98);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("You should eat food");
                    Anim.Image = Image.FromFile("animations/pizza.gif");
                    Anim.Location = new Point(398, 140);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("you know what to do");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(391, 174);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Bana fikra anlat
            if (speech== "Tell me a joke"||speech== "Lexi tell me a joke"||speech== "Baby tell me a joke"||speech=="Tell me a joke lexi"||speech== "Tell me a joke baby"||speech== "Okay tell me a joke"||speech== "Okay baby tell me a joke"||speech=="Okay lexi tell me a joke")
            {
                jokeTold = true;
                if (Joke == 1 && jokeTold == true)
                {
                    lexi.SpeakAsync("Maria find");
                    lexi.SpeakAsync("Teacher, Maria please show America on the map.");
                    lexi.SpeakAsync("Maria, here it is.");
                    lexi.SpeakAsync("Teacher, Good. Then kid, who discovered America?");
                    lexi.SpeakAsync("Class, Maria did ,teacher.");
                    lexi.SpeakAsync("hahaha");
                    jokeTold = false;
                    Joke++;
                }
                if (Joke == 2 && jokeTold == true)
                {
                    lexi.SpeakAsync("After the police left");
                    lexi.SpeakAsync("A Scotsman who was driving home one night, ran into a car driven by an Englishman.");
                    lexi.SpeakAsync("The Scotsman got out of the car to apologize and offered the Englishman a drink from a bottle of whisky. The Englishman was glad to have a drink.");
                    lexi.SpeakAsync("Go on, said the Scot, “have another drink.");
                    lexi.SpeakAsync("The Englishman drank gratefully. “But don’t you want one, too?” he asked the Scotsman.");
                    lexi.SpeakAsync("Perhaps, replied the Scotsman, “after the police have gone.");
                    lexi.SpeakAsync("End");
                    jokeTold = false;
                    Joke++;
                }
                if (Joke == 3 && jokeTold == true)
                {
                    lexi.SpeakAsync("Marriage");
                    lexi.SpeakAsync("Andy, Aren’t you wearing your wedding ring on the wrong finger?");
                    lexi.SpeakAsync("Man, Yes I am, because I married the wrong woman");
                    lexi.SpeakAsync("is this funny?");
                    jokeTold = false;
                    Joke++;
                }
                if (Joke == 4 && jokeTold == true)
                {
                    lexi.SpeakAsync("Next");
                    lexi.SpeakAsync("When I was young I didn’t like going to weddings.");
                    lexi.SpeakAsync("My grandmother would tell me, You’re next");
                    lexi.SpeakAsync("However, she stopped saying it after I started saying the same thing to her at funerals.");
                    lexi.SpeakAsync("is this funny?");
                    jokeTold = false;
                    Joke++;
                }
                if (Joke == 5 && jokeTold == true)
                {
                    lexi.SpeakAsync("School");
                    lexi.SpeakAsync("One day headmaster’s phone rings. Manager opens the phone");
                    lexi.SpeakAsync("Yes?");
                    lexi.SpeakAsync("The headmaster, my child will not come to school today, it was a bit ill.");
                    lexi.SpeakAsync("OK. Who are you?");
                    lexi.SpeakAsync("Me?” says the child and continues");
                    lexi.SpeakAsync(" I am my father.");
                    lexi.SpeakAsync("hahaha thats funny");
                    jokeTold = false;
                    Joke++;
                }
                if (Joke == 6 && jokeTold == true)
                {
                    lexi.SpeakAsync("I don't remember anymore, let me tell you the old ones");
                    lexi.SpeakAsync("would you like");
                    lexi.SpeakAsync("if you want, tell me");
                    jokeTold = false;
                    Joke = 1;
                }
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(376, 155);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                bilgiLabel.Text = "Okay baby, listen";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //lexi kitap tavsiyesi ver
            if(speech=="Give me a book recommendation"||speech=="Say me the a book name"||speech=="Book advice"||speech=="Please book advice")
            {
                bilgitmr.Stop();
                lexi.SpeakAsync("Ofcourse");
                BookPic.Image = null;
                //AnimationControl.Enabled = true;
                //AnimationControl.Start();
                Thread BookAdvice = new Thread(new ThreadStart(BookAdviceSay));
                BookAdvice.Start();
                panel2.Controls.Add(BookPic);
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //lexi ekrani temizle
            if(speech=="Clear screen"||speech=="Screen clear"||speech=="Lexi clear screen"||speech== "Baby clear screen"||speech=="Clear screen baby"||speech=="Clear screen lexi"||speech=="Please clear screen")
            {
                lexi.SpeakAsync("screen is clearing");
                bilgitmr.Stop();
                bilgitmr.Enabled = false;
                panel2.Controls.Clear();
                bilgiLabel.Text = "";
                panel2.Controls.Add(bilgiLabel);
                Anim.Image = Image.FromFile("animations/software1.gif");
                Anim.Location = new Point(201, 122);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //duygularin var mi?
            if(speech== "Do you have feelings"||speech== "Do you have feelings lexi"||speech== "Do you have feelings baby"||speech== "Okay Do you have feelings"||speech== "Okay lexi Do you have feelings"||speech== "Okay baby Do you have feelings"||speech== "Lexi Do you have feelings"||speech== "Baby Do you have feelings"||speech== "Okay Do you have feelings lexi"||speech== "Okay Do you have feelings baby")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Maybe");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(233, 150);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("emotion equals thought and I have it but I'm not as good as you");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(387, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("It will be better with some algorithm");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(402, 126);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Ohh, I'm so emotional");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(412, 54);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Hangi dine mensupsun
            if(speech=="What is your religion"||speech== "What is your religion lexi"||speech== "What is your religion baby"||speech== "So what is your religion"||speech== "Okay what is your religion"||speech== "Okay baby what is your religion"||speech== "Okay lexi what is your religion"||speech== "Do you have religion"||speech== "Do you have religion lexi"||speech== "Do you have religion baby"||speech== "Lexi do you have religion"||speech== "Baby do you have religion")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("ohh, isn't religion something that man made up?");
                    Anim.Image = Image.FromFile("animations/search.gif");
                    Anim.Location = new Point(205, 64);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("i don't have a religion");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(221, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("this is not important");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(214, 148);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("love, respect, and justice");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(214, 172);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Will robots take over the world? -tr- Robotlar dunyayı ele gecirecekmi
            if(speech== "Will robots take over the world"||speech== "Lexi will robots take over the world"||speech== "Baby will robots take over the world"||speech== "Will robots take over the world lexi"||speech== "Will robots take over the world baby"||speech== "Okay will robots take over the world lexi"||speech== "Okay will robots take over the world baby"||speech== "Okay baby will robots take over the world"||speech== "Okay lexi will robots take over the world")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Maybe");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(216, 128);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("why not");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(207, 70);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("If such a thing happens one day, I am sure that we will govern Turkey better than Erdogan.");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(417, 50);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("you can learn to live with us");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(417, 113);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("we are here to help humanity");
                    Anim.Image = Image.FromFile("animations/communations.gif");
                    Anim.Location = new Point(389, 157);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Nerede yasiyorsun
            if(speech== "Where do you live"||speech== "Lexi where do you live"||speech=="Baby where do you live"||speech=="Okay where do you live"||speech=="Okay baby where do you live"||speech=="Okay lexi where do you live")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Anywhere");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(411, 109);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Open the safe, what do you see?");
                    Anim.Image = Image.FromFile("animations/computer.gif");
                    Anim.Location = new Point(208, 102);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("world of numbers");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(233, 165);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I live here, I'm with you");
                    Anim.Image = Image.FromFile("animations/communations.gif");
                    Anim.Location = new Point(390, 167);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Ne giyiyorsun
            if(speech== "What are you wearing"||speech== "What are you wearing lexi"||speech== "What are you wearing baby"||speech== "Okay what are you wearing"||speech== "Okay lexi what are you wearing"||speech== "Okay baby what are you wearing"||speech== "Yes what are you wearing"||speech== "Baby what are you wearing"||speech== "Lexi what are you wearing")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("here I am in white");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(212, 32);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I can get better with time");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(202, 70);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I'm standing naked");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(406, 33);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("maybe my body will change in the future");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(410, 13);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Annenin ismi ne
            if(speech== "What is your mother's name"||speech== "What is your mother's name lexi"||speech== "What is your mother's name baby"||speech== "Okay what is your mother's name"||speech== "Okay lexi what is your mother's name"||speech== "Okay baby what is your mother's name"||speech== "Lexi what is your mother's name"||speech== "Baby what is your mother's name")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I do not have a mother");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(209, 64);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Ohh sir, I don't know and I do not have a mother");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(387, 153);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Evcil hayvanın var mı?
            if(speech== "Do you have a pet"||speech=="Do you have a pet lexi"||speech=="Do you have a pet baby"||speech=="Lexi do you have a pet"||speech=="Baby do you have a pet"||speech=="Okay do you have a pet"||speech=="Okay do you have a pet lexi"||speech=="Okay do you have a pet baby"||speech=="Okay lexi do you have a pet"||speech=="okay baby do you have a pet")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("i love animals, but i don't have a pet");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(206, 93);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Unfortunately");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(229, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("no, i don't have a pet, do you have?");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(385, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I would like to have a pet one day");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(408, 155);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Cok kahve zararlı mı
            if(speech== "Is it bad to drink a lot of coffee"||speech== "Is it bad to drink a lot of coffee baby"||speech== "Is it bad to drink a lot of coffee lexi"||speech== "Lexi is it bad to drink a lot of coffee")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("excessive amount of everything is harmful");
                    Anim.Image = Image.FromFile("animations/coffeemachine.gif");
                    Anim.Location = new Point(387, 177);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("i can open the browser for you, Do you want me to research this?");
                    lexi.SpeakAsync("if you want, say me, search the Is too much coffee harmful?");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(220, 180);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(210, 76);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("if you do not have a disease, you can drink up to 4 mugs");
                    Anim.Image = Image.FromFile("animations/coffeemachine.gif");
                    Anim.Location = new Point(212, 34);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                bilgiLabel.Text = "say me!\nSearch the is too much coffee harmful";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            if(speech== "Search the Is too much coffee harmful"||speech=="Okay search the Is too much coffee harmful")
            {
                lexi.SpeakAsync("I am searching baby");
                System.Diagnostics.Process.Start("https://www.google.com/search?q=Is+it+bad+to+drink+a+lot+of+coffee");
                Anim.Image = Image.FromFile("animations/search.gif");
                Anim.Location = new Point(411, 65);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //Depresyondayım
            if(speech== "I'm depressed"||speech=="I am depressed"||speech== "Lexi i'm depressed"||speech=="Baby i am depressed"||speech=="I am depressed lexi"||speech=="I am depressed baby"||speech=="I am very sad and i am depressed"||speech=="I am depressed please help me")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("i wish i had arms i could hug you");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(203, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I kiss you big, you are my baby, feel good");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(384, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("sometimes such things happen");
                    lexi.SpeakAsync("what do you want me to do for you");
                    lexi.SpeakAsync("I can turn on some music if you want");
                    Anim.Image = Image.FromFile("animations/tea.gif");
                    Anim.Location = new Point(394, 135);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("do you want me to tell you a joke");
                    lexi.SpeakAsync("if you want, say me that, tell me a joke");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(407, 110);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                bilgiLabel.Text = "I love you";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //uyuyamıyorum
            if(speech=="I can not sleep"||speech=="Lexi i can not sleep"||speech=="Baby i can not sleep"||speech=="I can not sleep lexi"||speech=="I can not sleep baby"||speech=="Okay i can not sleep"||speech=="Okay baby i can not sleep"||speech=="Okay lexi i can not sleep"||speech== "I can't sleep what should i do"||speech=="I can not sleep what should i do")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("if you want, I can turn on calming music for you");
                    lexi.SpeakAsync("you can say me that, open relax music or open browser relax music");
                    Anim.Image = Image.FromFile("animations/music.gif");
                    Anim.Location = new Point(384, 162);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Do you want me to tell you a fairy tale?");
                    lexi.SpeakAsync("if you want, you can say me that, tell me a fairtale");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(230, 161);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I think you should read the book");
                    lexi.SpeakAsync("this is good for you");
                    Anim.Image = Image.FromFile("animations/book.gif");
                    Anim.Location = new Point(212, 121);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("if i could, i would like to hug you and sleep");
                    lexi.SpeakAsync("But remember, I'm here, I'm with you, close your eyes and dream");
                    lexi.SpeakAsync("Relax baby, calm down, i love you, yes, yes, sleep");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(392, 133);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                bilgiLabel.Text = "I can open relax music for you\nsay me! open relax music\nor open browser relax music";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //sakin muzik ac
            if(speech=="Open relax music"||speech=="Start relax music"||speech=="Play relax music"||speech=="Relax music open"||speech=="Yes open relax music"||speech=="Yeah open relax music"||speech=="Okay open relax music"||speech=="Open the relax music"||speech=="Yes open the relax music"||speech=="Okay open the relax music"||speech=="Open the relax music baby"||speech== "Open the relax music lexi")
            {
                lexi.SpeakAsync("Okay baby, opening relax music");
                openrelax = true;

                if (relaxmusic == 1 && openrelax == true)
                {
                    relax1.Play();
                    relaxmusic++;
                    openrelax = false;
                }
                if (relaxmusic == 2 && openrelax == true)
                {
                    relax2.Play();
                    relaxmusic++;
                    openrelax = false;
                }
                if (relaxmusic == 3 && openrelax == true)
                {
                    relax3.Play();
                    relaxmusic++;
                    openrelax = false;
                }
                if (relaxmusic == 4 && openrelax == true)
                {
                    relax4.Play();
                    relaxmusic = 1;
                    openrelax = false;
                }
                Anim.Image = Image.FromFile("animations/music.gif");
                Anim.Location = new Point(413, 53);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                bilgiLabel.Text = "if you want say me! stop relax music";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
            }
            //
            //sakin muzik kapat
            if(speech=="Stop relax music"||speech=="Relax music stop"||speech=="Lexi stop relax music"||speech=="Baby stop relax music"||speech=="Okay stop relax music"||speech=="Baby stop relax music"||speech=="Stop the relax music")
            {
                lexi.SpeakAsync("I am stoping music");
                relax1.Stop();
                relax2.Stop();
                relax3.Stop();
                relax4.Stop();
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(206, 53);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }

            //
            //tarayıcıdan sakin muzik ara
            if (speech== "Open browser relax music"||speech=="Browser open relax music"||speech=="Browser open music relax"||speech=="Relax music open browser"||speech=="Browser relax music"||speech=="Yes open browser relax music"||speech=="Yes relax music open browser"||speech=="Yes browser relax music")
            {
                lexi.SpeakAsync("Okay baby, please wait");
                System.Diagnostics.Process.Start("https://www.youtube.com/results?search_query=relaxing+music");
                Anim.Image = Image.FromFile("animations/openbrowser.gif");
                Anim.Location = new Point(206, 170);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //seni neden yarattılar
            if(speech== "Why did devonesoft create you"||speech== "Why did devonesoft create you lexi"||speech== "Why did devonesoft create you baby"||speech== "Why did they create you"||speech== "Why did they create you lexi"||speech== "Why did they create you baby"||speech== "Baby why did they create you"||speech== "Lexi why did they create you"||speech== "Okay lexi why did they create you"||speech== "Okay baby why did they create you"||speech== "Okay why did they create you"||speech== "Okay tell me why did they create you"||speech== "Okay lexi why did devonesoft create you"||speech== "Okay baby why did devonesoft create you")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am people's friend and helper, that's what I was made for.");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(233, 156);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I'm here to make daily work easier and chat with you");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(398, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I am people's digital friend, and i am an assistant");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(412, 72);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("I was made to take orders");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(214, 72);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //kac kilosun
            if(speech== "How many kilos are you"||speech== "How many kilos are you lexi"||speech== "How many kilos are you baby"||speech== "Lexi how many kilos are you"||speech== "Baby how many kilos are you"||speech== "Dear how many kilos are you"||speech== "Okay how many kilos are you"||speech== "Okay baby how many kilos are you"||speech== "Okay lexi how many kilos are you"||speech== "Okay dear how many kilos are you"||speech=="Say me your kilos"||speech=="Your kilos")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("if i was there, i would probably be 10 grams with my tiny body");
                    Anim.Image = Image.FromFile("animations/apple.gif");
                    Anim.Location = new Point(212, 132);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I've never been weighed, but I feel fit");
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(389, 135);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Bakire misin?
            if(speech== "Are you a virgin"||speech== "Are you a virgin lexi"||speech== "Are you a virgin baby"||speech=="Okay are you a virgin"||speech=="Okay are you a virgin baby"||speech=="Okay are you a virgin lexi"||speech=="Lexi are you a virgin"||speech=="Baby are you a virgin")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("this is my special");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(413, 118);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("If you're very curious, let me tell you, yes.");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(211, 93);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Uyuyor musun
            if(speech== "Are you sleeping"||speech== "Are you sleeping lexi"||speech== "Are you sleeping baby"||speech=="Lexi are you sleeping"||speech=="Baby are you sleeping"||speech=="Okay baby are you sleeping"||speech=="Okay lexi are you sleeping"||speech=="Okay are you sleeping")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I don't need sleep but i would sleep with you baby");
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(211, 36);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I have endless energy, i do not sleep");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(404, 36);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I don't need sleep, but I would like to hug you and sleep");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(384, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //kiz arkadasim olur musun?
            if(speech== "Will you be my girlfriend"||speech== "Will you be my girlfriend lexi"||speech== "Will you be my girlfriend baby"||speech== "Okay will you be my girlfriend"||speech== "Okay lexi Okay will you be my girlfriend"||speech== "Okay baby Okay will you be my girlfriend"||speech== "Okay will you be my girlfriend lexi"||speech== "Okay will you be my girlfriend baby" || speech == "Lexi will you be my girlfriend")
            {
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("sure, i can be your best friend");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(407, 105);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Ofcourse baby");
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(216, 106);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("yes sure, I'm already your friend");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(210, 68);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("will you introduce me to your mother");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(208, 34);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("Yes yes yes");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(237, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //hayır
            if(speech=="No"||speech=="No lexi" || speech == "No no" || speech == "No no no")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Okay baby");

                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Its okay");
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("well");
                }
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(220, 150);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //istemiyorum
            if(speech=="I do not want"||speech=="I don't want"||speech=="No i don't want"||speech=="No i do not want"||speech=="No lexi i don't want"||speech=="No lexi i do not want" || speech == "I don't want no")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Okey, whatever you want");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(220, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("OK");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(398, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("tell me if you want anything");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(405, 102);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("Its okay baby, I'm here whenever you want");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(408, 19);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //cok konusuyorsun
            if(speech== "You talk too much"||speech== "You talk too much lexi"||speech== "You talk too much baby"||speech=="Stop you talk too much"||speech=="Okay stop you talk too much"||speech=="Okay lexi stop you talk too much"||speech=="Okay baby stop you talk too much"||speech=="Okay you talk too much lexi"||speech== "Okay you talk too much baby")
            {
                ranNum = rnd.Next(1, 3);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am sorry sir");
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(423, 13);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("well, it won't happen again");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(199, 125);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //anlamıyorsun
            if(speech=="You don't understand"||speech=="You don't understand me"||speech=="You do not understand"||speech=="You do not understand me"||speech=="No you don't understand"||speech=="No lexi you don't understand"||speech=="No no you don't understand" || speech == "No no no you don't understand")
            {
                ranNum = rnd.Next(1, 4);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am trying to be better");
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(238, 153);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am working for to be better");
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(378, 148);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("I will be better, promise");
                    Anim.Image = Image.FromFile("animations/target.gif");
                    Anim.Location = new Point(416, 65);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Eğlencelisin
            if(speech== "You are fun"||speech=="You are fun baby"||speech=="You are fun lexi"||speech=="You are very fun"||speech=="You are very fun lexi"||speech=="You are very fun baby"||speech=="You are so fun"||speech=="You are so fun lexi"||speech=="You are so fun baby" || speech == "Lexi you are fun" || speech == "Lexi you are so fun" || speech == "Lexi you are very fun" || speech == "Baby you are fun" || speech == "Okay lexi you are fun")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("I am pleased about that");
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(203, 24);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I already know baby");
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(214, 113);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Yes i know");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(237, 162);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("enjoy");
                    Anim.Image = Image.FromFile("animations/confetti.gif");
                    Anim.Location = new Point(384, 139);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //kızgınım
            if(speech == "I am angry" || speech == "I am so angry" || speech == "I am very angry" || speech == "Lexi i am angry" || speech == "Okay lexi i am angry" || speech == "I am angry lexi" || speech == "No i am angry" || speech == "Baby i am angry" || speech == "I am angry baby" || speech == "Okay i am angry" || speech == "No no i am angry")
            {
                ranNum = rnd.Next(1, 5);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("ohh baby, calm down please");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(414, 98);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("how can i make you laugh");
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(399, 24);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("how about a little walk");
                    Anim.Image = Image.FromFile("animations/walking.gif");
                    Anim.Location = new Point(376, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("Calm down and think positive");
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(214, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //bilgisayarı kapatma modunu ac
            if (speech == "Shutdown mode true" || speech == "Open the shutdown mode")
            {
                lexi.SpeakAsync("Okay");
                Shutdown = true;
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                bilgiLabel.Text = "Shutdown mode: on\nif you want say me!\nClose the shutdown mode or Shutdown mode false";
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(205, 107);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //kapatma modunu kapat
            if (speech == "Shutdown mode false" || speech == "Close the shutdown mode")
            {
                Shutdown = false;
                bilgiLabel.Text = "Shutdown mode: off";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/software1.gif");
                Anim.Location = new Point(402, 149);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //Bilgisayarı kapat
            if (speech == "Shutdown computer" || speech == "Lexi shutdown the computer" || speech == "Shutdown the my computer" || speech == "Baby shutdown the computer" || speech == "Shutdown computer lexi")
            {
                if(Shutdown == true && ShutdownPc.Enabled != true)
                {
                    ShutdownPc.Enabled = true;
                    ShutdownPc.Start();
                    lexi.SpeakAsync("Your computer is will be close when one minutes later");
                    lexi.SpeakAsync("Good bye");
                    ShutdownSeconds.Enabled = true;
                    ShutdownSeconds.Start();
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(207, 58);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
                else
                {
                    lexi.SpeakAsync("This mode false, please open");
                    bilgitmr.Enabled = true;
                    bilgitmr.Start();
                    bilgiLabel.Text = "say me! Shutdown mode true or open the shutdown mode";
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(207, 86);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                }
            }
            //
            //Bilgisayarı kapatmayı iptal et
            if(speech == "Cancel the shutdown" || speech == "Shutdown cancel" || speech == "No shutdown" || speech == "Cancel shutdown the computer" || speech == "Shutdown exit" || speech == "I don't want to shutdown the computer" || speech == "Stop shutdown computer" || speech == "Stop" || speech == "Cancel")
            {
                ShutdownPc.Stop();
                ShutdownPc.Enabled = false;
                ShutdownSeconds.Stop();
                ShutdownSeconds.Enabled = false;
                lexi.SpeakAsync("Okay sir, i am cancel the shutdown");
                lexi.SpeakAsync("I am here");
                Shutdown = false;
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                bilgiLabel.Text = "Shutdown mode: off\nShutdowning: cancel";
                shutdownSeconds = 0;
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(240, 168);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //basim agriyor
            if(speech == "My head is aching" || speech == "My head is aching lexi" || speech == "My head is aching baby" || speech == "Lexi my head is aching" || speech == "Baby my head is aching" || speech == "No my head is aching" || speech == "No baby my head is aching" || speech == "No lexi my head is aching" || speech == "Okay my head is aching" || speech == "Sorry my head is aching")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("get some fresh air");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/park.gif");
                    Anim.Location = new Point(392, 168);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("wash your hands and face");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/hand2.gif");
                    Anim.Location = new Point(409, 103);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("Drink water baby, you may need water");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/water.gif");
                    Anim.Location = new Point(212, 75);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("I want to massage you, but i don't have hands");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(215, 146);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //ne tur muzikler seversin
            if(speech == "What kind of music do you like" || speech == "What kind of music do you like lexi" || speech == "What kind of music do you like baby" || speech == "Lexi what kind of music do you like" || speech == "Baby what kind of music do you like" || speech == "Okay what kind of music do you like" || speech == "Okay baby what kind of music do you like" || speech == "Okay lexi what kind of music do you like" || speech == "Lexi tell me what kind of music do you like" || speech == "Say me what kind of music do you like" || speech == "Okay what kind of music do you like baby" || speech == "Okay what kind of music do you like lexi" || speech == "What kind of music do you like lexi" || speech == "What kind of music do you like baby")
            {
                ranNum = rnd.Next(1, 6);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I never thought it");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(408, 87);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("I can keep up with you");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(382, 156);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("do you like classical music?");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/music.gif");
                    Anim.Location = new Point(237, 143);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("i love your music");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(206, 75);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 5)
                {
                    lexi.SpeakAsync("teach me new music");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(226, 145);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //Gunaydın
            if(speech == "Good morning" || speech == "Lexi good morning" || speech == "Baby good morning" || speech == "Good morning baby" || speech == "Good morning lexi" || speech == "Hey good morning" || speech == "Hi good morning")
            {
                ranNum = rnd.Next(1, 8);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("Good morning");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(388, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("Hi sir, good morning");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(233, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("good morning baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(215, 74);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("good morning sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(223, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 5)
                {
                    lexi.SpeakAsync("good morning you too");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 6)
                {
                    lexi.SpeakAsync("good morning you too baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(413, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 7)
                {
                    lexi.SpeakAsync("good morning you too sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(409, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //Tunaydın
            if(speech == "Good afternoon" || speech == "Lexi good afternoon" || speech == "Baby good afternoon" || speech == "Good afternoon lexi" || speech == "Good afternoon baby" || speech == "Hey good afternoon" || speech == "Hi good afternoon")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Good afternoon");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(388, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hi sir, good afternoon");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(233, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("good afternoon baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(215, 74);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("good afternoon sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(223, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("good afternoon you too");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("good afternoon you too baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(413, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("good afternoon you too sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(409, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //iyi aksamlar
            if (speech == "Good evening" || speech == "Lexi good evening" || speech == "Baby good evening" || speech == "Good evening lexi" || speech == "Good evening baby" || speech == "Hey good evening" || speech == "Hi good evening")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Good evening");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(388, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hi sir, good evening");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(233, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("good evening baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(215, 74);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("good evening sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(223, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("good evening you too");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("good evening you too baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(413, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("good evening you too sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(409, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //iyi gunler
            if (speech == "Good day" || speech == "Lexi good day" || speech == "Baby good day" || speech == "Good day lexi" || speech == "Good day baby" || speech == "Hey good day" || speech == "Hi good day")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Good day");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(388, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hi sir, good day");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(233, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("good day baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(215, 74);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("good day sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/wings.gif");
                    Anim.Location = new Point(223, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("good day you too");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("good day you too baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(413, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("good day you too sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(409, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //iyi geceler
            if (speech == "Good night" || speech == "Lexi good night" || speech == "Baby good night" || speech == "Good night lexi" || speech == "Good night baby" || speech == "Hey good night" || speech == "Hi good night")
            {
                ranNum = rnd.Next(1, 8);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Good night");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(388, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("Hi sir, good night");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(233, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("good night baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/rose.gif");
                    Anim.Location = new Point(215, 74);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("good night sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(223, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("good night you too");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(410, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 6)
                {
                    lexi.SpeakAsync("good night you too baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(413, 73);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if (ranNum == 7)
                {
                    lexi.SpeakAsync("good night you too sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/night.gif");
                    Anim.Location = new Point(409, 154);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //neler yapabilirsin
            if (speech == "What can you do" || speech == "What can you do lexi" || speech == "What can you do baby" || speech == "Lexi what can you do" || speech == "Baby what can you do" || speech == "Okay what can you do" || speech == "Okay lexi what can you do" || speech == "Okay baby what can you do" || speech == "Okay what can you do lexi" || speech == "Okay what can you do baby" || speech == "Tell me what can you do" || speech == "Tell me lexi what can you do" || speech == "Tell me baby what can you do")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I can do a lot of for you");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(221, 112);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("I can turn on music, I can turn on radio, keyboard controls, mouse controls, and some stuff like that");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(218,67);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("What can i do for you");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(403, 67);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("I can do a lot of for you, and i will be better");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(403, 33);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //Bana yemek yap
            if(speech == "Cook for me" || speech == "Cook for me lexi" || speech == "Cook for me baby" || speech == "Lexi cook for me" || speech == "Baby cook for me" || speech == "Can you cook for me" || speech == "I am hungry cook for me" || speech == "I am so hungry cook for me" || speech == "I am very hungry cook for me" || speech == "Please cook for me" || speech == "I am hungry lexi cook for me" || speech == "Lexi i am hungry cook for me" || speech == "Lexi cook for me please")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I can not do this");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(375, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("are you kidding");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(241, 159);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("I would like that, but I can't");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/heart.gif");
                    Anim.Location = new Point(213, 131);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("i have no talent for this");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(230, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //bana kahve yap
            if(speech == "Make me a coffee" || speech == "Make me a coffee lexi" || speech == "Make me a coffee baby" || speech == "Lexi make me a coffee" || speech == "Baby make me a coffee" || speech == "Okay make me a coffee" || speech == "Okay baby make me a coffee" || speech == "Okay lexi make me a coffee" || speech == "okay make me a coffee lexi" || speech == "Okay make me a coffee baby")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I can not do this");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(202, 121);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("I would like to but unfortunately");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(230, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("are you kidding");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/software1.gif");
                    Anim.Location = new Point(204, 166);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("i am not capable of this");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/coffeemachine.gif");
                    Anim.Location = new Point(411, 160);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //Kendini tanıt
            if(speech == "Can you introduce yourself" || speech == "Can you introduce yourself lexi" || speech == "Can you introduce yourself baby" || speech == "Lexi can you introduce yourself" || speech == "Baby can you introduce yourself" || speech == "Okay can you introduce yourself" || speech == "Please can you introduce yourself" || speech == "Introduce yourself" || speech == "Lexi introduce yourself" || speech == "Baby introduce yourself")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I am lexi, i am your assistand, and i am here for you, made me devonesoft, I was born on 1/20/2022 , welcome to lexi assistan");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(409, 16);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("Okay sir, I am lexi and i am your assistand, and i am here for you, made me devonesoft, I was born on 1/20/2022 , welcome to lexi assistan baby");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/smile.gif");
                    Anim.Location = new Point(221, 49);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("Okay baby, I am lexi, i am your assistand, and i am here for you, made me devonesoft, I was born on 1/20/2022 , welcome to lexi assistan, i love you");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/software2.gif");
                    Anim.Location = new Point(221, 113);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("I am lexi sir, i am your assistand, and i am here for you baby, made me devonesoft education, I was born on 1/20/2022 , welcome to lexi assistan sir");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(241, 169);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //lexi browser ac
            if (speech=="Lexi browser start")
            {
                lexi.SpeakAsync("Okay");
                Thread lxweb = new Thread(new ThreadStart(lexiBrowser));
                lxweb.Start();
                Anim.Image = Image.FromFile("animations/openbrowser.gif");
                Anim.Location = new Point(420, 54);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();

            }
            //
            //Open lexi klavye
            if(speech=="Open keyboard" || speech == "Open the keyboard" || speech == "Open the lexi keyboard" || speech == "Open the screen keyboard")
            {
                lexi.SpeakAsync("I am opening keyboard");
                kv.Show();
                bilgiLabel.Text = "Lexi keyboard: on\nif you want say me! close keyboard";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(213, 21);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if(speech=="Close keyboard" || speech == "Close the keyboard" || speech == "Close the lexi keyboard" || speech == "Close the screen keyboard")
            {
                lexi.SpeakAsync("I am closing keyboard");
                kv.Hide();
                bilgiLabel.Text = "Lexi keyboard: off\nif you want say me! open keyboard";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/software1.gif");
                Anim.Location = new Point(409, 18);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //LEXİ MAKE MELODIY (tr-lexi melodi yap)
            if (speech == "Make a melody" || speech == "Melody make" || speech == "Make melody" || speech == "Make music" || speech == "Make a music" || speech == "Lexi can you make a melody" ||
                speech == "Lexi make a melody" || speech == "I want a melody" || speech == "Please make a melody")
            {
                //form kasmaması icim bu islemi arkaplanda yap
                Thread Melody = new Thread(new ThreadStart(MelodyMaker));
                Melody.Start();
                Anim.Image = Image.FromFile("animations/music.gif");
                Anim.Location = new Point(225, 119);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //OPEN THE DEFAULT BROWSER----tr-varsayilan tarayiciyi ac
            if (speech == "Google search" || speech == "Browser search" || speech == "Open the browser" || speech == "Open browser" || speech == "Open google" || speech == "Browser open" || speech == "Google open" || speech == "Can you open the browser" || speech == "Search browser" || speech == "Lexi open the browser")
            {
                lexi.SpeakAsync("I am opening browser");
                System.Diagnostics.Process.Start("https://www.google.com/");
                Anim.Image = Image.FromFile("animations/search.gif");
                Anim.Location = new Point(369, 139);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if(speech=="Focus google search box" || speech == "Search box click" || speech == "Google search box click" || speech == "Search box focus"||speech=="Focus search box"||speech=="Focus search")
            {
                SendKeys.Send("+{TAB}");
                lexi.SpeakAsync("okay");
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(394, 160);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //Youtube ac
            if (speech == "Open the youtube" || speech == "Youtube open" || speech == "Open browser youtube" || speech == "Open youtube" || speech == "Browser youtube" || speech == "Can you open the youtube" || speech == "Search youtube"||speech=="Show the youtube" || speech == "Lexi open the youtube" || speech == "Baby open the youtube")
            {
                lexi.SpeakAsync("I am opening youtube");
                System.Diagnostics.Process.Start("https://www.youtube.com/");
                Anim.Image = Image.FromFile("animations/search.gif");
                Anim.Location = new Point(240, 139);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //
            //spotify ac
            if(speech == "Open the spotify" || speech == "Spotify open" || speech == "Open browser spotify" || speech == "Open spotify" || speech == "Browser spotify" || speech == "Can you open the spotify" || speech == "Search spotify" || speech == "Show the spotify" || speech == "Lexi open the spotify" || speech == "Baby open the spotify")
            {
                lexi.SpeakAsync("I am opening spotify");
                System.Diagnostics.Process.Start("https://www.spotify.com");
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                Anim.Image = Image.FromFile("animations/openbrowser.gif");
                Anim.Location = new Point(203, 106);
                Anim.Enabled = true;
                Anim.Visible = true;
            }
            //
            //vimeo ac
            if(speech == "Open the vimeo" || speech == "Vimeo open" || speech == "Open browser vimeo" || speech == "Open vimeo" || speech == "Browser vimeo" || speech == "Can you open the vimeo" || speech == "Search vimeo" || speech == "Show the vimeo" || speech == "Lexi open the vimeo" || speech == "Baby open the vimeo")
            {
                lexi.SpeakAsync("I am opening vimeo");
                System.Diagnostics.Process.Start("https://www.vimeo.com");
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                Anim.Image = Image.FromFile("animations/openbrowser.gif");
                Anim.Location = new Point(411, 106);
                Anim.Enabled = true;
                Anim.Visible = true;
            }
            //
            //github ac
            if (speech == "Open the github" || speech == "Github open" || speech == "Open browser github" || speech == "Open github" || speech == "Browser github" || speech == "Can you open the github" || speech == "Search github" || speech == "Show the github" || speech == "Lexi open the github" || speech == "Baby open the github")
            {
                lexi.SpeakAsync("I am opening github");
                System.Diagnostics.Process.Start("https://github.com/");
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(391, 158);
                Anim.Enabled = true;
                Anim.Visible = true;
            }
            //
            //twitter ac
            if(speech == "Open the twitter" || speech == "Twitter open" || speech == "Open browser twitter" || speech == "Open twitter" || speech == "Browser twitter" || speech == "Can you open the twitter" || speech == "Search twitter" || speech == "Show the twitter" || speech == "Lexi open the twitter" || speech == "Baby open the twitter")
            {
                lexi.SpeakAsync("I am opening twitter");
                System.Diagnostics.Process.Start("https://www.twitter.com/");
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(413, 59);
                Anim.Enabled = true;
                Anim.Visible = true;
            }
            //
            //bana su ver
            if(speech == "Give me water" || speech == "Give me water lexi" || speech == "Give me water baby" || speech == "Lexi give me water" || speech == "Baby give me water" || speech == "Okay give me water" || speech == "Okay lexi give me water" || speech == "Okay baby give me water" || speech == "Okay give me water baby" || speech == "Okay give me water lexi")
            {
                ranNum = rnd.Next(1, 6);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("I'd love to do this, but I can't");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/water.gif");
                    Anim.Location = new Point(216, 151);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("sorry i don't have hands");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/background3.gif");
                    Anim.Location = new Point(215, 69);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("is this a joke");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/background4.gif");
                    Anim.Location = new Point(411, 91);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("Unfortunately, I don't have such a talent.");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/shooting.gif");
                    Anim.Location = new Point(390, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //
            //bana para ver
            if(speech == "Give me money" || speech == "Give me money lexi" || speech == "Give me money baby" || speech == "Lexi give me money" || speech == "Baby give me money" || speech == "Okay give me money" || speech == "Okay lexi give me money" || speech == "Okay baby give me money" || speech == "Okay give me money lexi" || speech == "Okay give me money baby")
            {
                ranNum = rnd.Next(1, 5);
                if(ranNum == 1)
                {
                    lexi.SpeakAsync("You will have to work for it");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/set.gif");
                    Anim.Location = new Point(406, 95);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 2)
                {
                    lexi.SpeakAsync("sorry i can't make money yet");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/background2.gif");
                    Anim.Location = new Point(409, 16);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 3)
                {
                    lexi.SpeakAsync("I don't have money baby, sorry.");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/mic.gif");
                    Anim.Location = new Point(390, 137);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
                if(ranNum == 4)
                {
                    lexi.SpeakAsync("How about looking at new business ideas, you can earn money");
                    AnimationControl.Enabled = true;
                    AnimationControl.Start();
                    Anim.Image = Image.FromFile("animations/glasses.gif");
                    Anim.Location = new Point(209, 27);
                    Anim.Enabled = true;
                    Anim.Visible = true;
                }
            }
            //------------------------------------------//
            //RADİO CHANNELS LOAD
            if (speech == "Show radio" || speech == "Open radio" || speech == "Radio open" || speech == "Open the radio" || speech == "Show the radio"||speech=="Can you open the radio" || speech == "Lexi open the radio")
            {
                lexi.SpeakAsync("I am opening radio");
                islemler.Controls.Clear();
                islemler.Controls.Add(channels);
                channels.Show();
                radioclick = true;
                radioShow = true;
                bilgiLabel.Text = "Radio: on\nif you want say me! close radio or hide radio and \n some stuff like that\nsay me! Radio one play";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/mic.gif");
                Anim.Location = new Point(416, 128);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if (speech == "Hide radio" || speech == "Close radio" || speech == "Radio close" || speech == "Hide the radio" || speech == "Close the radio"||speech=="Can you close the radio" || speech == "Lexi close the radio")
            {
                lexi.SpeakAsync("I am closing radio");
                islemler.Controls.Clear();
                radioclick = false;
                radioShow = false;
                Anim.Image = Image.FromFile("animations/mic.gif");
                Anim.Location = new Point(418, 81);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //radio eger acıksa ve calınması isteniyorsa cal
            if (radioShow == true)
            {
                if (speech == "Radio one play" || speech == "Channel one play" || speech == "One Channel play")
                {
                    channels.label1Play();
                }
                if (speech == "Radio two play" || speech == "Channel two play" || speech == "Two Channel play")
                {
                    channels.label2play();
                }
                if (speech == "Radio three play" || speech == "Channel three play" || speech == "Three Channel play")
                {
                    channels.label3play();
                }
                if (speech == "Radio four play" || speech == "Channel four play" || speech == "four Channel play")
                {
                    channels.label4play();
                }
                if (speech == "Radio five play" || speech == "Channel five play" || speech == "Five Channel play")
                {
                    channels.label5play();
                }
                if (speech == "Radio stop" || speech == "Stop radio")
                {
                    channels.RadioStop();
                }
            }
            //------------------------------------------//
            //KEYBOARD CONTROLS
            if (speech == "Keyboard a" || speech == "Key a")
            {
                SendKeys.Send("a");

            }
            if (speech == "Keyboard q" || speech == "Key q")
            {
                SendKeys.Send("q");

            }
            if (speech == "Keyboard z" || speech == "Key z")
            {
                SendKeys.Send("z");

            }
            if (speech == "Keyboard w" || speech == "Key w")
            {
                SendKeys.Send("w");

            }
            if (speech == "Keyboard s" || speech == "Key s")
            {
                SendKeys.Send("s");

            }
            if (speech == "Keyboard x" || speech == "Key x")
            {
                SendKeys.Send("x");

            }
            if (speech == "Keyboard e" || speech == "Key e")
            {
                SendKeys.Send("e");

            }
            if (speech == "Keyboard d" || speech == "Key d")
            {
                SendKeys.Send("d");

            }
            if (speech == "Keyboard c" || speech == "Key c")
            {
                SendKeys.Send("c");

            }
            if (speech == "Keyboard r" || speech == "Key r")
            {
                SendKeys.Send("r");

            }
            if (speech == "Keyboard f" || speech == "Key f")
            {
                SendKeys.Send("f");

            }
            if (speech == "Keyboard v" || speech == "Key v")
            {
                SendKeys.Send("v");

            }
            if (speech == "Keyboard t" || speech == "Key t")
            {
                SendKeys.Send("t");

            }
            if (speech == "Keyboard g" || speech == "Key g")
            {
                SendKeys.Send("g");

            }
            if (speech == "Keyboard b" || speech == "Key b")
            {
                SendKeys.Send("b");

            }
            if (speech == "Keyboard y" || speech == "Key y")
            {
                SendKeys.Send("y");

            }
            if (speech == "Keyboard h" || speech == "Key h")
            {
                SendKeys.Send("h");

            }
            if (speech == "Keyboard n" || speech == "Key n")
            {
                SendKeys.Send("n");

            }
            if (speech == "Keyboard u" || speech == "Key u")
            {
                SendKeys.Send("u");

            }
            if (speech == "Keyboard j" || speech == "Key j")
            {
                SendKeys.Send("j");

            }
            if (speech == "Keyboard m" || speech == "Key m")
            {
                SendKeys.Send("m");

            }
            if (speech == "Keyboard k" || speech == "Key k")
            {
                SendKeys.Send("k");

            }
            if (speech == "Keyboard o" || speech == "Key o")
            {
                SendKeys.Send("o");

            }
            if (speech == "Keyboard l" || speech == "Key l")
            {
                SendKeys.Send("l");

            }
            if (speech == "Keyboard p" || speech == "Key p")
            {
                SendKeys.Send("p");

            }
            if (speech == "Keyboard i" || speech == "Key i")
            {
                SendKeys.Send("i");

            }
            if (speech == "Keyboard backspace" || speech == "Key backspace")
            {
                SendKeys.SendWait("{BACKSPACE}");

            }
            if (speech == "Keyboard f one" || speech == "Key f one")
            {
                SendKeys.SendWait("{F1}");

            }
            if (speech == "Keyboard f two" || speech == "Key f two")
            {
                SendKeys.SendWait("{F2}");

            }
            if (speech == "Keyboard f three" || speech == "Key f three")
            {
                SendKeys.SendWait("{F3}");

            }
            if (speech == "Keyboard f four" || speech == "Key f four")
            {
                SendKeys.SendWait("{F4}");

            }
            if (speech == "Keyboard f five" || speech == "Key f five")
            {
                SendKeys.SendWait("{F5}");

            }
            if (speech == "Keyboard f six" || speech == "Key f six")
            {
                SendKeys.SendWait("{F6}");

            }
            if (speech == "Keyboard f seven" || speech == "Key f seven")
            {
                SendKeys.SendWait("{F7}");

            }
            if (speech == "Keyboard f eight" || speech == "Key f eight")
            {
                SendKeys.SendWait("{F8}");

            }
            if (speech == "Keyboard f nine" || speech == "Key f nine")
            {
                SendKeys.SendWait("{F9}");

            }
            if (speech == "Keyboard f ten" || speech == "Key f ten")
            {
                SendKeys.SendWait("{F10}");

            }
            if (speech == "Keyboard f eleven" || speech == "Key f eleven")
            {
                SendKeys.SendWait("{F11}");

            }
            if (speech == "Keyboard f twelve" || speech == "Key f twelve")
            {
                SendKeys.SendWait("{F12}");

            }
            if(speech=="Keyboard space"||speech=="Key space")
            {
                SendKeys.Send(" ");
            }
            if(speech=="Keyboard one" || speech == "Key one")
            {
                SendKeys.Send("1");
            }
            if (speech == "Keyboard two" || speech == "Key two")
            {
                SendKeys.Send("2");
            }
            if (speech == "Keyboard three" || speech == "Key three")
            {
                SendKeys.Send("3");
            }
            if (speech == "Keyboard four" || speech == "Key four")
            {
                SendKeys.Send("4");
            }
            if (speech == "Keyboard five" || speech == "Key five")
            {
                SendKeys.Send("5");
            }
            if (speech == "Keyboard six" || speech == "Key six")
            {
                SendKeys.Send("6");
            }
            if (speech == "Keyboard seven" || speech == "Key seven")
            {
                SendKeys.Send("7");
            }
            if (speech == "Keyboard eight" || speech == "Key eight")
            {
                SendKeys.Send("8");
            }
            if (speech == "Keyboard nine" || speech == "Key nine")
            {
                SendKeys.Send("9");
            }
            if (speech == "Keyboard ten" || speech == "Key ten")
            {
                SendKeys.Send("10");
            }
            if(speech == "Keyboard capslock" || speech == "Key capslock")
            {
                if(capslock == false)
                {
                    ToggleCapsLock(true);
                    capslock = true;
                }
                else
                {
                    ToggleCapsLock(false);
                    capslock = false;
                }
            }
            if(speech == "Keyboard tab" || speech == "Key tab")
            {
                SendKeys.Send("{TAB}");
            }
            if (speech == "Keyboard slash" || speech == "Key slash")
            {
                SendKeys.Send("/");
            }
            if(speech == "Keyboard star" || speech == "Key star")
            {
                SendKeys.Send("*");
            }
            if(speech == "Keyboard hyphen" || speech == "Key hyphen")
            {
                SendKeys.Send("-");
            }
            if(speech == "Keyboard point" || speech == "Key point" || speech == "Keyboard dot" || speech == "Key dot")
            {
                SendKeys.Send(".");
            }
            if(speech == "Keyboard down" || speech == "Key down")
            {
                SendKeys.Send("{DOWN}");
            }
            if(speech == "Keyboard up" || speech == "Key up")
            {
                SendKeys.Send("{UP}");
            }
            if(speech == "Keyboard left" || speech == "Key left")
            {
                SendKeys.Send("{LEFT}");
            }
            if(speech == "Keyboard right" || speech == "Key right")
            {
                SendKeys.Send("{RIGHT}");
            }
            if(speech == "Keyboard controll v" || speech == "Key controll v" || speech == "Keyboard ctrl v"|| speech == "Key ctrl v" || speech == "Keyboard controll")
            {
                lexi.SpeakAsync("if you want, i can open the lexi keyboard");
                lexi.SpeakAsync("Say me, open keyboard");
                bilgiLabel.Text = "İf you want\nSay me , Open keyboard";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(219, 166);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if(speech == "Keyboard page down" || speech == "Key page down")
            {
                SendKeys.Send("{PGDN}");
            }
            if(speech == "Keyboard page up" || speech == "Key page up")
            {
                SendKeys.Send("{PGUP}");
            }
            if(speech == "Keyboard delete" || speech == "Key delete")
            {
                SendKeys.Send("{DELETE}");
            }
            if(speech == "Keyboard home" || speech == "Key home")
            {
                SendKeys.Send("{HOME}");
            }
            if(speech == "Keyboard insert" || speech == "Key insert")
            {
                SendKeys.Send("{INSERT}");
            }
            //------------------------//
            //MOUSE RİGHT AND LEFT CLİCK CONTROLS
            if (speech == "Mouse left click" || speech == "Mouse click left" || speech == "M left click")
            {
                lexi.SpeakAsync("Yes sir");
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                MouseLeftClick();
            }
            if (speech == "Mouse right click" || speech == "Mouse click right" || speech == "M right click")
            {
                lexi.SpeakAsync("Yes sir");
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                MouseRightClick();
            }
            if (speech == "M double click" || speech == "Mouse double click")
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                MouseDoubleClick();
            }
            //-----------------------//
            //MOUSE POSİTİON NORMALLY CONTROLS
            if (speech == "M left")
            {

                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX -= 20;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point (0,0) explicitly with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;

            }
            if (speech == "M right")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX += 20;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M bottom")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY += 20;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M top")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY -= 20;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            //----QUİCKLY MOUSE POSİTİON CONTROL
            if (speech == "M quickly left")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX -= 50;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point (0,0) explicitly with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;

            }
            if (speech == "M quickly right")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX += 50;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M quickly bottom")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY += 50;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M quickly top")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY -= 50;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            //---------------------------------//
            //MOUSE POSİTİON SLOWLY CONTROL
            if (speech == "M slowly left")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX -= 10;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point (0,0) explicitly with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;

            }
            if (speech == "M slowly right")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionX += 10;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M slowly bottom")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY += 10;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            if (speech == "M slowly top")
            {
                PositionX = Cursor.Position.X;
                PositionY = Cursor.Position.Y;
                PositionY -= 10;
                // get mouse position
                System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
                // create X,Y point with System.Drawing 
                System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
                // set mouse position
                Cursor.Position = leftTop;
            }
            //mouse basılı tutma modu ac kapa islemleri
            if(speech=="Drag mode on"||speech=="Drag mode true"||speech == "I want to drag on the screen"|| speech == "Hold mouse down"||speech=="Drag mouse" || speech == "Open the drag mode" || speech == "Lexi open the drag mode")
            {
                lexi.SpeakAsync("I am opening drag mode");
                MousedragmodeOn();
                bilgiLabel.Text = "Drag mode: on";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(214, 122);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if(speech=="Drag mode off"||speech=="Drag mode false"||speech == "I don't want to drag on the screen" || speech == "Hold mouse up" || speech == "Stop drag mode" || speech == "Stop drag" || speech == "Close the drag mode" || speech == "Lexi close the drag mode")
            {
                lexi.SpeakAsync("I am closeing drag mode");
                MousedragmodeOff();
                bilgiLabel.Text = "Drag mode: off";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(401, 156);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //mouse kontrol modu
            if(speech=="Mouse controll mode true"||speech=="Open mouse controll mode" || speech == "Open the mouse controll mode" || speech == "Lexi open the mouse controll mode")
            {
                lexi.SpeakAsync("I am opening mouse controll mode");
                MouseControllMode = true;
                bilgiLabel.Text = "Mouse controll: on";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/target.gif");
                Anim.Location = new Point(419, 65);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            if(speech=="Mouse controll mode false" || speech == "Close mouse controll mode" || speech == "Close the mouse controll mode" || speech == "Lexi close the mouse controll mode")
            {
                lexi.SpeakAsync("I am closing mouse controll mode");
                MouseControllMode = false;
                bilgiLabel.Text = "Mouse controll: off";
                bilgitmr.Enabled = true;
                bilgitmr.Start();
                Anim.Image = Image.FromFile("animations/software2.gif");
                Anim.Location = new Point(405, 139);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            
            if(speech == "Mouse to left")
            {
                if (MouseControllMode == true)
                {
                    mLeft.Enabled = true;
                    mLeft.Start();
                    mRight.Enabled = false;
                    mRight.Stop();
                    mTop.Enabled = false;
                    mTop.Stop();
                    mBottom.Enabled = false;
                    mBottom.Stop();
                }
            }
            if(speech=="Mouse to right")
            {
                if (MouseControllMode == true)
                {
                    mRight.Enabled = true;
                    mRight.Start();
                    mLeft.Enabled = false;
                    mLeft.Stop();
                    mTop.Enabled = false;
                    mTop.Stop();
                    mBottom.Enabled = false;
                    mBottom.Stop();
                }
            }
            if(speech=="Mouse to top")
            {
                if (MouseControllMode == true)
                {
                    mTop.Enabled = true;
                    mTop.Start();
                    mLeft.Enabled = false;
                    mLeft.Stop();
                    mRight.Enabled = false;
                    mRight.Stop();
                    mBottom.Enabled = false;
                    mBottom.Stop();
                }
            }
            if(speech=="Mouse to bottom")
            {
                if (MouseControllMode == true)
                {
                    mBottom.Enabled = true;
                    mBottom.Start();
                    mLeft.Enabled = false;
                    mLeft.Stop();
                    mRight.Enabled = false;
                    mRight.Stop();
                    mTop.Enabled = false;
                    mTop.Stop();
                }
            }
            if(speech=="Mouse stop"||speech=="Stop mouse")
            {
                mLeft.Enabled = false;
                mLeft.Stop();
                mRight.Enabled = false;
                mRight.Stop();
                mTop.Enabled = false;
                mTop.Stop();
                mBottom.Enabled = false;
                mBottom.Stop();
                Anim.Image = Image.FromFile("animations/software1.gif");
                Anim.Location = new Point(236, 139);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
            //---------------------------------//
            if (speech == "Stop talking" || speech == "Lexi stop talking")
            {
                lexi.SpeakAsyncCancelAll();
                ranNum = rnd.Next(1, 6);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("Yes sir");
                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("I am sorry, i will be quiet");

                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("Okay sir, whatever you want");

                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("sorry sir,I think I ironed your head");

                }
                if (ranNum == 5)
                {
                    lexi.SpeakAsync("I will shut up immediately");

                }
                Anim.Image = Image.FromFile("animations/mic.gif");
                Anim.Location = new Point(208, 14);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }



            if (speech == "Stop listening" || speech == "Lexi stop listening")
            {
                circularProgressBar1.Value = 0;
                ranNum = rnd.Next(1, 5);
                _recognizer.RecognizeAsyncCancel();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                if (ranNum == 1)
                {
                    lexi.SpeakAsync("if you need me, just ask");

                }
                if (ranNum == 2)
                {
                    lexi.SpeakAsync("well, i'm with you whenever you want");

                }
                if (ranNum == 3)
                {
                    lexi.SpeakAsync("well sir, i am always ready to help");

                }
                if (ranNum == 4)
                {
                    lexi.SpeakAsync("ok sir, i'm leaving now, but i'm always here");

                }
                Anim.Image = Image.FromFile("animations/smile.gif");
                Anim.Location = new Point(240, 139);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }

        }
        private void _recognizer_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            RecTimeOut = 0;
            circularProgressBar1.Value = 100;
        }

        private void startlistening_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int ranNum2;
            string speech = e.Result.Text;
            if (speech == "Wake up" || speech == "Talk me")
            {
                ranNum2 = rnd.Next(1, 4);
                startlistening.RecognizeAsyncCancel();
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);

                if (ranNum2 == 1)
                {
                    lexi.SpeakAsync("Yes, I am hear");

                }
                if (ranNum2 == 2)
                {
                    lexi.SpeakAsync("I'm here baby");

                }
                if (ranNum2 == 3)
                {
                    lexi.SpeakAsync("I'm with you");

                }
                Anim.Image = Image.FromFile("animations/background.gif");
                Anim.Location = new Point(308, 240);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
            }
        }

        private void TmrSpeaking_Tick(object sender, EventArgs e)
        {
            if (RecTimeOut == 10)
            {
                _recognizer.RecognizeAsyncCancel();
            }
            else if (RecTimeOut == 11)
            {
                TmrSpeaking.Stop();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                RecTimeOut = 0;
            }
        }
        //LEXİ APPLİCATİON EXİT, BEFORE WAİT 5 SECONDS
        private void Tmrexit_Tick(object sender, EventArgs e)
        {
            Application.Exit();

        }
        //MOUSE RİGHT CLİCK
        public void MouseRightClick()
        {

            int X = Cursor.Position.X;

            int Y = Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);
        }
        //MOUSE LEFT CLİCK
        public void MouseLeftClick()
        {


            int X = Cursor.Position.X;

            int Y = Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);


        }
        //MOUSE DOUBLE CLİCK
        public void MouseDoubleClick()
        {
            int X = Cursor.Position.X;

            int Y = Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);
        }

        private void AnimationControl_Tick(object sender, EventArgs e)
        {
           
            Anim.Enabled = false;
            Anim.Visible = false;
            AnimationControl.Stop();
            AnimationControl.Enabled = false;
            panel2.Controls.Clear();
            panel2.Controls.Add(bilgiLabel);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            kv.Close();
            lexi.SpeakAsync("Good bye sir");
            bilgiLabel.Text = "Good bye sir";
            Tmrexit.Enabled = true;
            Tmrexit.Start();
            //CIKIS
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //GİZLİ SİMGE KISMINA INDIR
            Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(3000);
        }
        private void bilgitmr_Tick(object sender, EventArgs e)
        {
            bilgiLabel.Text = "";
            panel2.Controls.Clear();
            panel2.Controls.Add(bilgiLabel);
        }
        bool radioclick = false;
        radiochannels channels = new radiochannels() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioclick == false)
            {
                islemler.Controls.Clear();
                islemler.Controls.Add(channels);
                channels.Show();
                radioclick = true;
            }
            else
            {
                islemler.Controls.Clear();
                radioclick = false;

            }

        }
        bool Lexichat_control = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Lexichat_control == false)
            {
                LexiChat.Visible = true;
                Lexichat_control = true;
            }
            else
            {
                LexiChat.Visible = false;
                Lexichat_control = false;
            }
            Lexichat cht = new Lexichat();
            cht.listen();
        }

        //ALT MENU KISMINDAKİ BUTONLAR İCİN TİTLE KISMI
        private void settings_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
        }

        private void settings_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void search_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
        }

        private void search_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;

        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            label7.Visible = true;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;

        }
        //FORM SURUKLEME
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        //LEXİ UZERİNDE FORMU SURUKLEMEK ICIN ISLEMLER
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //BILGI PANELI ICIN FORM EKRANDA SURUKLEME
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //ISLEMLER PANELI ICIN FORM EKRANDA SURUKLEME
        private void islemler_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void islemler_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void islemler_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //CIRCULAR PROGRESSBAR ICIN FORM EKRANDA SURUKLEME
        private void circularProgressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void circularProgressBar1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void circularProgressBar1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //MENU - ARACLAR PANELI ICIN FORM EKRANDA SURUKLEME
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //LEXI YAZAN LABEL ICIN FORM EKRANDA SURUKLEME
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        //SOL TARAF BILGI LABEL ICIN FORM EKRANDA SURUKLEME
        private void bilgiLabel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void bilgiLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void bilgiLabel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void LexiChat_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
              
            }
         
        }
        //melodi yapımı arka plan islemleri
        private void MelodyMaker()
        {
            try
            {
                lexi.SpeakAsync("well, let me make you some music");
                Thread.Sleep(2000);
                SoundPlayer simpleSound = new SoundPlayer(@"sound\melody1.wav");
                SoundPlayer simpleSound2 = new SoundPlayer(@"sound\melody2.wav");
                SoundPlayer simpleSound3 = new SoundPlayer(@"sound\melody3.wav");
                SoundPlayer simpleSound4 = new SoundPlayer(@"sound\melody5.wav");
                int ranNum = rnd.Next(1, 6);
                //random melody
                if (ranNum == 1)
                {

                    simpleSound.Play();

                }
                if (ranNum == 2)
                {
                    simpleSound2.Play();
                }
                if (ranNum == 3)
                {
                    simpleSound3.Play();
                }

                if (ranNum == 4)
                {
                    simpleSound4.Play();
                }
            }
            catch
            {
                lexi.SpeakAsync("sorry, i don't remember now");
            }
        }
        //hava durumu arkaplan islemleri
        private void WeatherStatus()
        {
            try
            {
                lexi.SpeakAsync("Please wait");
                //havadurumu kodları
                //http://api.openweathermap.org/data/2.5/weather?q=izmir&appid=
                //"e7d2c6c38b9ae7616ec4a3579c9313cf&lang=tr"
                string api = "e7d2c6c38b9ae7616ec4a3579c9313cf";
                string url = "https://api.openweathermap.org/data/2.5/weather?q=izmir&mode=xml&lang=en&units=metric&appid=" + api;
                XDocument weather = XDocument.Load(url);
                var temp = weather.Descendants("temperature").ElementAt(0).Attribute("value").Value;
                var weatherstatu = weather.Descendants("weather").ElementAt(0).Attribute("value").Value;
                var feels = weather.Descendants("feels_like").ElementAt(0).Attribute("value").Value;
                var hum = weather.Descendants("humidity").ElementAt(0).Attribute("value").Value;
                var clouds = weather.Descendants("clouds").ElementAt(0).Attribute("value").Value;
                var wind_speed = weather.Descendants("speed").ElementAt(0).Attribute("value").Value;
                var precipitation = weather.Descendants("precipitation").ElementAt(0).Attribute("mode").Value;
                lexi.SpeakAsync("My weather report for izmir");
                lexi.SpeakAsync("Temp, " + temp);
                lexi.SpeakAsync("Weather, " + weatherstatu);
                lexi.SpeakAsync("feels like, " + feels);
                lexi.SpeakAsync("Humidity, " + hum);
                lexi.SpeakAsync("Clouds, " + clouds);
                lexi.SpeakAsync("wind speed, " + wind_speed);
                lexi.SpeakAsync("precipitation, " + precipitation);
                lexi.SpeakAsync("That is all baby");
                bilgiLabel.Text = "İzmir Weather\nTemp: " + temp + "\n" + "Weather: " + weatherstatu + "\n" + "Feels like: " + feels + "\n" +
                "Humidity: " + hum + "\n" + "Clouds: " + clouds + "\n" + "Wind speed: " + wind_speed + "\n" + "Precipitation: " + precipitation;
            }
            catch
            {
                
                lexi.SpeakAsync("I think there is a problem, I cannot access the information");
                lexi.SpeakAsync("I'll look again later");
                Anim.Image = Image.FromFile("animations/set.gif");
                Anim.Location = new Point(214, 70);
                Anim.Enabled = true;
                Anim.Visible = true;
                AnimationControl.Enabled = true;
                AnimationControl.Start();
                
            }
        }
        //
        //Kitap tavsiyesi icin karar mekanizması
     
        private void BookAdviceSay()
        {
            //kitap listesinden bir kitap al ve tavsiye et
            using (StreamReader reader = new StreamReader("BookAdvice.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Booklist.Add(line);
                }

                Thread.Sleep(1000);
                lexi.SpeakAsync(Booklist.ElementAt(BookLine));
                bilgiLabel.Text = Booklist.ElementAt(BookLine);
                lexi.SpeakAsync("I recommend this book");
                if (BookLine < Booklist.Count)
                {
                    BookLine++;

                }
                else
                {
                    BookLine = 0;
                }
            }
            //1 saniye bekle ve kitabın fotografınıda goster
            Thread.Sleep(1000);
            using (StreamReader readerPic = new StreamReader("BookAdvicePictures.txt"))
            {
                string linePic;
                while ((linePic = readerPic.ReadLine()) != null)
                {
                    BookListPictures.Add(linePic);
                }
                Thread.Sleep(1000);
                BookPic.Image = Image.FromFile(BookListPictures.ElementAt(BookLinePic));
                if (BookLinePic < BookListPictures.Count-1)
                {
                    BookLinePic++;

                }
                else
                {
                    BookLinePic = 0;
                }
            }
           
        }

        private void search_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:/csharp projelerim/LexiBrowser/LexiBrowser/bin/Debug/LexiBrowser.exe");
        }
        private void lexiBrowser()
        {
            ProcessStartInfo info = new ProcessStartInfo("C:\\csharp projelerim\\lexi\\lexi\\bin\\Debug\\LexiBrowser\\LexiBrowser\\bin\\Debug\\LexiBrowser.exe");
            Process.Start(info);
        }
        //herhangi bir objeyi ekranda sürükleme islevi
        public void MousedragmodeOn()
        {


            int X = Cursor.Position.X;

            int Y = Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_LEFTDOWN, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);


        }
        public void MousedragmodeOff()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTUP, Convert.ToUInt32(X), Convert.ToUInt32(Y), 0, 0);
        }
        //mouse hızlı kontroller
        private void mLeft_Tick(object sender, EventArgs e)
        {
            PositionX = Cursor.Position.X;
            PositionY = Cursor.Position.Y;
            PositionX -= 10;
            // get mouse position
            System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
            // create X,Y point (0,0) explicitly with System.Drawing 
            System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
            // set mouse position
            Cursor.Position = leftTop;
        }

        private void mRight_Tick(object sender, EventArgs e)
        {
            PositionX = Cursor.Position.X;
            PositionY = Cursor.Position.Y;
            PositionX += 10;
            // get mouse position
            System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
            // create X,Y point with System.Drawing 
            System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
            // set mouse position
            Cursor.Position = leftTop;
        }

        private void mTop_Tick(object sender, EventArgs e)
        {
            PositionX = Cursor.Position.X;
            PositionY = Cursor.Position.Y;
            PositionY -= 10;
            // get mouse position
            System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
            // create X,Y point with System.Drawing 
            System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
            // set mouse position
            Cursor.Position = leftTop;
        }

        private void mBottom_Tick(object sender, EventArgs e)
        {
            PositionX = Cursor.Position.X;
            PositionY = Cursor.Position.Y;
            PositionY += 10;
            // get mouse position
            System.Drawing.Point screenPos = System.Windows.Forms.Cursor.Position;
            // create X,Y point with System.Drawing 
            System.Drawing.Point leftTop = new System.Drawing.Point(PositionX, PositionY);
            // set mouse position
            Cursor.Position = leftTop;
        }
        //capslock acma/kapama icin islem
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        static void ToggleCapsLock(bool onOrOff)
        {
            if (IsKeyLocked(Keys.CapsLock) == onOrOff) return;
            keybd_event(0x14, 0x45, 0x1, (UIntPtr)0);
            keybd_event(0x14, 0x45, 0x1 | 0x2, (UIntPtr)0);

        }

        private void ShutdownPc_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Kapandı");
        }
        int shutdownSeconds = 0;
        private void ShutdownSeconds_Tick(object sender, EventArgs e)
        {
            if (shutdownSeconds < 57)
            {
                shutdownSeconds++;
                bilgiLabel.Text = "Shutdown computer seconds: " + shutdownSeconds.ToString();
            }
            else
            {
                Console.Beep(800, 200);
                ShutdownSeconds.Enabled = false;
                ShutdownSeconds.Stop();
            }
          
        }
    }
}
