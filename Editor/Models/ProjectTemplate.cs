﻿
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Editor.Models
{
    [DataContract]
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }
        public string IconPath { get; set; }
        public byte[] Icon { get; set; }
        public string ScreenshotPath { get; set; }
        public byte[] Screenshot { get; set; }
    }
}
