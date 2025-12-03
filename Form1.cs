using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace ProiectSlotMachine
{
    public partial class Form1 : Form
    {
        private int[] slotTextureIds = new int[4];
        private string[] textureFiles = { "cherry.png", "lemon.png", "bar.png", "seven.png" };
        private int[] currentSlotImage = new int[3];

        private Timer slotTimer;
        private Random random = new Random();
        private int remainingCycles;
        private bool isSpinning = false;
        private bool isLoaded = false;
        private const int SPIN_SPEED_MS = 50;

        public Form1()
        {
            Text = "Slot Machine";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            // Asigură-te că InitializeComponent() este setat corect (vezi secțiunea de jos)

            GlControl1.Load += GlControl1_Load;
            GlControl1.Paint += GlControl1_Paint;
            GlControl1.Resize += GlControl1_Resize;
            btnTrageSlot.Click += btnTrageSlot_Click; // Re-atașează click event
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.3f, 1f); // Fundal albastru închis
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            // Setări Blending (Transparență)
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            // ** ÎMBUNĂTĂȚIRE: ACTIVARE ILUMINARE ȘI MATERIALE **
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);

            float[] light_position = { 0.0f, 10.0f, 30.0f, 1.0f };
            float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);


            slotTimer = new Timer();
            slotTimer.Interval = SPIN_SPEED_MS;
            slotTimer.Tick += SlotTimer_Tick;

            for (int i = 0; i < 3; i++)
                currentSlotImage[i] = random.Next(0, 4);

            LoadSlotTextures();
            isLoaded = true;
        }

        private void LoadSlotTextures()
        {
            GL.GenTextures(slotTextureIds.Length, slotTextureIds);
            for (int i = 0; i < slotTextureIds.Length; i++)
                LoadTexture(slotTextureIds[i], textureFiles[i]);
        }

        private void LoadTexture(int textureId, string filename)
        {
            try
            {
                Bitmap bmp = new Bitmap(filename);
                BitmapData data = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.BindTexture(TextureTarget.Texture2D, textureId);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, // Rgba include canalul alpha
                                     bmp.Width, bmp.Height, 0,
                                     OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                                     PixelType.UnsignedByte, data.Scan0);

                bmp.UnlockBits(data);
                bmp.Dispose();

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea texturii {filename}: {ex.Message}");
            }
        }

        private void btnTrageSlot_Click(object sender, EventArgs e)
        {
            if (isSpinning) return;

            remainingCycles = Math.Max(30, (int)numericCicluriSlot.Value);

            isSpinning = true;
            btnTrageSlot.Enabled = false;
            numericCicluriSlot.Enabled = false;

            labelStatus.Text = "Rotire...";
            slotTimer.Start();
        }

        private void SlotTimer_Tick(object sender, EventArgs e)
        {
            if (remainingCycles > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    int step = (remainingCycles > 30) ? random.Next(1, 4) : 1;
                    currentSlotImage[i] = (currentSlotImage[i] + step) % 4;
                }

                remainingCycles--;
                GlControl1.Invalidate();
            }
            else
            {
                slotTimer.Stop();
                isSpinning = false;
                btnTrageSlot.Enabled = true;
                numericCicluriSlot.Enabled = true;
                GlControl1.Invalidate();
                VerificaCastig();
            }
        }

        private void VerificaCastig()
        {
            if (currentSlotImage[0] == currentSlotImage[1] && currentSlotImage[1] == currentSlotImage[2])
                labelStatus.Text = "🎉 FELICITĂRI! AI CÂȘTIGAT!";
            else
                labelStatus.Text = "Ai pierdut. Încearcă din nou.";
        }

        private void GlControl1_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, GlControl1.Width, GlControl1.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            float aspect = GlControl1.Width / (float)GlControl1.Height;
            Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspect, 1f, 100f);
            GL.LoadMatrix(ref perspectiveMatrix);

            GlControl1.Invalidate();
        }

        private void DeseneazaCorpSlotMachine()
        {
            GL.Disable(EnableCap.Texture2D);

            // ** ÎMBUNĂTĂȚIRE: Culoare și Material Metalic/Lucios **
            GL.Color3(0.7f, 0.05f, 0.05f); // Roșu Închis/Bordeaux

            // Setarea materialului pentru reflexii
            float[] specular_color = { 1.0f, 1.0f, 1.0f, 1.0f };
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, specular_color);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 50.0f);

            GL.PushMatrix();
            GL.Scale(15f, 25f, 5f);

            GL.Begin(PrimitiveType.Quads);

            // [Desenarea fețelor cubului rămâne la fel]
            GL.Normal3(0, 0, 1); GL.Vertex3(1, 1, 1); GL.Vertex3(-1, 1, 1); GL.Vertex3(-1, -1, 1); GL.Vertex3(1, -1, 1);
            GL.Normal3(0, 0, -1); GL.Vertex3(1, 1, -1); GL.Vertex3(1, -1, -1); GL.Vertex3(-1, -1, -1); GL.Vertex3(-1, 1, -1);
            GL.Normal3(0, 1, 0); GL.Vertex3(1, 1, 1); GL.Vertex3(1, 1, -1); GL.Vertex3(-1, 1, -1); GL.Vertex3(-1, 1, 1);
            GL.Normal3(0, -1, 0); GL.Vertex3(1, -1, 1); GL.Vertex3(-1, -1, 1); GL.Vertex3(-1, -1, -1); GL.Vertex3(1, -1, -1);
            GL.Normal3(1, 0, 0); GL.Vertex3(1, 1, 1); GL.Vertex3(1, -1, 1); GL.Vertex3(1, -1, -1); GL.Vertex3(1, 1, -1);
            GL.Normal3(-1, 0, 0); GL.Vertex3(-1, 1, 1); GL.Vertex3(-1, 1, -1); GL.Vertex3(-1, -1, -1); GL.Vertex3(-1, -1, 1);

            GL.End();
            GL.PopMatrix();

            // Resetare material
            GL.Color3(Color.White);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0f, 0f, 0f, 1f });
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 0.0f);

            GL.Enable(EnableCap.Texture2D);
        }

        private void DeseneazaSlot(int slotIndex, int textureId)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // ** FIX TRANSPARENȚĂ **
            // Setarea mediului texturii la REPLACE forțează OpenGL să folosească 
            // culoarea și alpha-ul texturii direct, ignorând culoarea primitivelor
            // și efectele de iluminare asupra culorii de bază a slotului.
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Replace);

            GL.Color3(Color.White); // Culoarea de bază (necesară pentru alte moduri, dar ignorată de REPLACE)

            GL.PushMatrix();
            float xOffset = (slotIndex - 1) * 8f;
            GL.Translate(xOffset, 0f, 5.1f);
            GL.Scale(6f, 12f, 0.1f);

            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(0, 0, 1);
            GL.TexCoord2(0, 1); GL.Vertex3(-1, -1, 0);
            GL.TexCoord2(1, 1); GL.Vertex3(1, -1, 0);
            GL.TexCoord2(1, 0); GL.Vertex3(1, 1, 0);
            GL.TexCoord2(0, 0); GL.Vertex3(-1, 1, 0);
            GL.End();

            GL.PopMatrix();
        }

        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!isLoaded) return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // CAMERA SIMPLĂ (FRONTALĂ)
            Matrix4 lookatMatrix = Matrix4.LookAt(0, 0, 30, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookatMatrix);

            DeseneazaCorpSlotMachine();

            for (int i = 0; i < 3; i++)
                DeseneazaSlot(i, slotTextureIds[currentSlotImage[i]]);

            GlControl1.SwapBuffers();
        }
    }
}