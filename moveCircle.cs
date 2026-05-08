using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoveCircle
{
    public partial class FormBallGame : Form
    {
        // 背景に描画する正解の一文字
        private string correctText = "獲";
        private Bitmap canvas = null;

        public FormBallGame()
        {
            InitializeComponent();
        }

        // [List 2] フォームロード時の処理
        private void FormBallGame_Load(object sender, EventArgs e)
        {
            // メインの背景（正解の文字）を描画 [tint 3]
            DrawMainPictureBox(Brushes.Gray, correctText);

            // 選択用の円（ボール）を描画
            DrawCircleSelectPictureBox();
        }

        // メインの背景を描画するメソッド
        private void DrawMainPictureBox(Brush color, string text)
        {
            // キャンバスが空（Nothing/null）の場合のみ新しく作成
            if (canvas == null)
            {
                canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            }

            // キャンバスへ描画する準備
            using (Graphics g = Graphics.FromImage(canvas))
            {
                // 背景の文字を描画
                g.DrawString(
                    text,
                    new Font(this.Font.FontFamily, MainPictureBox.Height * 2 / 3), // 高さの2/3サイズ
                    color,
                    (float)(MainPictureBox.Width / 4.5), // X座標調整
                    (float)(-MainPictureBox.Height / 10) // Y座標調整
                );

                // キャンバスの絵を画像としてセット
                MainPictureBox.Image = canvas;
            }
        }

        // 選択用円を描画するメソッド
        private void DrawCircleSelectPictureBox()
        {
            int height = SelectPictureBox.Height;
            int width = SelectPictureBox.Width;
            Bitmap selectCanvas = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(selectCanvas))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.LightBlue, 0, 0, height, height);
                SelectPictureBox.Image = selectCanvas;
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoveCircle
{
    public partial class FormBallGame : Form
    {
        // プロパティ・変数の設定
        private string correctText = "獲";
        private string FontName = "MS UI Gothic"; // 漢字のフォント名初期設定
        private Bitmap canvas = null;
        private Point position = new Point(0, 0);
        private Point previous = new Point(0, 0);
        private Point direction = new Point(1, 1); // 移動方向
        private int radius = 20; // 半径

        public FormBallGame()
        {
            InitializeComponent();
        }

        // [List 2] ロード時の処理
        private void FormBallGame_Load(object sender, EventArgs e)
        {
            // 背景の正解漢字を描画 [tint 3]
            DrawMainPictureBox(Brushes.Gray, correctText);

            // 選択用PictureBoxの描画
            DrawCircleSelectPictureBox();
        }

        // 背景を描画するメソッド
        private void DrawMainPictureBox(Brush color, string text)
        {
            // キャンバスがNothing(null)なら作成
            canvas ??= new Bitmap(MainPictureBox.Width, MainPictureBox.Height);

            using (Graphics g = Graphics.FromImage(canvas))
            {
                // 背景に漢字を描画
                g.DrawString(
                    text,
                    new Font(this.Font.FontFamily, MainPictureBox.Height * 2 / 3),
                    color,
                    (float)(MainPictureBox.Width / 4.5),
                    (float)(-MainPictureBox.Height / 10)
                );
                MainPictureBox.Image = canvas;
            }
        }

        // 指定した位置にボールを描画する
        public void PutCircle(int x, int y, Brush brushColor)
        {
            // 現在の場所を記憶
            position.X = x;
            position.Y = y;

            canvas ??= new Bitmap(MainPictureBox.Width, MainPictureBox.Height);

            using (Graphics g = Graphics.FromImage(canvas))
            {
                // 円を描く (FillEllipse)
                g.FillEllipse(brushColor, x, y, radius * 2, radius * 2);

                // 文字を描く
                g.DrawString(correctText, new Font(FontName, radius + 4), Brushes.Black, x, y, new StringFormat());

                MainPictureBox.Image = canvas;
            }
        }

        // 指定した場所のボールを消去する（白で塗る）
        public void DeleteCircle()
        {
            // 初回呼び出し時、previousが空なら現在の位置をセット
            if (previous.X == 0 && previous.Y == 0)
            {
                previous = new Point(position.X, position.Y);
            }

            using (Graphics g = Graphics.FromImage(canvas))
            {
                // 中を白で塗る
                g.FillEllipse(Brushes.White, previous.X, previous.Y, radius * 2, radius * 2);
                MainPictureBox.Image = canvas;
            }
        }

        // ボールを移動させる
        public void MoveBall()
        {
            // ① 前回の表示を消す
            previous = new Point(position.X, position.Y);
            DeleteCircle();

            // ② 新しい移動先の計算
            int x = position.X + direction.X * 5; // 5はスピード
            int y = position.Y + direction.Y * 5;

            // ③ 壁跳ね返り補正
            // 右端に到達
            if (x > MainPictureBox.Width - radius * 2)
            {
                direction.X = -1;
            }
            // 左端に到達
            if (x <= 0)
            {
                direction.X = 1;
            }
            // 下端に到達
            if (y > MainPictureBox.Height - radius * 2)
            {
                direction.Y = -1;
            }
            // 上端に到達
            if (y <= 0)
            {
                direction.Y = 1;
            }

            // 新しい位置に描画
            PutCircle(x, y, Brushes.LightBlue);
        }

        // 選択用円の描画
        private void DrawCircleSelectPictureBox()
        {
            Bitmap selectCanvas = new Bitmap(SelectPictureBox.Width, SelectPictureBox.Height);
            using (Graphics g = Graphics.FromImage(selectCanvas))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.LightBlue, 0, 0, SelectPictureBox.Height, SelectPictureBox.Height);
                SelectPictureBox.Image = selectCanvas;
            }
        }
    }
}

