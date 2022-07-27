using System;
using System.Collections.Generic;

namespace PrettyWorld.Models
{
    public partial class MyProfile
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? Introduction { get; set; }
        public string? LiveIn { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public int JavascriptLevel { get; set; }
        public int Htmllevel { get; set; }
        public int Csslevel { get; set; }
        public int BootstrapLevel { get; set; }
        public int Ajaxlevel { get; set; }
        public int CsharpLevel { get; set; }
        public int JavaLevel { get; set; }
        public int PythonLevel { get; set; }
        public int Mssqllevel { get; set; }
        public int GitLevel { get; set; }
    }
}
