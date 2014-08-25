#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using SPSD.Editor.Interfaces;
using SPSD.Editor.Properties;

#endregion

namespace SPSD.Editor.Utilities
{
    internal class EnvironmentFileHandler
    {
        private static EnvironmentFileHandler _self;
        private readonly IFileHandler<Model.SPSD> _control;
        private readonly List<string> notifications = new List<string>();
        private string _filename;
        private string _filepath;

        private bool _isDirty;
        private bool _isReadonly;
        private Model.SPSD _spsd;

        public EnvironmentFileHandler(IFileHandler<Model.SPSD> control)
        {
            _control = control;
            if (_self != null)
            {
                throw new ArgumentException("EnvironmentFileHandler is a Singleton and can only instantiated once.");
            }
            _self = this;
            if (_control == null)
            {
                throw new NullReferenceException(
                    "A null object was passed to the EnvironmentFileHandler. Valid objects are of type IFileHandler<Model.SPSD>");
            }
        }

        public string Filename
        {
            get { return _filename; }
        }
        public string Filepath
        {
            get { return _filepath; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public bool IsReadonly
        {
            get { return _isReadonly; }
        }

        public static bool IsSingletonNew
        {
            get
            {
                if (_self == null)
                {
                    return true;
                }
                return _self.IsNew;
            }
        }

        public bool IsNew
        {
            get { return string.IsNullOrEmpty(_self._filename); }
        }

        public static void AddNotification(string notification)
        {
            if (_self != null)
            {
                _self.notifications.Add(notification);
            }
        }

        private void MakeDirty()
        {
            _isDirty = true;
            FileOperationCompleted.Invoke(this, new EventArgs());
        }

        public static void MakeSingletonDirty()
        {
            if (_self != null)
            {
                _self.MakeDirty();
            }
        }

        public static Model.SPSD LoadFile(string filepath)
        {
            Model.SPSD file = null;
            try
            {
                FileInfo fi = new FileInfo(filepath);
                if (fi.Exists)
                {
                    using (FileStream filestream = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read))
                    {
                        string content;
                        using (StreamReader reader = new StreamReader(filestream))
                        {
                            content = reader.ReadToEnd();
                            reader.Close();
                        }
                        // can be done better directly in the serializer but will to for now

                        content = Regex.Replace(content, "\"true\"", "\"true\"", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, "\"false\"", "\"false\"", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, ">true<", ">true<", RegexOptions.IgnoreCase);
                        content = Regex.Replace(content, ">false<", ">false<", RegexOptions.IgnoreCase);
                        using (MemoryStream memStream = new MemoryStream(Encoding.ASCII.GetBytes(content)))
                        {
                            var serializer = new XmlSerializer(typeof (Model.SPSD));
                            file = serializer.Deserialize(memStream) as Model.SPSD;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("The file \"{0}\" does not exist!", filepath), "File does not exist",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                string.Format(
                                    "Exception while loading \"{0}\". The file has to be a valid SPSD environment file.",
                                    filepath),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return file;
        }

        public bool Load(string filepath)
        {
            if (IsDirty)
            {
                if (MessageBox.Show(
                    Resources.EnvironmentFileHandler_Load_UnsavedChanges,
                    Resources.EnvironmentFileHandler_Load_UnsavedChangesTitle, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }
            if (string.IsNullOrEmpty(filepath))
            {
                InitNewFile();
                return true;
            }
            FileInfo fi = new FileInfo(filepath);
            _spsd = LoadFile(filepath);
            if (_spsd != null)
            {
                if (!string.IsNullOrEmpty(_spsd.Version) &&
                    !_spsd.Version.Equals(Assembly.GetExecutingAssembly().GetName().Version.ToString()))
                {
                    // Versions do not match
                    _spsd.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                _control.LoadEnv(_spsd);
                _filename = fi.Name;
                _filepath = fi.FullName;
                _isReadonly = fi.IsReadOnly;
                Clean();
                return true;
            }
            else
            {
                return false;
            }
        }

        public event EventHandler FileOperationCompleted;

        private void InitNewFile()
        {
            _spsd = new Model.SPSD();
            _spsd.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            _control.LoadEnv(_spsd);
            _isReadonly = false;
            _filepath = null;
            _filename = null;
            MakeSingletonDirty();
        }

        public static bool SaveFile(string filepath, Model.SPSD file)
        {
            try
            {
                using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                {
                    var serializer = new XmlSerializer(typeof (Model.SPSD));
                    serializer.Serialize(fileStream, file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), string.Format("Exception while saving file \"{0}\"", filepath),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool Save(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                throw new ArgumentNullException(
                    "Save without parameters can only be invoked on files which have been loaded before.");
            }
            notifications.Clear();
            _control.SaveEnv(_spsd);
            if (notifications.Count > 0)
            {
                MessageBox.Show("Saving the file has been canceled due to validation errors:\n\n" +
                                string.Join("\n- ",
                                            notifications.ToArray()),
                                Resources.EnvironmentFileHandler_Save_ValidationFailedTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            FileInfo fi = new FileInfo(filepath);
            if (fi.Exists && fi.IsReadOnly)
            {
                MessageBox.Show("The file is readonly and couldn't be saved.", "Save aborted",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (SaveFile(filepath, _spsd))
            {
                _filename = fi.Name;
                _filepath = fi.FullName;
                _isDirty = false; // clean to avoid messagebox
                _isReadonly = false;
                _control.LoadEnv(_spsd);
                Clean();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            return Save(_filepath);
        }

        internal void Clean()
        {
            _isDirty = false;
            notifications.Clear();
            FileOperationCompleted.Invoke(this, new EventArgs());
        }

        public static void CleanSingleton()
        {
            if (_self != null)
            {
                _self.Clean();
            }
        }

        internal static FileInfo GetFile()
        {
            if (_self != null && !string.IsNullOrEmpty(_self._filepath))
            {
                return new FileInfo(_self.Filepath);
            }
            return null;
        }
    }
}