using SyncTool;
using System.Drawing;
using System.Windows.Forms;

namespace SyncToolOld
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            // Replace this with the actual path to your image
            string path = @"https://ql-sdl.hanhchinhcong.net/sites/qldl/public/HinhAnh/1bfb5ff8-1846-4d80-b31e-f06b39165feb/2023/05/09/isirme/lCv88YItV3U4XionSTKg466XpreYfr2AvqnLq8LD.jpg";
            Image img = Utils.GetImageFromUrl(path);
            pictureOrigin.Image = img;
            Image resize = Utils.ResizeImage(img, new Size(500, 500));
            PictureResize.Image = resize;
        }
    }
}
