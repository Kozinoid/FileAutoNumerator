using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileAutoNumerator
{
    public partial class Form1 : Form
    {
        string currentPath = "";
        string mainName = "NoName";
        int numberOfDigits = 2;
        string devider = "_";
        int shift = 1;


        public Form1()
        {
            InitializeComponent();

            RefreshOutTittle();
        }

        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = currentPath;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filenames = Directory.GetFiles(currentPath);
                if (filenames.Length > 0)
                {
                    currentPath = fbd.SelectedPath;
                    AddFilesInList(filenames);
                }
            }
        }

        private void openFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = currentPath;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filenames = ofd.FileNames;
                if (filenames.Length > 0)
                {
                    currentPath = Path.GetDirectoryName(filenames[0]);
                    AddFilesInList(filenames);
                }
                
            }
        }

        private void AddFilesInList(string[] filenames)
        {
            lb_InList.Items.Clear();
            lb_OutList.Items.Clear();
            string[] arr = currentPath.Split('\\');
            int len = arr.Length;
            mainName = arr[len - 1];
            foreach (string name in filenames)
            {
                lb_InList.Items.Add(Path.GetFileName(name));
            }
            RefreshOutList();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lb_InList.Items.Count; i++)
            {
                string initialFileName = Path.Combine(currentPath, lb_InList.Items[i].ToString());
                string newFileName = Path.Combine(currentPath, lb_OutList.Items[i].ToString());
                File.Move(initialFileName, newFileName);
                lb_InList.Items[i] = lb_OutList.Items[i];
            }

            MessageBox.Show("Files was renamed succesfully!");
        }

        private void RefreshOutTittle()
        {
            string nameTemplate = "";
            for (int i = 0; i < numberOfDigits; i++)
            {
                nameTemplate += "#";
            }
            nameTemplate += devider;
            nameTemplate += mainName;
            label2.Text = "Out files: " + nameTemplate;
        }

        private void RefreshOutList()
        {
            lb_OutList.Items.Clear();

            if (lb_InList.Items.Count > 0)
            {
                for (int i = 0; i < lb_InList.Items.Count; i++)
                {
                    string name = "";
                    string ext = Path.GetExtension(lb_InList.Items[i].ToString());
                    switch (numberOfDigits)
                    {
                        case 2:
                            name = string.Format("{0:d2}", i + shift);
                            break;
                        case 3:
                            name = string.Format("{0:d3}", i + shift);
                            break;
                        case 4:
                            name = string.Format("{0:d4}", i + shift);
                            break;
                        case 5:
                            name = string.Format("{0:d5}", i + shift);
                            break;
                        case 6:
                            name = string.Format("{0:d6}", i + shift);
                            break;
                        case 7:
                            name = string.Format("{0:d7}", i + shift);
                            break;
                        case 8:
                            name = string.Format("{0:d8}", i + shift);
                            break;
                        case 9:
                            name = string.Format("{0:d9}", i + shift);
                            break;
                        case 10:
                            name = string.Format("{0:d10}", i + shift);
                            break;
                    }
                    name += devider;
                    name += mainName;
                    name += ext;
                    lb_OutList.Items.Add(name);

                    renameToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                renameToolStripMenuItem.Enabled = false;
            }
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatDialog fd = new FormatDialog();
            fd.MainName = mainName;
            fd.Devider = devider;
            fd.NumberOfDigits = numberOfDigits;
            fd.Shift = shift;
            if (fd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                mainName = fd.MainName;
                devider = fd.Devider;
                numberOfDigits = fd.NumberOfDigits;
                shift = fd.Shift;
                RefreshOutTittle();
                RefreshOutList();
            }
        }


        private void lb_InList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteItem();
                    break;
            }
                        
        }

        private void DeleteItem()
        {
            int selItem = lb_InList.SelectedIndex;
            if (selItem >= 0)
            {
                if (MessageBox.Show("Are you sure?", "Delete item", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lb_InList.Items.RemoveAt(selItem);
                    RefreshOutList();
                }
            }
        }

        // ----------------------------------- Drag & Drop ----------------------------------------------------

        private void lb_InList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;  
        }

        private void lb_InList_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filenames.Length > 0)
            {
                currentPath = Path.GetDirectoryName(filenames[0]);
                AddFilesInList(filenames);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_InList.Items.Clear();
            lb_OutList.Items.Clear();
            renameToolStripMenuItem.Enabled = false;
        }
        //----------------------------------------------------------------------------------------------------------
    }
}
