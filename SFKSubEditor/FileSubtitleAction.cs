using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SFKSubEditor
{
    class FileSubtitleAction
    {

        void readSubtitleFile(String fileName, Encoding fileEncoding, float frameRate, Delegate del)
        {

            using (StreamReader sw = new StreamReader(fileName, fileEncoding))
            {
                readHeader(sw);
            }
            if (fileEncoding == null || (needFramRate && frameRate == 0.0))
            {
                askforencodingAndFramRate();
            }
            using (StreamReader sw = new StreamReader(fileName, fileEncoding))
            {
                readHeader(sw);
                del(readSubtitle(sw));
            }
        }
    }
}
