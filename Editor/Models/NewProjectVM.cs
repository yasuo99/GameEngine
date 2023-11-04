
using Editor.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Documents;

namespace Editor.Models
{
    public class NewProjectVM : VMBase
    {
        private readonly string _templatePath = $@"..\..\..\PrimalTemplates\";
        private string _projectName = "NewProject";
        public string ProjectName
        {
            get => _projectName;
            set
            {
                if (_projectName != value)
                {
                    _projectName = value;
                    Validate();
                    OnPropertyChanged(nameof(ProjectName));
                }

            }
        }
        private string _projectPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Editor";
        public string ProjectPath
        {
            get => _projectPath; set
            {
                if (_projectPath != value)
                {
                    _projectPath = value;
                    Validate();
                    OnPropertyChanged(nameof(ProjectPath));
                }
            }
        }
        private string _errorMessage;
        private bool _isValid;
        public bool IsValid
        {
            get => _isValid; set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage; set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        private string CreateProject()
        {

        
            return string.Empty ;
        }

        private bool Validate()
        {
            var path = ProjectPath;
            if (Path.EndsInDirectorySeparator(path))
            {
                path += $@"\";
            }
            path += $"{ProjectName}";
            IsValid = false;
            if (ProjectName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                ErrorMessage = "Project name contains invalid character(s)!";
            }
            else if(path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                ErrorMessage = "Project path contains invalid character(s)!";
            }
            else if(Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any()) {
                ErrorMessage = "Project path already exists and is system path";
            }
            else
            {
                IsValid = true;
            }
            return IsValid;
        }
        private ObservableCollection<ProjectTemplate> _projectTemplate = new ObservableCollection<ProjectTemplate>();

        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplate { get; }
        public NewProjectVM()
        {
            try
            {
                ProjectTemplate = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplate);
                var templates = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templates.Any());
                foreach (var template in templates)
                {
                    ProjectTemplate project = SerializerUtils.FromFile<ProjectTemplate>(template);
                    project.IconPath = Path.GetFullPath(Path.Combine(template, @"..\icon.png"));
                    project.Icon = File.ReadAllBytes(project.IconPath);

                    project.ScreenshotPath = Path.GetFullPath(Path.Combine(template, @"..\screenshot.png"));
                    project.Screenshot = File.ReadAllBytes(project.ScreenshotPath);

                    _projectTemplate.Add(project);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
    }
}
