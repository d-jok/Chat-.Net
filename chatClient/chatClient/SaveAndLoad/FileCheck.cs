using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace chatClient.SaveAndLoad
{
    class FileCheck
    {
        public void FileExist(string path)
        {
            if (!File.Exists(path))
                File.Create(path);
        }
    }
}
