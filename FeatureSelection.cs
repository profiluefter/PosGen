using System;

namespace PosGen
{
    public class FeatureSelection
    {
        public string ProjectName { get; private set; }
        public string ProjectLocation { get; private set; }
        public ProjectTypes ProjectType { get; set; }
        public string PackageName { get; set; }
        public bool InsertCliGui { get; set; }

        public static FeatureSelection Of(string projectName, string projectLocation, string projectType,
            string packageName, string insertCliGui)
        {
            return new FeatureSelection
            {
                ProjectName = projectName,
                ProjectLocation = projectLocation,
                InsertCliGui = insertCliGui.Contains("y"),
                PackageName = packageName,
                ProjectType = projectType == ProjectTypes.Ant.ToString().ToLower()
                    ? ProjectTypes.Ant
                    : ProjectTypes.Maven
            };
        }

        public enum ProjectTypes
        {
            Ant,
            Maven
        }
    }
}