using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.DAL
{
    public class SaveImage
    {
        private static byte[] image;
        private static string extension;

        public void setImage(byte[] image, string extension)
        {
            SaveImage.image = image;
            SaveImage.extension = extension;
        }

        public byte[] getImage()
        {
            return image;
        }

        public string getExtension()
        {
            return extension;
        }
    }
}
