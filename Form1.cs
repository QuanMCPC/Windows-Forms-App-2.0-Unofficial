using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Template
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }
        /*
         * Windows Presentation Foundation (WPF) (Also know as Windows Form App (WFA)) 2.0 (Unofficial) by Quan_MCPC (Also know as: QuanMCPC)
         * Very Important Information:
         * - To change the title of the program, you just need to edit the label on the top.
         *   __________________________________________
         *  | [] <insert_label_here>             _ 0 X |
         *  |------------------------------------------|
         *  |                                          |
         *  ...
         *  - To change the icon of the program, you will need to change the Icon property of the form
         *    and the Image property of the PictureBox on the top-left corner.
         *  and that's all!
         */
        #region Very important code to make the program run. Do Not Edit the code UNLESS you know what you're doing.
        private Rectangle rfirst, rsecond, rthird;
        private Region r;
        const int _ = 4;
        #region This part of code is for handle user resizing.
        Rectangle Top1 { get { return new Rectangle(0, 0, ClientSize.Width, _); } }
        Rectangle Left1 { get { return new Rectangle(0, 0, _, ClientSize.Height); } }
        Rectangle Bottom1 { get { return new Rectangle(0, ClientSize.Height - _, ClientSize.Width, _); } }
        Rectangle Right1 { get { return new Rectangle(ClientSize.Width - _, 0, _, ClientSize.Height); } }
        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(ClientSize.Width - _, ClientSize.Height - _, _, _); } }
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == 0x84 && WindowState != FormWindowState.Maximized) // WM_NCHITTEST
            {
                Point cursor = PointToClient(Cursor.Position);
                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)13;//;HTTOPLEFT
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)14;//HTTOPRIGHT
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)16;//HTBOTTOMLEFT
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)17;//HTBOTTOMRIGHT
                else if (Top1.Contains(cursor)) message.Result = (IntPtr)12;//HTTOP
                else if (Left1.Contains(cursor)) message.Result = (IntPtr)10;//HTLEFT
                else if (Right1.Contains(cursor)) message.Result = (IntPtr)11;//HTRIGHT
                else if (Bottom1.Contains(cursor)) message.Result = (IntPtr)15;//HTBOTTOM
            }
            base.WndProc(ref message);
        }
        #endregion
        #region This part of code is for handle the three button on the top-right corner of the screen
        private void MaxRestoreButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            { WindowState = FormWindowState.Normal; MaxRestoreButton.BackgroundImage = Properties.Resources.maximize; }
            else
            { WindowState = FormWindowState.Maximized; MaxRestoreButton.BackgroundImage = Properties.Resources.restore_down; }
        }
        private void MinimizeButton_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }
        private void CloseButton_MouseClick(object sender, EventArgs e) { Application.Exit(); }
        private void Form1_Load(object sender, EventArgs e) { MaximumSize = Screen.PrimaryScreen.WorkingArea.Size; Text = Title.Text; }
        #endregion
        #region This is for allow user to resize the window on the menu bar (because of some reason idk)
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            r = new Region(new Rectangle(0, 0, MenuBar.Width, MenuBar.Height));
            rfirst = new Rectangle(0, 0, _, MenuBar.Height);
            rsecond = new Rectangle(0, 0, MenuBar.Width, _);
            rthird = new Rectangle(MenuBar.Width - _, 0, _, MenuBar.Height);
            r.Exclude(rfirst); r.Exclude(rsecond); r.Exclude(rthird);
            MenuBar.Region = r;
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Brush b = new SolidBrush(Color.FromArgb(48, 48, 48));
            g.FillRectangle(b, rfirst);
            g.FillRectangle(b, rsecond);
            g.FillRectangle(b, rthird);
        }
        #endregion
        #region This is for allow user to move the window
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr one, int two, int three, int four);
        private void Panel1_MouseDown(object sender, MouseEventArgs e) { ReleaseCapture(); SendMessage(Handle, 0x112, 0xf012, 0); }
        private void Title_MouseDown(object sender, MouseEventArgs e) { Panel1_MouseDown(sender, e); }
        #endregion
        #region Misc. code
        private void Form1_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized: MaxRestoreButton.BackgroundImage = Properties.Resources.restore_down; break;
                case FormWindowState.Normal: MaxRestoreButton.BackgroundImage = Properties.Resources.maximize; break;
            }
        }
        #endregion
        #endregion
    }
}