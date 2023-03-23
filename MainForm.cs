using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Data.SQLite;
using System.ComponentModel;



namespace uk.andyjohnson.XmpLibeRator
{
    /// <summary>
    /// Main form class. Everything is basically in here.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const string infoUrl = "https://github.com/andyjohnson0/XmpLibeRator";



        #region UI event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
#if !DEBUG
            var msg =
                "This application is carefully designed to not modify your lightroom catalogue or overwrite any files. " +
                "Nevertheless you are advised to ensure that your Lightroom catalogue and photos are securely backed-up," +
                "and to use a temporary copy of the catalogue file when using this application.\n\n" +
                "Click Ok to proceed, or click Cancel if you wish to exit the application now.";
            var dr = MessageBox.Show(this, msg, "Caution", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr != DialogResult.OK)
            {
                Application.Exit();
            }
#endif

#if DEBUG
            catalogueFilePathTbx.Text = @"C:\Users\andy\Projects\Software\XmpLibeRator\Lightroom Catalog-v12.lrcat";
            outputDirPathTbx.Text = @"C:\Users\andy\Projects\Software\XmpLibeRator\output";
            BuildFolderTree();
#endif
        }


        private void ExitMui_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void InfoMui_Click(object sender, EventArgs e)
        {
            var pi = new System.Diagnostics.ProcessStartInfo(infoUrl) { UseShellExecute = true };
            System.Diagnostics.Process.Start(pi);
        }


        private void AboutMui_Click(object sender, EventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var versionStr = version != null ? "v" + version.ToString() : "";

            MessageBox.Show(
                this,
                $"Xmp LibeRator {versionStr}\n\n" +
                $"by Andrew Johnson | andyjohnson.uk\n\n" +
                $"See {infoUrl} for more info",
                "About Xmp LibeRator",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void CatalogueFileBrowseBtn_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Title = "Open Catalogue File",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Lightroom catalogue files (*.lrcat)|*.lrcat|All files (*.*)|*.*",
                RestoreDirectory = true
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                catalogueFilePathTbx.Text = dlg.FileName;
                BuildFolderTree();
            }
        }


        private void OutputDirBrowseBtn_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                outputDirPathTbx.Text = dlg.SelectedPath;
            }
        }


        private void SelectAllCbx_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < foldersTvw.Nodes.Count; i++)
            {
                foldersTvw.Nodes[i].SetAllChecked(selectAllCbx.Checked);
            }
            if (selectAllCbx.Checked)
                selectAllCbx.Text = "Deselect all";
            else
                selectAllCbx.Text = "Select all";
        }


        private TreeNode? currentNode = null;

        private void FoldersTvw_MouseClick(object sender, MouseEventArgs e)
        {
            currentNode = foldersTvw.GetNodeAt(e.Location);
        }

        private void SelectTreeNodeChildren_Click(object? sender, EventArgs e)
        {
            if (currentNode != null)
            {
                currentNode.GetSelfAndChildren().ToList().ForEach(n => n.Checked = true);
                currentNode = null;
            }
        }
        private void DeselectTreeNodeChildren_Click(object? sender, EventArgs e)
        {
            if (currentNode != null)
            {
                currentNode.GetSelfAndChildren().ToList().ForEach(n => n.Checked = false);
                currentNode = null;
            }
        }


        private void StartBtn_Click(object sender, EventArgs e)
        {
            BuildOutputFiles();
        }

        #endregion UI event handlers


        #region Build folder tree

        /// <summary>
        /// Build the folder tree
        /// </summary>
        private void BuildFolderTree()
        {
            if (!File.Exists(catalogueFilePathTbx.Text))
            {
                MessageBox.Show(this, "The selected catalogue file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.UseWaitCursor = true;

            // Create a background worker to extract the folder structure from the catalogue file.
            var worker = new BackgroundWorker();
            worker.DoWork += BuildFolderTreeWorker_DoWork;
            worker.RunWorkerCompleted += BuildFolderTreeWorker_Completed;
            var args = new BuildFolderTreeWorkerArgs()
            {
                CatalogueFi = new FileInfo(catalogueFilePathTbx.Text)
            };
            worker.RunWorkerAsync(args);
        }


        /// <summary>
        /// Worker input arguments
        /// </summary>
        private class BuildFolderTreeWorkerArgs
        {
            public FileInfo? CatalogueFi { get; set; }
        } 


        /// <summary>
        /// Worker output results.
        /// </summary>
        private class BuildFolderTreeWorkerResult
        {
            /// <summary>
            /// Dictionary of root folder IDs to names.
            /// Note that these are the id_local IDs in AgLibraryRootFolder that are referenced by AgLibraryFolder.rootFolder.
            /// They are not the same as id_local folder IDs in AgLibraryFolder.
            /// </summary>
            public Dictionary<int, string>? RootFolders { get; set; }

            /// <summary>
            /// List of folder ID, path from root, file count, and root folder ID.
            /// </summary>
            public List<(int folderId, string pathFromRoot, int fileCount, int rootFolderId)>? Folders { get; set; }
        }


        private void BuildFolderTreeWorker_DoWork(
            object? sender,
            DoWorkEventArgs ea)
        {
            if (ea?.Argument == null)
                throw new ArgumentNullException(nameof(ea));

            var args = (BuildFolderTreeWorkerArgs)ea.Argument;
            if (args.CatalogueFi == null)
                throw new ArgumentNullException(nameof(args.CatalogueFi));
            
            var result = new BuildFolderTreeWorkerResult();
            ea.Result = result;

            var connStrBuilder = new SQLiteConnectionStringBuilder() { DataSource = args.CatalogueFi.FullName, ReadOnly = true };

            // Build a dictionary of root folder IDs to names.
            // Note that these are the id_local IDs in AgLibraryRootFolder that are referenced by AgLibraryFolder.rootFolder.
            // They are not the same as id_local folder IDs in AgLibraryFolder.
            // We use this dictionary later to build root nodes on the fly.
            result.RootFolders = new Dictionary<int, string>();
            using (var conn = new SQLiteConnection(connStrBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_local, name FROM AgLibraryRootFolder ORDER BY name ASC";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.RootFolders.Add(rdr.GetInt32(0), rdr.GetString(1));
                        }
                    }
                }
            }

            // Build tree nodes for everything.
            // Because we order results by AgLibraryFolder.pathFromRoot we are guaranteed to encounter root nodes, which have an empty
            // path, before their children.
            result.Folders = new List<(int, string, int, int)>();
            using (var conn = new SQLiteConnection(connStrBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT AgLibraryFolder.id_local, AgLibraryFolder.pathFromRoot, COUNT(AgLibraryFile.id_local) AS fc, AgLibraryFolder.rootFolder " +
                        "FROM AgLibraryFolder " +
                        "LEFT JOIN AgLibraryFile ON AgLibraryFile.folder = AgLibraryFolder.id_local " +
                        "GROUP BY AgLibraryFolder.id_local, AgLibraryFolder.pathFromRoot " +
                        "ORDER BY AgLibraryFolder.pathFromRoot ASC";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Folders.Add( (rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3)) );
                        }
                        
                    }
                }
            }
        }


        private void BuildFolderTreeWorker_Completed(object? sender, RunWorkerCompletedEventArgs ea)
        {
            if (ea?.Error != null)
            {
                this.UseWaitCursor = false;
                MessageBox.Show(this, 
                                $"An error occurred while extracting the Lightroom folder structure: {ea.Error.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ea?.Result == null)
                throw new ArgumentNullException(nameof(ea.Result));
            var result = (BuildFolderTreeWorkerResult)ea.Result;

            if (result.RootFolders == null)
                throw new ArgumentNullException(nameof(result.RootFolders));
            if (result.Folders == null)
                throw new ArgumentNullException(nameof(result.Folders));

            // Tree node context menu
            var treeNodeContextMenu = new ContextMenuStrip();
            ToolStripItem item = new ToolStripLabel("Folder") { Font = new Font(Font, FontStyle.Bold) };
            treeNodeContextMenu.Items.Add(item);
            item = new ToolStripMenuItem("&Select all Children");
            item.Click += SelectTreeNodeChildren_Click;
            treeNodeContextMenu.Items.Add(item);
            item = new ToolStripMenuItem("&Deselect all Children");
            item.Click += DeselectTreeNodeChildren_Click;
            treeNodeContextMenu.Items.Add(item);

            // Clear the tree view.
            foldersTvw.Nodes.Clear();

            // Create tree nodes.
            // In each case the node's tag (as an int) and its key in it's parent collection (as a string) will both be the
            // id_local key from AgLibraryFolder. Root nodes have an empty path string and we need to look them up in the dictionary.
            foreach (var folderInfo in result.Folders!)
            {
                // Determine the root node
                TreeNode? rootNode;
                if (!string.IsNullOrEmpty(folderInfo.pathFromRoot))
                {
                    // This is not a root node. Find the root node by name, as we don't have the AgLibraryFolder key.
                    rootNode = foldersTvw.Nodes.FindByText(result.RootFolders[folderInfo.rootFolderId]);
                }
                else
                {
                    // The path from root is empty, so this is a root folder and we need to create a root node.
                    // Get the label from the rootFoldersDict. The key will be the id from AgLibraryFolder, not AgLibraryFolder.rootFolder.
                    rootNode = foldersTvw.Nodes.Add(folderInfo.folderId.ToString(), result.RootFolders[folderInfo.rootFolderId]);
                    rootNode.Tag = folderInfo.rootFolderId;
                    rootNode.ContextMenuStrip = treeNodeContextMenu;
                }

                if (rootNode == null)
                    throw new InvalidOperationException($"Failed to find root node for folder {folderInfo.rootFolderId}");

                // Build nodes below the root node using the slash-delimited elements of pathFromRoot.
                TreeNode? currentNode = rootNode;
                var pathParts = folderInfo.pathFromRoot.Split('/', StringSplitOptions.RemoveEmptyEntries);
                foreach (var pathPart in pathParts)
                {
                    var childNode = currentNode.Nodes.FindByText(pathPart);
                    if (childNode == null)
                    {
                        var countStr = folderInfo.fileCount > 0 ? $"[{folderInfo.fileCount}] " : "";
                        childNode = currentNode.Nodes.Add(folderInfo.folderId.ToString(), $"{countStr}{pathPart}");
                        childNode.Tag = folderInfo.folderId;
                        childNode.ContextMenuStrip = treeNodeContextMenu;
                    }
                    currentNode = childNode;
                }
            }
            foldersTvw.ExpandAll();
            if (foldersTvw.Nodes.Count > 0)
                foldersTvw.Nodes[0].EnsureVisible();

            this.UseWaitCursor = false;
        }

        #endregion Build folder tree



        #region Build output files

        public void BuildOutputFiles()
        {
            if (!File.Exists(catalogueFilePathTbx.Text))
            {
                MessageBox.Show(this, "The selected catalogue file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(outputDirPathTbx.Text))
            {
                MessageBox.Show(this, "The selected output folder does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Directory.GetFiles(outputDirPathTbx.Text).Length > 0 || Directory.GetDirectories(outputDirPathTbx.Text).Length > 0)
            {
                var dr = MessageBox.Show(this,
                                "The selected output folder contains existing files and/or folders. " +
                                "If they conflict with files/folders to be created then they will not be overwritten. " +
                                "Do you wish to proceed?",
                                "Proceed?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                    return;
            }

            // Get all the selected nodes and extract the LR folder IDs from their Tag properties.
            var checkedNodes = foldersTvw.GetRootNodes().Select(n => n.GetAllChecked(true)).SelectMany(o => o);
            var folderIds = checkedNodes.Select(n => (int)n.Tag);

            this.UseWaitCursor = true;

            var worker = new BackgroundWorker();
            worker.DoWork += BuildOutputFilesWorker_DoWork;
            worker.RunWorkerCompleted += BuildOutputFilesWorker_Completed;
            var args = new BuildOutputFilesWorkerArgs()
            {
                CatalogueFi = new FileInfo(catalogueFilePathTbx.Text),
                FolderIds = folderIds,
                OutputDi = new DirectoryInfo(outputDirPathTbx.Text),
                ImageExtInOutputFilename = includeImageExtInOutputFilenameCbx.Checked
            };
            worker.RunWorkerAsync(args);
        }


        private class BuildOutputFilesWorkerArgs
        {
            public FileInfo? CatalogueFi { get; set; }
            public IEnumerable<int>? FolderIds { get; set; }
            public DirectoryInfo? OutputDi { get; set; }
            public bool ImageExtInOutputFilename { get; set; }
        }


        private class BuildOutputFilesWorkerResult
        {
            public int OutputFileCount { get; set; } = 0;
            public List<string> SkippedFiles { get; } = new List<string>();
        }



        private void BuildOutputFilesWorker_DoWork(
            object? sender,
            DoWorkEventArgs ea)
        {
            if (ea?.Argument == null)
                throw new ArgumentNullException(nameof(ea));

            var args = (BuildOutputFilesWorkerArgs)ea.Argument;
            if (args.CatalogueFi == null || args.FolderIds == null || args.OutputDi == null)
                throw new ArgumentNullException();

            var result = new BuildOutputFilesWorkerResult();
            ea.Result = result;

            var connStrBuilder = new SQLiteConnectionStringBuilder() { DataSource = args.CatalogueFi.FullName, ReadOnly = true };
            using (var conn = new SQLiteConnection(connStrBuilder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT AgLibraryRootFolder.name, AgLibraryFolder.pathFromRoot, AglibraryFile.baseName, AglibraryFile.extension, Adobe_AdditionalMetadata.xmp " +
                        "FROM Adobe_images " +
                        "LEFT JOIN AgLibraryFile ON AgLibraryFile.id_local = Adobe_Images.rootFile " +
                        "LEFT JOIN AgLibraryFolder ON AgLibraryFolder.id_local = AgLibraryFile.folder " +
                        "LEFT JOIN AgLibraryRootFolder ON AgLibraryFolder.rootFolder = AgLibraryRootFolder.id_local\r\n " +
                        "LEFT JOIN Adobe_AdditionalMetadata ON Adobe_AdditionalMetadata.image = Adobe_Images.id_local " +
                        $"WHERE AgLibraryFolder.id_local IN ({string.Join(",", args.FolderIds.Select(n => n.ToString()))}) " +
                        "ORDER BY AgLibraryFolder.pathFromRoot, AglibraryFile.baseName";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // Create a file stream that empties into an xmp file.
                            var outputDirPath = Path.Join(args.OutputDi.FullName, rdr.GetString(0), rdr.GetString(1));
                            if (!Directory.Exists(outputDirPath))
                                Directory.CreateDirectory(outputDirPath);
                            var ext = args.ImageExtInOutputFilename ? "." + rdr.GetString(3) : "";
                            var outputFilePath = Path.Join(outputDirPath, rdr.GetString(2) + ext + ".xmp");
                            if (!File.Exists(outputFilePath))
                            {
                                using (var fileStm = new FileStream(outputFilePath, FileMode.CreateNew, FileAccess.Write))
                                {
                                    // Open the XMP blob as a stream.
                                    using (var blobStm = rdr.GetStream(4))
                                    {
                                        // Skip over the blob's uncompressed length value.
                                        blobStm.Seek(4, SeekOrigin.Begin);

                                        // Create a zlib decompression stream that writes to the file stream.
                                        using (var decompressStm = new Ionic.Zlib.ZlibStream(fileStm, Ionic.Zlib.CompressionMode.Decompress))
                                        {
                                            // Copy the blob into the decomression stream.
                                            blobStm.CopyTo(decompressStm);
                                        }
                                    }
                                }
                                result.OutputFileCount += 1;
                            }
                            else
                            {
                                // Output file already exists. Probably a copy.
                                var path = "/" + rdr.GetString(0) + "/" + rdr.GetString(1) + rdr.GetString(2) + "." + rdr.GetString(3);
                                result.SkippedFiles.Add(path);
                            }
                        }  // end while read
                        ea.Result = result;
                    }
                }
            }
            // All done
        }


        private void BuildOutputFilesWorker_Completed(object? sender, RunWorkerCompletedEventArgs ea)
        {
            if (ea?.Result == null)
                throw new ArgumentNullException(nameof(ea));

            var result = (BuildOutputFilesWorkerResult)ea.Result;

            this.UseWaitCursor = false;

            // Create report file.
            var reportPath = Path.Join(outputDirPathTbx.Text, "XmpLibeRator report.txt");
            using (var wtr = new StreamWriter(reportPath, false))
            {
                if (ea.Error != null)
                {
                    wtr.WriteLine($"Metadata extraction was stopped due to an error: {ea.Error.Message}");
                }
                else
                {
                    wtr.WriteLine("No errors were encountered");
                }

                if (result.SkippedFiles.Count > 0)
                {
                    wtr.WriteLine($"{result.SkippedFiles.Count} file(s) were skipped:");
                    foreach (var path in result.SkippedFiles)
                    {
                        wtr.WriteLine($"    {path}");
                    }
                }
                else
                {
                    wtr.WriteLine("No files were skipped");
                }
            }

            // Display outcome.
            if (ea.Error != null)
            {
                MessageBox.Show(this,
                                $"An error occurred: {ea.Error.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(this,
                                $"Successfully created {result.OutputFileCount} metadata file(s). {result.SkippedFiles.Count} file(s) were skipped.",
                                "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Build output files
    }
}
