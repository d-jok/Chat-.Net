using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using chatClient.Models;

namespace chatClient.SaveAndLoad
{
    class Load
    {
        public void LoadFromFile(ref List<ListOfUsers> listOfUsers, ref string fileOfChats)
        {
            FileStream fs = new FileStream(fileOfChats, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                listOfUsers = (List<ListOfUsers>)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fs.Close();
            }
        }

        /*public void ChatBoxLoad(ref List<ChatBoxList> chatBox, ref string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                chatBox = (List<ChatBoxList>)formatter.Deserialize(fs);
            }
            catch(SerializationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fs.Close();
            }
        }*/
    }
}
