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
    class Save
    {
        public void SaveInFile(ref List<ListOfUsers> listOfUsers, ref string fileOfChats)
        {
            FileStream fs = new FileStream(fileOfChats, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, listOfUsers);
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

        public void SaveInFile(ref List<NewMsgList> newMsgList, ref string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, newMsgList);
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
        
        /*public void ChatBoxSave(ref List<ChatBoxList> chatBox, ref string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, chatBox);
            }
            catch (SerializationException ex)
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
