namespace SPSD.Editor.Interfaces
{
    internal interface IAction
    {
        bool AfterDeploy { get; set; }
        bool AfterDeploySpecified { get; set; }
        bool AfterRetract { get; set; }
        bool AfterRetractSpecified { get; set; }
        bool AfterUpdate { get; set; }
        bool AfterUpdateSpecified { get; set; }
    }
}