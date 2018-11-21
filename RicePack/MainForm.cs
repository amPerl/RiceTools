using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RicePack.Archives;
using RicePack.Files;

namespace RicePack
{
    public partial class MainForm : Form
    {
        public static FileInfo ActiveFile;
        public static ArchiveBase ActiveArchive;
        public static TreeNode SelectedNode;
        public static ArchiveFile SelectedFile;
        public static TDF ActiveTDF;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private TreeNode createNode(ArchiveFolder folder)
        {
            var nodes = new List<TreeNode>();

            foreach (var subfolder in folder.SubFolders)
                nodes.Add(createNode(subfolder));

            foreach (var file in folder.Files)
                nodes.Add(createNode(file));

            return new TreeNode(folder.Name, nodes.ToArray()) { Tag = folder };
        }

        private TreeNode createNode(ArchiveFile file)
        {
            return new TreeNode(file.ToString()) { Tag = file };
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "RicePack by savetherobots (@am_Perl)\n" +
                "Reverse engineering by savetherobots & cosmos\n\n" +
                $"Build v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}"
            );
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = e.Node;

            bool isTDF = SelectedNode.Tag is AGTFile && (SelectedNode.Tag as AGTFile).Name.Contains(".tdf");
            tdfToolStripMenuItem.Enabled = isTDF;
            ActiveTDF = isTDF ? TDF.Load((SelectedFile as AGTFile).Decompress(), tdfGridView) : null;

            tdfGridView.Rows.Clear();
            tdfGridView.Columns.Clear();
            ActiveTDF = null;

            if (!(SelectedNode.Tag is ArchiveFile))
                return;

            SelectedFile = SelectedNode.Tag as ArchiveFile;
        }

        private void saveTdfButton_Click(object sender, EventArgs e)
        {
            SelectedFile.Load(ActiveTDF.Save(tdfGridView));
            MessageBox.Show("Saved TDF.");
        }

        private void fileSaveButton_Click(object sender, EventArgs e)
        {
            ActiveArchive.Save(ActiveFile.FullName);
            MessageBox.Show("Saved AGT as " + ActiveFile.FullName + ".");
        }

        private void fileSaveAsButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog { FileName = ActiveFile.Name };

            if (diag.ShowDialog() != DialogResult.OK)
                return;

            ActiveArchive.Save(diag.FileName);
            MessageBox.Show("Saved AGT as " + diag.FileName + ".");
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog { FileName = SelectedFile.Name };

            if (diag.ShowDialog() != DialogResult.OK)
                return;

            SelectedFile.Save(diag.FileName);
            MessageBox.Show("Saved file as " + diag.FileName + ".");
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog { CheckFileExists = true };

            if (diag.ShowDialog() != DialogResult.OK)
                return;

            SelectedFile.Load(diag.FileName);
            MessageBox.Show("Replaced file contents with " + diag.FileName + ".");
        }

        private void columnReplaceButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tdfGridView.Rows)
            {
                foreach (DataGridViewColumn col in tdfGridView.SelectedColumns)
                {
                    row.Cells[col.Index].Value = columnReplaceValueBox.Text;
                }
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tdfGridView.Rows)
            {
                foreach (DataGridViewTextBoxCell cell in row.Cells)
                {
                    if ((cell.Value as string).Contains(findBox.Text))
                    {
                        tdfGridView.ClearSelection();
                        cell.Selected = true;
                        tdfGridView.CurrentCell = cell;
                        tdfGridView.BeginEdit(true);
                        return;
                    }
                }
            }
        }

        private void exportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();

            if (diag.ShowDialog() != DialogResult.OK)
                return;

            extractFolder(ActiveArchive.RootFolder, diag.SelectedPath);

            MessageBox.Show($"Extracted to ${diag.SelectedPath}.");
        }

        private void extractFolder(ArchiveFolder folder, string outPath)
        {
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            foreach (var subFolder in folder.SubFolders)
                extractFolder(subFolder, Path.Combine(outPath, subFolder.Name));

            foreach (var file in folder.Files)
                file.Save(Path.Combine(outPath, file.Name));
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog { CheckFileExists = true };

            if (diag.ShowDialog() != DialogResult.OK)
                return;

            var selectedItem = SelectedNode.Tag;
            if (SelectedNode.Tag is ArchiveFile)
            {
                var selectedFile = SelectedNode.Tag as ArchiveFile;
                var newFile = selectedFile.ParentFolder.AddFile(diag.FileName);
                SelectedNode.Parent.Nodes.Add(createNode(newFile));
            } else if (SelectedNode.Tag is ArchiveFolder)
            {
                var selectedFolder = SelectedNode.Tag as ArchiveFolder;
                var newFile = selectedFolder.AddFile(diag.FileName);
                SelectedNode.Nodes.Add(createNode(newFile));
            }
            else
            {
                MessageBox.Show("Cannot add file to selected node.");
                return;
            }

            MessageBox.Show("Added " + diag.FileName + " to archive.");
        }

        private void openFile(string filePath, ArchiveBase archive)
        {
            // set defaults here
            setReadOnly(false);

            fileTreeView.Nodes.Clear();
            ActiveFile = new FileInfo(filePath);
            ActiveArchive = archive;
            ActiveArchive.Load(ActiveFile.FullName);

            TreeNode node = createNode(ActiveArchive.RootFolder);
            foreach (TreeNode subnode in node.Nodes)
                fileTreeView.Nodes.Add(subnode);

            archiveToolStripMenuItem.Enabled = true;
        }

        private void openAGTButton_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog { CheckFileExists = true };
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            openFile(diag.FileName, new AGT());
        }

        private void openLOFButton_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog { CheckFileExists = true };
            if (diag.ShowDialog() != DialogResult.OK)
                return;

            openFile(diag.FileName, new LOF());
            setReadOnly(true);
        }

        private void setReadOnly(bool readOnly)
        {
            // make sure openFile resets these
            fileSaveButton.Enabled = !readOnly;
            fileSaveAsButton.Enabled = !readOnly;
            replaceToolStripMenuItem.Enabled = !readOnly;
            addFileToolStripMenuItem.Enabled = !readOnly;
        }
    }
}
