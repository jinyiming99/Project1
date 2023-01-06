namespace GameFrameWork.UI
{
    public interface IUIControllerInterface
    {
        void ShowPanel(params object[] args);
        
        void HidePanel();
        
        void ReleasePanel();

        UIPopTypeEnum GetPopType();
        
    }
}