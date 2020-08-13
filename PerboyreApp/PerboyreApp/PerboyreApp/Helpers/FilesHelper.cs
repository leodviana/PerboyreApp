﻿using System;
using System.IO;

namespace PerboyreApp.Helpers
{
    
        public  class FilesHelper
        {
            public static byte[] ReadFully(Stream input)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    input.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    
}
