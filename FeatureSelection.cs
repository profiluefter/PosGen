using System;

namespace PosGen
{
    public class FeatureSelection
    {
        public string ProjectName { get; set; }
        public string ProjectLocation { get; set; }
        public ProjectTypes ProjectType { get; set; }
        public bool InsertCliGui { get; set; }

        public static FeatureSelection of(string projectName, string projectLocation, string projectType,
            string insertCliGui)
        {
            var selection = new FeatureSelection();
            selection.ProjectName = projectName;
            selection.ProjectLocation = projectLocation;

            Enum.TryParse(projectType.ToUpper(), out ProjectTypes type);
            selection.ProjectType = type;

            selection.InsertCliGui = insertCliGui.Contains("y");

            return selection;
        }

        public enum ProjectTypes
        {
            ANT,
            MAVEN
        }
    }
}