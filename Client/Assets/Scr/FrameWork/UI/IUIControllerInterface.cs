namespace GameFrameWork.UI
{
    public interface IUIControllerInterface
    {
        void CreatePanel();
        void ShowPanel(params object[] args);
        
        void HidePanel();
        
        void ReleasePanel();

        bool GetActive();

        UIPopTypeEnum GetPopType();
    }
}