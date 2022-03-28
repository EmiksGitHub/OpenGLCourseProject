using System;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.DevIl;

namespace Shuvalov_Roman_PRI_118_KP
{
    public partial class Form1 : Form
    {
        double sizeX = 1, sizeY = 1, sizeZ = 1;
        double a = 10;
        double deltaA;
        int kolobokX = 153;
        int kolobokY = 155;
        double kolobokZ = 18.5;
        double kolobokR;
        int portalX = 8;
        int portalY = 13;
        bool moveBall = false;
        Point ball;
        double cameraSpeed = 5;
        private Explosion explosion = new Explosion(1, 10, 1, 300, 900);
        private Explosion explosionPortal = new Explosion(0, 0, 0, 20, 50);
        bool isExplosion = false;
        int imageId;
        uint mGlTextureObject;
        int imageId2;
        uint mGlTextureObject2;
        bool addKolobok = false;
        bool kolobokBorn = false;
        bool kolobokOnEarth = false;
        double localTime;

        double angle = 10, angleX = -82, angleY = 0, angleZ = -60; double translateX = -62, translateY = 13, translateZ = -15;

        float global_time = 0;
        bool flagExplosion = false;


        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            numericX.Value = ((decimal)cameraSpeed);
            numericAngle.Value = ((decimal)angle);
            //textBox1.Text = "double angle = " + angle + ", angleX = " + angleX + ", angleY = " + angleY +
            //    ", angleZ = " + angleZ + "; double translateX = " + translateX +
            //    ", translateY = " + translateY + ", translateZ = " + translateZ + ";";
            //label1.Text = "X: " + translateX + ", " + "Y: " + translateY + ", Z: " + translateZ + ".";
            //label2.Text = "Коорд. портала: \nX: " + (float)portalX + ", Y: " + (float)portalY;
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            ball = new Point(-2.3, 25, 16.7);
            // инициализация OpenGL
            // инициализация бибилиотеки glut
            Glut.glutInit();
            // инициализация режима экрана
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // установка цвета очистки экрана (RGBA)
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы
            Gl.glLoadIdentity();

            Glu.gluPerspective(60, (float)AnT.Width / (float)AnT.Height, 0.1, 800);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            // ЗАГУРУЗКА ИЗОБРАЖЕНИЯ ПО УМОЛЧАНИЮ
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage("../../texture/brick.png"))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId2);
            Il.ilGenImages(1, out imageId2);
            Il.ilBindImage(imageId2);
            if (Il.ilLoadImage("../../texture/background.png"))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        mGlTextureObject2 = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject2 = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(2, ref imageId2);

            RenderTimer.Start();
        }

        private void numericX_ValueChanged(object sender, EventArgs e)
        {
            cameraSpeed = ((double)numericX.Value);
        }

        private void numericAngle_ValueChanged(object sender, EventArgs e)
        {
            angle = ((double)numericAngle.Value);
        }

        private void buttonAddKolobok_Click(object sender, EventArgs e)
        {
            addKolobok = true;
            AnT.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    angle = 10; angleX = -82; angleY = 0; angleZ = -60;
                    translateX = -62; translateY = 13; translateZ = -15;
                    break;
                case 1:
                    angle = 10; angleX = -62; angleY = 0; angleZ = -50; 
                    translateX = -37; translateY = -12; translateZ = -25;
                    break;
                case 2:
                    angle = 10; angleX = -72; angleY = 0; angleZ = -60; 
                    translateX = -32; translateY = 28; translateZ = -20;
                    break;
                case 3:
                    angle = 10; angleX = -72; angleY = 0; angleZ = -230; 
                    translateX = 38; translateY = -62; translateZ = -30;
                    break;
            }
            AnT.Focus();
        }

        private void buttonAnimation_Click(object sender, EventArgs e)
        {
            moveBall = true;
            AnT.Focus();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
            {
                translateY -= cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                translateY += cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                translateX += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                translateX -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                translateZ += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                translateZ -= cameraSpeed;
            }


            if (e.KeyCode == Keys.Q)
            {
                angleZ -= angle;
            }
            if (e.KeyCode == Keys.E)
            {
                angleZ += angle;
            }
            if (e.KeyCode == Keys.R)
            {
                angleX -= angle;
            }
            if (e.KeyCode == Keys.F)
            {
                angleX += angle;
            }
            if (e.KeyCode == Keys.Z)
            {
                sizeX += 0.1;
            }
            if (e.KeyCode == Keys.X)
            {
                sizeX -= 0.1;
            }



            if (e.KeyCode == Keys.Y && kolobokY <= 201 && kolobokOnEarth)
            {
                kolobokY += 1;
            }
            if (e.KeyCode == Keys.H && kolobokY >= -18 && kolobokOnEarth)
            {
                kolobokY -= 1;
            }
            if (e.KeyCode == Keys.G && kolobokX >= -14 && kolobokOnEarth)
            {
                kolobokX -= 1;
            }
            if (e.KeyCode == Keys.J && kolobokX <= 205 && kolobokOnEarth)
            {
                kolobokX += 1;
            }



            if (e.KeyCode == Keys.O && portalY <= 17)
            {
                portalY += 1;
            }
            if (e.KeyCode == Keys.L && portalY >= 5)
            {
                portalY -= 1;
            }
            if (e.KeyCode == Keys.K && portalX >= 2)
            {
                portalX -= 1;
            }
            if (e.KeyCode == Keys.OemSemicolon && portalX <= 18)
            {
                portalX += 1;
            }
        }

















        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glRotated(angleX, 1, 0, 0); Gl.glRotated(angleY, 0, 1, 0); Gl.glRotated(angleZ, 0, 0, 1);
            Gl.glTranslated(translateX, translateY, translateZ);
            Gl.glScaled(sizeX, sizeY, sizeZ);
            Gl.glColor3f(0.99f, 0.00f, 0.00f);
            explosion.Calculate(global_time);
            Gl.glColor3f(1f / 255f * 153, 1f / 255f * 17, 1f / 255f * 121);
            explosionPortal.Calculate(global_time);
            Gl.glPushMatrix();
            if (isExplosion)
            {
                flagExplosion = true;
                isExplosion = false;
                explosion.SetNewPosition(-7, -15.5f, 0);
                explosion.SetNewPower(90);
                explosion.Boooom(global_time, 25, 3);
            }
            Gl.glPushMatrix();













            Gl.glPushMatrix();

            // Столбы
            Gl.glTranslated(0, 125, 0);
            Gl.glScaled(0.2, 0.2, 10);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(0, 25, 0);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(-25, 0, 0);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(0, -25, 0);
            block(0, 0, 0, 147, 140, 78, 3);

            // Пол
            Gl.glTranslated(0, 25, 1.5);
            Gl.glScaled(13.5, 13.5, 0.01);
            block(0, 0, 0, 147, 140, 78, 3);
            // Потолок
            Gl.glTranslated(0, 0, 50);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Point[] krug = new Point[40];
            double grad = Math.PI * 2 / 40;
            for (int i = 0; i < 40; i++)
            {
                krug[i] = new Point();
                krug[i].x = 0;
                krug[i].y = Math.Sin(grad * i) * 2 + 27.3;
                krug[i].z = Math.Cos(grad * i) * 2 + 17.5;
            }
            krug = cilinder(krug, 3, 28, 16.5, 2, -15, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 5, 27.3, 15.5, 2, -30, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 6.4, 25.8, 14, 2, -45, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 6.6, 24, 13, 2, -60, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 5.8, 21.5, 12, 1.7, -75, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 4.5, 19, 10, 1.6, -90, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 1, 17.2, 8, 1.7, -130, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -4, 18, 7, 2, -185, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -8, 20, 6, 2, -210, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -11, 24, 4, 2, -260, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -12, 31, 2, 2, -290, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            Gl.glPopMatrix();
















































            /////////////////////
            // ЗЕМЛЯ
            /////////////////////
            ///

            Gl.glPushMatrix();
            Gl.glColor3f(1f / 255f * 21, 1f / 255f * 153, 1f / 255f * 37);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(200, 200, 0);
            Gl.glVertex3d(-200, 200, 0);
            Gl.glVertex3d(-200, -200, 0);
            Gl.glVertex3d(200, -10, 0);
            Gl.glEnd();
            Gl.glPopMatrix();



            /////////////////////
            // РИНГ ДЛЯ КОЛОБКА
            /////////////////////
            ///
            Gl.glPushMatrix();
            Gl.glTranslated(7, 18, 0);
            Gl.glColor3f(1f / 255f * 219, 1f / 255f * 219, 1f / 255f * 2);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(25, 25, 0.5);
            Gl.glVertex3d(0, 25, 0.5);
            Gl.glVertex3d(0, 0, 0.5);
            Gl.glVertex3d(25, 0, 0.5);
            Gl.glEnd();
            Gl.glPopMatrix();


            /////////////////////
            // КАРКАС ГОРКИ
            /////////////////////
            ///

            Gl.glPushMatrix();

            // Столбы
            Gl.glTranslated(0, 25, 0);
            Gl.glScaled(0.2, 0.2, 10);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(0, 25, 0);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(-25, 0, 0);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glTranslated(0, -25, 0);
            block(0, 0, 0, 147, 140, 78, 3);

            // Пол
            Gl.glTranslated(0, 25, 1.5);
            Gl.glScaled(13.5, 13.5, 0.01);
            block(0, 0, 0, 147, 140, 78, 3);
            // Потолок
            Gl.glTranslated(0, 0, 50);
            block(0, 0, 0, 147, 140, 78, 3);
            Gl.glPopMatrix();




            /////////////////////
            // КРУГОВАЯ ГОРКА
            /////////////////////
            ///


            Gl.glPushMatrix();
            Point[] krug = new Point[40];
            double grad = Math.PI * 2 / 40;
            for (int i = 0; i < 40; i++)
            {
                krug[i] = new Point();
                krug[i].x = 0;
                krug[i].y = Math.Sin(grad * i) * 2 + 27.3;
                krug[i].z = Math.Cos(grad * i) * 2 + 17.5;
            }
            krug = cilinder(krug, 3, 28, 16.5, 2, -15, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 5, 27.3, 15.5, 2, -30, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 6.4, 25.8, 14, 2, -45, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 6.6, 24, 13, 2, -60, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 5.8, 21.5, 12, 1.7, -75, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 4.5, 19, 10, 1.6, -90, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, 1, 17.2, 8, 1.7, -130, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -4, 18, 7, 2, -185, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -8, 20, 6, 2, -210, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -11, 24, 4, 2, -260, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            krug = cilinder(krug, -12, 31, 2, 2, -290, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            Gl.glPopMatrix();

            /////////////////////
            // ПРЯМАЯ ГОРКА
            /////////////////////
            ///

            Gl.glPushMatrix();
            Gl.glRotated(30, 1, 0, 0);
            Gl.glTranslated(-4.5, 28.8, 0.75);
            Gl.glScaled(2.2, 23, 0.01);
            block(0, 0, 0, 48, 204, 204, 5);
            Gl.glPopMatrix();

            /////////////////////
            // КАЧЕЛЬ
            /////////////////////
            ///

            // ДОСКА КАЧЕЛИ
            Gl.glPushMatrix();
            Gl.glRotated(a, 41, 25, 1);
            //Gl.glTranslated(40, 35, 2);

            Gl.glPushMatrix();
            //Gl.glTranslated(0.5, 0, 1.5);
            Gl.glTranslated(41, 35, 3.5);
            Gl.glColor3f(1f / 255f * 240, 1f / 255f * 193, 1f / 255f * 24);
            Glut.glutSolidSphere(1.1, 32, 32);
            Gl.glTranslated(0, -20, 0);
            Glut.glutSolidSphere(1.1, 32, 32);
            Gl.glPopMatrix();

            Gl.glRotated(a, 41, 25, 1);
            Gl.glTranslated(40, 35, 2);
            Gl.glPushMatrix();
            Gl.glScaled(1, 10, 0.2);
            block(0, 0, 0, 48, 204, 204, 1);
            Gl.glPopMatrix();

            a += deltaA;
            if (a == -10) deltaA = 0.5;
            if (a == 10) deltaA = -0.5;
            Gl.glPopMatrix();


            // ОПОРА КАЧЕЛИ
            Gl.glPushMatrix();
            Gl.glTranslated(40, 25, 0);
            Point[] kachel = new Point[40];
            double ugol = Math.PI * 2 / 40;
            for (int i = 0; i < 40; i++)
            {
                kachel[i] = new Point();
                kachel[i].x = -0.5;
                kachel[i].y = Math.Sin(ugol * i) * 1 + 0;
                kachel[i].z = Math.Cos(ugol * i) * 1 + 1;
            }
            kachel = cilinder(kachel, 2.5, 0, 1, 1, 0, "x", false, 1f / 255f * 65, 1f / 255f * 95, 1f / 255f * 217, 0, 0, 0);
            Gl.glPopMatrix();

            /////////////////////
            // ОГРАЖДЕНИЕ ПЕСОЧНИЦЫ
            /////////////////////
            ///
            // опоры
            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4, 0);
            Gl.glScaled(0.2, 0.2, 1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4 + 24.6, 0);
            Gl.glScaled(0.2, 0.2, 1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7 + 24.6, 18.4 + 24.6, 0);
            Gl.glScaled(0.2, 0.2, 1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7 + 24.6, 18.4, 0);
            Gl.glScaled(0.2, 0.2, 1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            // перила
            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4, 1);
            Gl.glScaled(24.6 / 2, 0.2, 0.1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4 + 24.6, 1);
            Gl.glScaled(24.6 / 2, 0.2, 0.1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4 + 24.6, 1);
            Gl.glScaled(0.2, 24.6 / 2, 0.1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(7 + 24.6, 18.4 + 24.6, 1);
            Gl.glScaled(0.2, 24.6 / 2, 0.1);
            block(0, 0, 0, 147, 140, 78, 1);
            Gl.glPopMatrix();
            


            /////////////////////
            // ЯДРО
            /////////////////////
            ///
            Gl.glPushMatrix();

            if (moveBall && ball.z > 1.5)
            {
                ball.y -= 0.5;
                ball.z -= 0.28;
            }
            if (moveBall && ball.z <= 1.5 && ball.y >= -15)
            {
                ball.y -= 0.5;
            }
            Gl.glTranslated(ball.x, ball.y, ball.z);
            if (ball.y != -15.5)
            {
                Gl.glColor3f(1f / 255f * 0, 1f / 255f * 0, 1f / 255f * 0);
                Glut.glutSolidSphere(1.5, 32, 32);
            }
            else
            {
                if (!flagExplosion) isExplosion = true;
            }
            Gl.glPopMatrix();



            /////////////////////
            // ПРЕПЯТСТВИЕ
            /////////////////////
            ///
            Gl.glPushMatrix();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);



            Gl.glPushMatrix();
            Gl.glTranslated(-7, -15.5, 0);
            Gl.glScaled(4, 1, 2);
            if (ball.y != -15.5)
            {
                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glVertex3d(2, 0, 0);
                Gl.glVertex3d(0, 0, 0);
                Gl.glVertex3d(0, 0 - 2, 0);
                Gl.glVertex3d(0 + 2, 0 - 2, 0);
                Gl.glEnd();

                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(2, 0, 0 + 2);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(2, 0, 0);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(2, 0 - 2, 0);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3d(2, 0 - 2, 0 + 2);
                Gl.glEnd();

                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(2, 0, 0 + 2);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(0, 0, 0 + 2);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(0, 0 - 2, 0 + 2);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3d(2, 0 - 2, 0 + 2);
                Gl.glEnd();

                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(0, 0, 0 + 2);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(0, 0, 0);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3d(0, 0 - 2, 0);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(0, 0 - 2, 0 + 2);
                Gl.glEnd();

                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(2, 0, 0);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(0, 0, 0);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(0, 0, 0 + 2);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3d(2, 0, 0 + 2);
                Gl.glEnd();

                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(1, 0);
                Gl.glVertex3d(2, 0 - 2, 0);
                Gl.glTexCoord2f(1, 1);
                Gl.glVertex3d(0, 0 - 2, 0);
                Gl.glTexCoord2f(0, 1);
                Gl.glVertex3d(0, 0 - 2, 0 + 2);
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3d(2, 0 - 2, 0 + 2);
                Gl.glEnd();

                Gl.glColor3f(0, 0, 0);
                Gl.glLineWidth(1);
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(2, 0, 0);
                Gl.glVertex3d(0, 0, 0);
                Gl.glVertex3d(0, 0, 0 + 2);
                Gl.glVertex3d(2, 0, 0 + 2);
                Gl.glVertex3d(2, 0, 0);
                Gl.glVertex3d(2, 0 - 2, 0);
                Gl.glVertex3d(0, 0 - 2, 0);
                Gl.glVertex3d(0, 0, 0);
                Gl.glEnd();
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(2, 0 - 2, 0 + 2);
                Gl.glVertex3d(0, 0 - 2, 0 + 2);
                Gl.glVertex3d(0, 0 - 2, 0);
                Gl.glEnd();
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(0, 0 - 2, 0 + 2);
                Gl.glVertex3d(0, 0, 0 + 2);
                Gl.glEnd();
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(2, 0, 0 + 2);
                Gl.glVertex3d(2, 0 - 2, 0 + 2);
                Gl.glVertex3d(2, 0 - 2, 0);
                Gl.glEnd();
            }

            Gl.glDisable(Gl.GL_BLEND);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
            Gl.glPopMatrix();




            /////////////////////
            // ФОН
            /////////////////////
            ///
            Gl.glPushMatrix();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject2);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            Gl.glPushMatrix();
            Gl.glTranslated(-200, -200, -50);

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(0, 0, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 200, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(0, 200, 200);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(0, 0, 200);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(0, 200, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(0, 400, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(0, 400, 200);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(0, 200, 200);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(0, 400, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(200, 400, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(200, 400, 200);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(0, 400, 200);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(200, 400, 0);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3d(400, 400, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(400, 400, 200);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(200, 400, 200);
            Gl.glEnd();

            Gl.glDisable(Gl.GL_BLEND);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
            Gl.glPopMatrix();



            /////////////////////
            // ПОРТАЛ
            /////////////////////
            ///

            Gl.glPushMatrix();
            Gl.glTranslated(7, 18.4, 20);
            Gl.glTranslated(3, 3, 0);
            Gl.glTranslated(portalX, portalY, 0);
            Gl.glColor3f(1f / 255f * 150, 1f / 255f * 15, 1f / 255f * 120);
            circle(0, 0, 0.2, 2, "z", 90, false, false, 40, 2);
            circle(0, 0, 0, 2, "z", 90, false, false, 40, 2);
            circle(0, 0, -0.2, 2, "z", 90, false, false, 40, 2);
            Gl.glPopMatrix();
            if (addKolobok)
            {
                // колобок в песочнице
                Gl.glPushMatrix();
                Gl.glColor3f(1f / 255f * 240, 1f / 255f * 193, 1f / 255f * 24);
                Gl.glTranslated(7, 18.4, 0.5);
                Gl.glTranslated(3, 3, 1.1);
                if (!kolobokBorn)
                {
                    kolobokX = portalX * 10;
                    kolobokY = portalY * 10;
                    Gl.glTranslated(kolobokX / 10f, kolobokY / 10f, kolobokZ);
                    kolobokR = 0.1;
                    explosionPortal.SetNewPosition(kolobokX/10+10, kolobokY/10+21.4f, ((float)kolobokZ+0.5f));
                    explosionPortal.SetNewPower(6);
                    explosionPortal.Boooom(global_time, 15, 1);
                    Glut.glutSolidSphere(kolobokR, 32, 32);
                    kolobokBorn = true;
                }
                if (kolobokR < 1.1)
                {
                    Gl.glTranslated(kolobokX / 10f, kolobokY / 10f, kolobokZ);
                    kolobokR += 0.04;
                    Glut.glutSolidSphere(kolobokR, 32, 32);
                    localTime = global_time;
                } else
                {
                    Gl.glTranslated(kolobokX / 10f, kolobokY / 10f, kolobokZ);
                    Glut.glutSolidSphere(1.1, 32, 32);
                    if (global_time - localTime > 1 && kolobokZ > 0)
                    {
                        kolobokZ -= 0.2;
                        Gl.glTranslated(kolobokX / 10f, kolobokY / 10f, kolobokZ);
                        if (kolobokZ < 0.3) kolobokOnEarth = true;
                    }
                }

                if (kolobokOnEarth)
                {
                    Gl.glTranslated(kolobokX / 10f, kolobokY / 10f, 0);
                }
                Gl.glPopMatrix();
            }



            /////////////////////
            // ЛЕСТНИЦА
            /////////////////////
            ///
            Gl.glPushMatrix();
            Gl.glTranslated(-5, 32, 14);
            Gl.glPushMatrix();
            Gl.glScaled(2.7, 1, 0.5);
            for (double i = 0; i < 15; i++)
            {
                block(0, 0+i*1.3, 0-i*2, 147, 140, 78, 3);
            }
            Gl.glPopMatrix();
            Gl.glPopMatrix();


            // возвращаем состояние матрицы
            Gl.glPopMatrix();
            // отрисовываем геометрию
            Gl.glFlush();

            // обновляем состояние элемента
            AnT.Invalidate();
        }


        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {

            // идентификатор текстурного объекта
            uint texObject;

            // генерируем текстурный объект
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // создаем RGB или RGBA текстуру
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }

            // возвращаем идентификатор текстурного объекта

            return texObject;

        }

        private void block(double x, double y, double z, float r, float g, float b, float lineWidth)
        {
            Gl.glColor3f(1f / 255f * r, 1f / 255f * g, 1f / 255f * b);


            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(x + 2, y, z);
            Gl.glVertex3d(x, y, z);
            Gl.glVertex3d(x, y - 2, z);
            Gl.glVertex3d(x + 2, y - 2, z);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(x + 2, y, z + 2);
            Gl.glVertex3d(x + 2, y, z);
            Gl.glVertex3d(x + 2, y - 2, z);
            Gl.glVertex3d(x + 2, y - 2, z + 2);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);

            Gl.glVertex3d(x + 2, y, z + 2);
            Gl.glVertex3d(x, y, z + 2);
            Gl.glVertex3d(x, y - 2, z + 2);
            Gl.glVertex3d(x + 2, y - 2, z + 2);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(x, y, z + 2);
            Gl.glVertex3d(x, y, z);
            Gl.glVertex3d(x, y - 2, z);
            Gl.glVertex3d(x, y - 2, z + 2);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(x + 2, y, z);
            Gl.glVertex3d(x, y, z);
            Gl.glVertex3d(x, y, z + 2);
            Gl.glVertex3d(x + 2, y, z + 2);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(x + 2, y - 2, z);
            Gl.glVertex3d(x, y - 2, z);
            Gl.glVertex3d(x, y - 2, z + 2);
            Gl.glVertex3d(x + 2, y - 2, z + 2);
            Gl.glEnd();

            Gl.glColor3f(0, 0, 0);
            Gl.glLineWidth(lineWidth);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex3d(x + 2, y, z);
            Gl.glVertex3d(x, y, z);
            Gl.glVertex3d(x, y, z + 2);
            Gl.glVertex3d(x + 2, y, z + 2);
            Gl.glVertex3d(x + 2, y, z);
            Gl.glVertex3d(x + 2, y - 2, z);
            Gl.glVertex3d(x, y - 2, z);
            Gl.glVertex3d(x, y, z);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex3d(x + 2, y - 2, z + 2);
            Gl.glVertex3d(x, y - 2, z + 2);
            Gl.glVertex3d(x, y - 2, z);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex3d(x, y - 2, z + 2);
            Gl.glVertex3d(x, y, z + 2);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex3d(x + 2, y, z + 2);
            Gl.glVertex3d(x + 2, y - 2, z + 2);
            Gl.glVertex3d(x + 2, y - 2, z);
            Gl.glEnd();
        }

        private Point[] circle(double x, double y, double z, double R, string os, double grad, bool ba, bool bb, int count, float width)
        {
            Point[] krug = new Point[count];
            grad = grad / count;
            double a, b;
            Gl.glLineWidth(width);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            if (os.Equals("x"))
            {
                for (int i = 0; i < count; i++)
                {
                    if (ba)
                        a = Math.Cos(grad * i) * R + z;
                    else
                        a = -Math.Cos(grad * i) * R + z;
                    if (bb)
                        b = Math.Sin(grad * i) * R + y;
                    else
                        b = -Math.Sin(grad * i) * R + y;
                    krug[i] = new Point(x, b, a);
                    Gl.glVertex3d(x, b, a);
                }
            }
            if (os.Equals("y"))
            {
                for (int i = 0; i < count; i++)
                {
                    if (ba)
                        a = Math.Cos(grad * i) * R + x;
                    else
                        a = -Math.Cos(grad * i) * R + x;
                    if (bb)
                        b = Math.Sin(grad * i) * R + z;
                    else
                        b = -Math.Sin(grad * i) * R + z;
                    krug[i] = new Point(a, y, b);
                    Gl.glVertex3d(a, y, b);
                }
            }
            if (os.Equals("z"))
            {
                for (int i = 0; i < count; i++)
                {
                    if (ba)
                        a = Math.Cos(grad * i) * R + x;
                    else
                        a = -Math.Cos(grad * i) * R + x;
                    if (bb)
                        b = Math.Sin(grad * i) * R + y;
                    else
                        b = -Math.Sin(grad * i) * R + y;
                    krug[i] = new Point(a, b, z);
                    Gl.glVertex3d(a, b, z);
                }
            }
            Gl.glEnd();
            return krug;

        }

        private Point[] cilinder(Point[] circle0, double x1, double y1, double z1, double R1, double rota, string os, bool flag2, float red, float green, float blue, float redOS, float greenOS, float blueOS)
        {
            Point[] circle1 = new Point[40];
            int count = 40;
            double grad = Math.PI * 2 / count;
            for (int i = 0; i < count; i++)
            {

                circle1[i] = new Point();

                if (os.Equals("z"))
                {
                    circle1[i].x = Math.Cos(grad * i) * R1 + x1;
                    circle1[i].y = Math.Sin(grad * i) * R1 + y1;
                    circle1[i].z = z1;
                }
                if (os.Equals("y"))
                {
                    circle1[i].x = Math.Cos(grad * i) * R1 + x1;
                    circle1[i].y = y1;
                    circle1[i].z = Math.Sin(grad * i) * R1 + z1;
                }
                if (os.Equals("x"))
                {
                    circle1[i].x = x1;
                    circle1[i].y = Math.Sin(grad * i) * R1 + y1;
                    circle1[i].z = Math.Cos(grad * i) * R1 + z1;
                }
                if (rota != 0)
                {
                    circle1[i].x = circle1[i].x - x1;
                    circle1[i].y = circle1[i].y - y1;
                    circle1[i].x = circle1[i].x * Math.Cos((rota * Math.PI) / 180) - circle1[i].y * Math.Sin((rota * Math.PI) / 180);
                    circle1[i].y = circle1[i].x * Math.Sin((rota * Math.PI) / 180) + circle1[i].y * Math.Cos((rota * Math.PI) / 180);
                    circle1[i].x = circle1[i].x + x1;
                    circle1[i].y = circle1[i].y + y1;
                }
            }
            Gl.glColor3f(red, green, blue);
            Gl.glLineWidth(5f);
            for (int i = 0; i < count - 1; i++)
            {
                Gl.glColor3f(red, green, blue);
                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glVertex3d(circle0[i].x, circle0[i].y, circle0[i].z);
                Gl.glVertex3d(circle1[i].x, circle1[i].y, circle1[i].z);
                Gl.glVertex3d(circle1[i + 1].x, circle1[i + 1].y, circle1[i + 1].z);
                Gl.glVertex3d(circle0[i + 1].x, circle0[i + 1].y, circle0[i + 1].z);
                Gl.glEnd();
                Gl.glColor3f(redOS, greenOS, blueOS);
                if (!flag2)
                {
                    Gl.glBegin(Gl.GL_LINE_STRIP);
                    Gl.glVertex3d(circle0[i].x, circle0[i].y, circle0[i].z);
                    Gl.glVertex3d(circle0[i + 1].x, circle0[i + 1].y, circle0[i + 1].z);
                    Gl.glEnd();
                    Gl.glBegin(Gl.GL_LINE_STRIP);
                    Gl.glVertex3d(circle1[i].x, circle1[i].y, circle1[i].z);
                    Gl.glVertex3d(circle1[i + 1].x, circle1[i + 1].y, circle1[i + 1].z);
                    Gl.glEnd();
                }
            }
            Gl.glColor3f(red, green, blue);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex3d(circle0[count - 1].x, circle0[count - 1].y, circle0[count - 1].z);
            Gl.glVertex3d(circle1[count - 1].x, circle1[count - 1].y, circle1[count - 1].z);
            Gl.glVertex3d(circle1[0].x, circle1[0].y, circle1[0].z);
            Gl.glVertex3d(circle0[0].x, circle0[0].y, circle0[0].z);
            Gl.glEnd();

            if (!flag2)
            {
                Gl.glColor3f(redOS, greenOS, blueOS);
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(circle0[count - 1].x, circle0[count - 1].y, circle0[count - 1].z);
                Gl.glVertex3d(circle0[0].x, circle0[0].y, circle0[0].z);
                Gl.glEnd();
                Gl.glBegin(Gl.GL_LINE_STRIP);
                Gl.glVertex3d(circle1[count - 1].x, circle1[count - 1].y, circle1[count - 1].z);
                Gl.glVertex3d(circle1[0].x, circle1[0].y, circle1[0].z);
                Gl.glEnd();
            }
            return circle1;
        }
    }
}
